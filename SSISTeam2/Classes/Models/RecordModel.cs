using SSISTeam2.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RecordModel
    {
        private int requestId;
        private UserModel userModel;
        private DateTime date;
        private Dictionary<ItemModel, int> items;
        private Department department;

        public RecordModel()
        {
        }
        public RecordModel(UserModel userModel, DateTime date, Department department, Dictionary<ItemModel, int> items)
        {
            this.userModel = userModel;
            this.date = date;
            this.department = department;
            this.items = items;
        }
        public RecordModel(Request efRequest, Dictionary<ItemModel, int> items)
        {
            requestId = efRequest.request_id;
            this.userModel = new UserModel(efRequest.username);
            this.date = _getLatestDateTime();
            this.department = efRequest.Department;
            this.items = items;
        }

        // Private Methods
        protected DateTime _getLatestDateTime()
        {
            using (SSISEntities context = new SSISEntities())
            {
                Request request = context.Requests.Find(RequestId);

                var requestEvents = request.Request_Details.SelectMany(sm => sm.Request_Event);
                if (requestEvents.Count() == 0)
                {
                    return request.date_time.HasValue ? request.date_time.Value : DateTime.Now;
                }

                return requestEvents.OrderByDescending(o => o.date_time).First().date_time;
            }
        }

        protected DateTime _getLatestDateTime(string status)
        {
            using (SSISEntities context = new SSISEntities())
            {
                Request thing = context.Requests.Find(RequestId);
                if (thing != null)
                { // Found one
                    List<Request_Details> deets = thing.Request_Details.ToList();
                    if (deets.Count > 0)
                    {
                        List<Request_Event> events = deets.First().Request_Event.Where(x => x.deleted != "Y").OrderBy(o => o.date_time).ToList();
                        if (events.Count > 0)
                        {
                            return events.Last().date_time;
                        }
                    }
                }

                //throw new DateNotFoundException();
                return DateTime.Now;
            }
        }

        // Computed Properties
        public string Username
        {
            get
            {
                return UserModel.Username;
            }
        }

        // Properties
        public virtual int RequestId
        {
            get
            {
                return requestId;
            }

            set
            {
                if (true)//requestId == -1) // Hasn't been assigned
                {
                    requestId = value;
                }
                else
                {
                    throw new IdentifierAlreadySetException();
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public Dictionary<ItemModel, int> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        internal UserModel UserModel
        {
            get
            {
                return userModel;
            }

            set
            {
                userModel = value;
            }
        }

        public Department Department
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

        public DateTime getDate()
        {
            return Date;
        }

        public Department getDepartment()
        {
            return Department;
        }
        public UserModel getUserModel()
        {
            return UserModel;
        }
    }
}
