using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SSISTeam2.Classes.Models
{
    public class RetrievalModelCollection : Collection<RetrievalModel>
    {
        public RetrievalModelCollection(IList<RetrievalModel> list) : base(list)
            {
        }

        public List<RetrievalModel> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public RetrievalModelCollection beforeDateInclusive(DateTime date)
        {
            List<RetrievalModel> result = Items.Where(x => x.getDate().CompareTo(date) <= 0).ToList();
            return new RetrievalModelCollection(result);
        }
        public RetrievalModelCollection afterDateInclusive(DateTime date)
        {
            List<RetrievalModel> result = Items.Where(x => x.getDate().CompareTo(date) >= 0).ToList();
            return new RetrievalModelCollection(result);
        }
        public RetrievalModelCollection betweenDatesInclusive(DateTime start, DateTime end)
        {
            List<RetrievalModel> result = Items
                    .Where(x => x.getDate().CompareTo(start) >= 0
                                && x.getDate().CompareTo(end) <= 0
                    ).ToList();
            return new RetrievalModelCollection(result);
        }
        public RetrievalModelCollection fromDepartment(string deptCode)
        {
            List<RetrievalModel> result = Items.Where(x => x.getDepartment().dept_code == deptCode).ToList();
            return new RetrievalModelCollection(result);
        }
        public RetrievalModelCollection fromDepartments(params string[] deptCodes)
        {
            List<RetrievalModel> result = Items.Where(x => deptCodes.Contains(x.getDepartment().dept_code)).ToList();
            return new RetrievalModelCollection(result);
        }
        public RetrievalModelCollection byUser(string username)
        {
            List<RetrievalModel> result = Items.Where(x => x.getUserModel().Username == username).ToList();
            return new RetrievalModelCollection(result);
        }
    }
}