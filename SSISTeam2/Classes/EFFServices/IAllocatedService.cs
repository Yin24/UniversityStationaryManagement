using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IAllocatedService
    {
        //AllocatedModelCollection getAllAllocatedForCollectionPoint(int collectionPointId);
        //AllocatedModelCollection getAllAllocatedFromDepartment(string deptCode);
        AllocatedModelCollection getAllAllocated();
        AllocatedModel findLatestAllocatedByRequestId(int requestId);
        //int allocateRequest(RequestModel toAllocate, string currentUser);
        //int reAllocateRequest(RequestModel toAllocate, string currentUser);
    }
}
