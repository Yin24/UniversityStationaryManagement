using SSISTeam2.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RequestModel : AllocatedModel
    {
        
        private string reason;
        private string rejectedReason;
        private bool rejected;
        private string status;
        private bool wasRetrieved;
        private bool wasDisbursed;
        private bool wasAllocated;

        // Constructors
        public RequestModel()
        {
            RequestId = 0;
            //UserModel = 
            Date = DateTime.Now;
            Status = "";
            Items = null;
            Department = null;
            reason = "";
            rejectedReason = "";
            rejected = false;
            wasRetrieved = false;
            wasDisbursed = false;
            wasAllocated = false;
        }

        public RequestModel(Request efRequest, Dictionary<ItemModel, int> items) : this(efRequest, efRequest.Department, items)
        {
        }
        public RequestModel(Request efRequest, Department efDepartment, Dictionary<ItemModel, int> items) : this(efRequest, efDepartment, items, DateTime.Now)
        {
            Date = _getLatestDateTime(Status);
        }
        public RequestModel(Request efRequest, Department efDepartment, Dictionary<ItemModel, int> items, DateTime date)
        {
            UserModel = new UserModel(efRequest.username);
            RequestId = efRequest.request_id;
            Date = date;
            Status = efRequest.current_status;
            Items = items;
            Department = efDepartment;
            reason = efRequest.reason;
            rejectedReason = efRequest.rejected_reason;
            rejected = efRequest.rejected.ToUpper() == "Y" ? true : false;
            //wasRetrieved = _findIfWasRetrieved();
            //wasDisbursed = _findIfWasDisbursed();
            //wasAllocated = _findIfWasAllocated();
        }

        private bool _findIfWasAllocated()
        {
            throw new NotImplementedException();
        }

        private bool _findIfWasDisbursed()
        {
            throw new NotImplementedException();
        }

        private bool _findIfWasRetrieved()
        {
            throw new NotImplementedException();
        }

        // Methods
        public List<RetrievalModel> getRetrievals(SSISEntities context)
        {
            throw new NotImplementedException();
        }

        public List<DisbursementModel> getDisbursements(SSISEntities context)
        {
            throw new NotImplementedException();
        }

        public List<AllocatedModel> getAllocated(SSISEntities context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ItemModel, int> getShortfall(DisbursementModel disbursement)
        {
            throw new NotImplementedException();
        }
        public Dictionary<ItemModel, int> getShortfall(AllocatedModel allocated)
        {
            throw new NotImplementedException();
        }
        public Dictionary<ItemModel, int> getShortfall(RetrievalModel retrieved)
        {
            throw new NotImplementedException();
        }

        // Properties
        public string Reason
        {
            get
            {
                return reason;
            }

            set
            {
                reason = value;
            }
        }

        public string RejectedReason
        {
            get
            {
                return rejectedReason;
            }

            set
            {
                rejectedReason = value;
            }
        }

        public bool WasRetrieved
        {
            get
            {
                return wasRetrieved;
            }

        }

        public bool WasDisbursed
        {
            get
            {
                return wasDisbursed;
            }

        }

        public bool WasAllocated
        {
            get
            {
                return wasAllocated;
            }

        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
