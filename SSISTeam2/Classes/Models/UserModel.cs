using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System;
using System.DirectoryServices.AccountManagement;

namespace SSISTeam2.Classes.Models
{
    public class UserModel
    {
        private string username, fullname;
        private string email;
        private Department department;
        private string role;
        private string mobilenum;

        public List<string> ROLES = new List<string>(new string[] { "DeptHead", "Manager", "Supervisor", "Clerk", "Employee" });
        //public static readonly string[] ROLES = { "DeptHead", "Manager", "Supervisor", "Clerk", "Employee" };

        //public enum UserRoles { DeptHead, Manager, Supervisor, Clerk, Employee }

        public UserModel(string username)
        {
            SSISEntities context = new SSISEntities();
            Dept_Registry user = context.Dept_Registry.Where(x => x.username.ToLower() == username.ToLower()).ToList().First();
            Department dept = context.Departments.Where(x => x.dept_code == user.dept_code).ToList().First();

            //Membership.FindUsersByName(username);            
            this.username = user.username;
            // Cannot enable yet, as members do not exist in asp.net db
            MembershipUser thisUser = Membership.GetUser(username);
            this.email = thisUser != null ? thisUser.Email : "sa44ssisteamtwo+" + username + "@gmail.com";
            this.department = dept;

            var roles = Roles.GetRolesForUser(username);
            if (roles.Count() > 0)
            {
                var filteredRoles = ROLES.Where(w => roles.Contains(w));
                this.role = filteredRoles.Count() > 0 ? filteredRoles.First() : ROLES.Last();
            } else
            {
                this.role = "Employee";
            }

            //this.fullname = UserPrincipal.Current.DisplayName;
            this.fullname = user.fullname;
            if (user.mobile_no == null)
                this.mobilenum = "";
            else
                this.mobilenum = user.mobile_no;
        }

        public bool isDeptHead()
        {
            // If this user is a delegate head, or if he is the department head
            return
                this.username == this.FindDelegateOrDeptHead().username
                || this.username == this.department.head_user
                || this.role == "DeptHead"
                || this.role == "Manager"
                ;
        }
        public bool isDeptHeadButNotStoreManager()
        {
            // If this user is a delegate head, or if he is the department head
            return (
                this.username == this.FindDelegateOrDeptHead().username
                || this.username == this.department.head_user
                || this.role == "DeptHead"
                )
                && this.role != "Manager"
                && this.department.dept_code != "STOR";
        }
        public bool isDelegateHead()
        {
            // If he is a depthead, but he is not the original head
            return isDeptHead() && this.username != this.department.head_user;
        }
        public bool isEmployee()
        {
            return this.role == "Employee" && !isDeptHead();
        }
        public bool isDepartmentRep()
        {
            using (SSISEntities context = new SSISEntities())
            {
                // Check if user is department rep
                int count = context.Departments.Where(d => d.rep_user == username).Count();

                return count > 0;
            }

        }
        public bool isStoreManager()
        {
            return this.role == "Manager" || ( isDeptHead() && this.department.dept_code == "STOR" );
        }
        public bool isStoreSupervisor()
        {
            return this.role == "Supervisor" && !isDeptHead();
        }
        public bool isStoreClerk()
        {
            return this.role == "Clerk" && !isStoreSupervisor() && !isDeptHead();
        }

        public UserModel FindStoreSupervisor()
        {
            SSISEntities context = new SSISEntities();
            string username = "";
            if (this.role != "Clerk")
            {
                //return null;
            }
            List<Dept_Registry> allDeptEmp = context.Dept_Registry.Where(x => x.dept_code == department.dept_code).ToList();

            foreach (Dept_Registry i in allDeptEmp)
            {
                var roles = Roles.GetRolesForUser(i.username);
                if (roles.Length == 0) continue;

                if (roles.First().ToString() == "Supervisor")
                {
                    username = i.username;
                    break;
                }
            }

            if (username == null)
            {
                return null;
            }

            return new UserModel(username);
        }

        public UserModel FindDelegateOrDeptHead()
        {
            SSISEntities context = new SSISEntities();

            // Check delegate table if there is a delegate entry for this time period
            // If yes, return that person
            var delegateHead = FindDelegateHead();
            if (delegateHead != null)
            {
                return delegateHead;
            }

            string username = "";
            Department dept = this.department;
            List<Dept_Registry> allDeptEmp = context.Dept_Registry.Where(x => x.dept_code == dept.dept_code).ToList();
            foreach (Dept_Registry i in allDeptEmp)
            {
                var roles = Roles.GetRolesForUser(i.username);
                if (roles.Length == 0) continue;

                if (roles.First().ToString() == "DeptHead")
                {
                    username = i.username;
                    break;
                }
            }

            // Backup search
            if (username == "")
            {
                username = dept.head_user;
            }

            UserModel deptHead = new UserModel(username);
            return deptHead;
        }

        public List<UserModel> FindAllDeptUser()
        {
            SSISEntities context = new SSISEntities();
            string dept = this.department.dept_code;
            List<Dept_Registry> regList = context.Dept_Registry.Where(x => x.dept_code == dept).ToList();
            List<UserModel> deptList = new List<UserModel>();
            foreach (Dept_Registry i in regList)
            {
                UserModel user = new UserModel(i.username);
                deptList.Add(user);
            }
            return deptList;
        }

        public UserModel FindDelegateHead()
        {
            try
            {
                DateTime today = DateTime.Today;
                SSISEntities context = new SSISEntities();
                string dept = this.department.dept_code;
                List<Approval_Duties> approvedList = context.Approval_Duties.Where(x => x.dept_code == dept && x.deleted == "N").ToList();
                List<Approval_Duties> validList = new List<Approval_Duties>();
                for (int i = 0; i < approvedList.Count; i++)
                {
                    if (approvedList[i].start_date.Ticks <= today.Ticks && approvedList[i].end_date.Ticks >= today.Ticks)
                    {
                        validList.Add(approvedList[i]);
                    }
                }

                DateTime currentApproved = validList.Max(x => x.created_date);
                //var listOfApproved = context.Approval_Duties.Where(x => x.created_date == currentApproved);
                var listOfApproved = validList.Where(x => x.created_date == currentApproved);

                if (listOfApproved.Count() > 0)
                {
                    Approval_Duties currentRep = listOfApproved.Last();
                    UserModel repUser = new UserModel(currentRep.username);

                    repUser.role = "DeptHead";
                    return repUser;
                }
                else
                {
                    return null;
                }
            } catch (Exception)
            {
                return null;
            }
            
        }

        public UserModel FindDeptRep()
        {
            SSISEntities context = new SSISEntities();
            Department dept = context.Departments.Where(x => x.dept_code == this.department.dept_code).ToList().First();
            string repUser = dept.rep_user;
            UserModel repUserModel = new UserModel(repUser);
            return repUserModel;
        }

        //public UserModel
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string MobileNum
        {
            get
            {
                return mobilenum;
            }

            set
            {
                mobilenum = value;
            }
        }

        internal Department Department
        {
            get
            {
                return department;
            }

            set
            {
                department = value;
            }
        }

        public string Fullname
        {
            get
            {
                return fullname;
            }

            set
            {
                fullname = value;
            }
        }

    }
}