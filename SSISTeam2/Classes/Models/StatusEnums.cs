using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RequestStatus
    {
        public const string PENDING = "Pending";
        public const string CANCELLED = "Cancelled";
        public const string UPDATED = "Updated";
        public const string REJECTED = "Rejected";

        public const string APPROVED = "Approved";
        public const string PART_DISBURSED = "PartDisbursed";
        public const string DISBURSED = "Disbursed";


        private static List<string> statuses = new List<string>(new string[]{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });

        public static bool requestHasHadStatus(RequestModel request, string status)
        {
            int index = statuses.IndexOf(status);
            foreach (string statusCompare in statuses.Skip(index))
            {
                if (request.Status == statusCompare)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class EventStatus
    {
        public const string PENDING = "Pending";
        public const string CANCELLED = "Cancelled";
        public const string UPDATED = "Updated";
        public const string REJECTED = "Rejected";

        public const string APPROVED = "Approved";
        public const string ALLOCATED = "Allocated";
        public const string RETRIEVING = "Retrieving";
        public const string RETRIEVED = "Retrieved";
        public const string DISBURSING = "Disbursing";
        public const string DISBURSED = "Disbursed";

    }

    //public enum RequestServiceStatus
    //{
    //    PENDING,
    //    APPROVED,
    //    REJECTED,
    //    DISBURSED,
    //    //PART_DISBURSED,
    //    CANCELLED,
    //    UPDATED,

    //    ALLOCATED,
    //    DISBURSING
    //    //PART_ALLOCATED,

    //}

    //public enum RequestStatus
    //{

    //    PENDING,
    //    APPROVED,
    //    REJECTED,
    //    DISBURSED,
    //    PART_DISBURSED,
    //    CANCELLED,
    //    UPDATED
    //}
}
