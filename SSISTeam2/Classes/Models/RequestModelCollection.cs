using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RequestModelCollection : Collection<RequestModel>
    {
        public RequestModelCollection(List<RequestModel> records) : base(records)
        {
        }

        public List<RequestModel> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public RequestModelCollection beforeDateInclusive(DateTime date)
        {
            List<RequestModel> result = Items.Where(x => x.getDate().CompareTo(date) <= 0).ToList();
            return new RequestModelCollection(result);
        }
        public RequestModelCollection afterDateInclusive(DateTime date)
        {
            List<RequestModel> result = Items.Where(x => x.getDate().CompareTo(date) >= 0).ToList();
            return new RequestModelCollection(result);
        }
        public RequestModelCollection betweenDatesInclusive(DateTime start, DateTime end)
        {
            List<RequestModel> result = Items
                    .Where(x => x.getDate().CompareTo(start) >= 0
                                && x.getDate().CompareTo(end) <= 0
                    ).ToList();
            return new RequestModelCollection(result);
        }
        public RequestModelCollection fromDepartment(string deptCode)
        {
            List<RequestModel> result = Items.Where(x => x.getDepartment().dept_code == deptCode).ToList();
            return new RequestModelCollection(result);
        }
        public RequestModelCollection fromDepartments(params string[] deptCodes)
        {
            List<RequestModel> result = Items.Where(x => deptCodes.Contains(x.getDepartment().dept_code)).ToList();
            return new RequestModelCollection(result);
        }
        public RequestModelCollection byUser(string username)
        {
            List<RequestModel> result = Items.Where(x => x.getUserModel().Username == username).ToList();
            return new RequestModelCollection(result);
        }
        public List<RequestModel> withStatuses(params string[] status)
        {
            throw new NotImplementedException();
        }
    }
}
