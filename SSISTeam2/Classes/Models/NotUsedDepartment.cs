using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2;

namespace SSISTeam2.Classes.Models
{
    public class NotUsedDepartment
    {
        private string deptCode;
        private string deptname;
        private string repName;
        private string contactUser;
        private string contactNum;
        private string faxNum;
        private string headUser;
        private int collectionPoint;
        private string logoPath;

        public NotUsedDepartment() : this ("","","","","","","",0,"")
        {

        }

        public NotUsedDepartment(Department department)
        {
            deptCode = department.dept_code;
            deptname = department.name;
            repName = department.rep_user;
            contactUser = department.contact_user;
            contactNum = department.contact_num;
            faxNum = department.fax_num;
            headUser = department.head_user;
            collectionPoint = department.collection_point;
            logoPath = department.logo_path;
    }

        public NotUsedDepartment(string deptCode, string deptname, string repName, string contactUser, string contactNum, string faxNum, string headUser, int collectionPoint, string logoPath)
        {
            this.deptCode = deptCode;
            this.deptname = deptname;
            this.repName = repName;
            this.contactUser = contactUser;
            this.contactNum = contactNum;
            this.faxNum = faxNum;
            this.headUser = headUser;
            this.collectionPoint = collectionPoint;
            this.logoPath = logoPath;
        }

        public string DeptCode
        {
            get
            {
                return deptCode;
            }

            set
            {
                deptCode = value;
            }
        }

        public string Deptname
        {
            get
            {
                return deptname;
            }

            set
            {
                deptname = value;
            }
        }

        public string RepName
        {
            get
            {
                return repName;
            }

            set
            {
                repName = value;
            }
        }

        public string ContactUser
        {
            get
            {
                return contactUser;
            }

            set
            {
                contactUser = value;
            }
        }

        public string ContactNum
        {
            get
            {
                return contactNum;
            }

            set
            {
                contactNum = value;
            }
        }

        public string FaxNum
        {
            get
            {
                return faxNum;
            }

            set
            {
                faxNum = value;
            }
        }

        public string HeadUser
        {
            get
            {
                return headUser;
            }

            set
            {
                headUser = value;
            }
        }

        public int CollectionPoint
        {
            get
            {
                return collectionPoint;
            }

            set
            {
                collectionPoint = value;
            }
        }

        public string LogoPath
        {
            get
            {
                return logoPath;
            }

            set
            {
                logoPath = value;
            }
        }
    }
}