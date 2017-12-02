using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Models
{
    public class DisbursementModelCollection : Collection<DisbursementModel>
    {
        public DisbursementModelCollection(IList<DisbursementModel> list) : base(list)
            {
        }

        public List<DisbursementModel> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public DisbursementModelCollection beforeDateInclusive(DateTime date)
        {
            List<DisbursementModel> result = Items.Where(x => x.getDate().CompareTo(date) <= 0).ToList();
            return new DisbursementModelCollection(result);
        }
        public DisbursementModelCollection afterDateInclusive(DateTime date)
        {
            List<DisbursementModel> result = Items.Where(x => x.getDate().CompareTo(date) >= 0).ToList();
            return new DisbursementModelCollection(result);
        }
        public DisbursementModelCollection betweenDatesInclusive(DateTime start, DateTime end)
        {
            List<DisbursementModel> result = Items
                    .Where(x => x.getDate().CompareTo(start) >= 0
                                && x.getDate().CompareTo(end) <= 0
                    ).ToList();
            return new DisbursementModelCollection(result);
        }
        public DisbursementModelCollection fromDepartment(string deptCode)
        {
            List<DisbursementModel> result = Items.Where(x => x.getDepartment().dept_code == deptCode).ToList();
            return new DisbursementModelCollection(result);
        }
        public DisbursementModelCollection fromDepartments(params string[] deptCodes)
        {
            List<DisbursementModel> result = Items.Where(x => deptCodes.Contains(x.getDepartment().dept_code)).ToList();
            return new DisbursementModelCollection(result);
        }
        public DisbursementModelCollection byUser(string username)
        {
            List<DisbursementModel> result = Items.Where(x => x.getUserModel().Username == username).ToList();
            return new DisbursementModelCollection(result);
        }

        public DisbursementModelCollection forCollectionPoint(int collectionPtId)
        {
            List<DisbursementModel> result = Items.Where(x => x.CollectionPtId == collectionPtId).ToList();
            return new DisbursementModelCollection(result);
        }
    }
}