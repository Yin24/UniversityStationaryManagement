using System.Collections.Generic;
using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRequestMovementService
    {
        //List<string> getRequestNonAllocatedItemCodes(int requestId);
        void allocateRequest(int requestId, string currentUser);
        void moveFromAllocatedToRetrieving(int requestId, List<string> itemCodes, string currentUser);
        void moveFromRetrievingToRetrieved(int requestId, Dictionary<string, int> items, string currentUser);
        //void moveFromRetrievingToRetrieved(int requestId, List<string> itemCodes, string currentUser);
        void moveFromRetrievedToDisbursing(int requestId, List<string> itemCodes, string currentUser);
        void moveFromDisbursingToDisbursed(int requestId, Dictionary<string, int> items, string currentUser);
        //void moveFromDisbursingToDisbursed(int requestId, List<string> itemCodes, string currentUser);

    }
}