using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRetrievalService
    {
        RetrievalModel findLatestRetrievingByRequestId(int requestId, string currentUser);
        //RetrievalModelCollection getAllRetrieved();
        //RetrievalModelCollection getAllRetrievedForCollectionPoint(int collectionPointId);
        //RetrievalModelCollection getAllRetrievedFromDepartment(string deptCode);
        RetrievalModelCollection getAllRetrievingByClerk(string currentUser);
    }
}