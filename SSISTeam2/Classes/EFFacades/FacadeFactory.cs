using SSISTeam2.Classes.EFFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.EFFacades
{
    public class FacadeFactory
    {
        public static IRequestService getRequestService(SSISEntities context)
        {
            return new RequestService(context);
        }
        public static IAllocatedService getAllocatedService(SSISEntities context)
        {
            return new AllocatedService(context);
        }
        public static IRetrievalService getRetrievalService(SSISEntities context)
        {
            return new RetrievalService(context);
        }
        public static IDisbursementService getDisbursementService(SSISEntities context)
        {
            return new DisbursementService(context);
        }
        public static IRequestMovementService getRequestMovementService(SSISEntities context)
        {
            return new RequestMovementService(context);
        }

        public static ITenderListService GetTenderListService()
        {
            return new TenderListService();
        }
    }
}