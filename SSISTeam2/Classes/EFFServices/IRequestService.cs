using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRequestService
    {
        RequestModelCollection getAllRequestsOfDepartment(string departmentCode);
        // Returns all requests by a department ever, with the pending/updated quantity filled in.
        RequestModelCollection getAllPendingRequestsOfDepartment(string departmentCode);
        // Returns all pending or updated requests by a department ever,
        // with the pending/updated quantity filled in.
        RequestModel findRequestById(int requestId);
        // Returns a request, with the pending/updated quantity filled in.
        RequestModelCollection getAllRequestsByUser(string username);
        // Returns all requests made by a user, with the pending/updated quantity filled in.
        RequestModelCollection getAllApprovedRequests();
        // Returns all requests that have been marked as approved.
        bool saveNewRequest(RequestModel request);
        // Save a new request. Make sure to provide items and quantities.
        // And use your own ctx.SaveChanges()
        bool updateRequestChanges(RequestModel request);
        // Save a new request. This will update based on the request argument's RequestId.
        // And use your own ctx.SaveChanges()
        bool approveRequest(RequestModel request, string currentUser);
        // Add approve records for a list of requests, and changes current_status.
        // Will use the quantities supplied in the requests arguments.
        // And use your own ctx.SaveChanges()
        bool rejectRequest(RequestModel request, string currentUser);
        // Same as approve requests, but for rejection.
   
        bool setRequestToCancelled(int requestId, string username);
        //RequestDTOCollection getAllAllocatedRequests();
        //RequestDTOCollection getAllPartiallyAllocatedRequests();
        //RequestDTOCollection getAllRequestsDisbursing();
        //RequestDTOCollection getAllDisbursedRequests();
    }
}
