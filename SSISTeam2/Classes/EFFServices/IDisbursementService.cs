using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IDisbursementService
    {
        DisbursementModel findLatestPossibleDisbursingByRequestId(int requestId);
        DisbursementModel findLatestSignOffsByRequestId(int requestId, string currentUser);
        DisbursementModelCollection getAllPossibleDisbursementsForCollectionPoint(int collectionPointId);
        DisbursementModelCollection getAllSignOffsForCollectionPoint(int collectionPointId, string currentUser);
        DisbursementModelCollection getAllPossibleDisbursements();
        DisbursementModelCollection getAllThatCanBeSignedOff(string currentUser);
        DisbursementModelCollection getAllThatWereDisbursed();
    }
}