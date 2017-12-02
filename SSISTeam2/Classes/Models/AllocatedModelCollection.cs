using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class AllocatedModelCollection : Collection<AllocatedModel>
    {
        public AllocatedModelCollection(IList<AllocatedModel> list) : base(list)
        {
        }

        public List<AllocatedModel> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public AllocatedModelCollection beforeDateInclusive(DateTime date)
        {
            List<AllocatedModel> result = Items.Where(x => x.getDate().CompareTo(date) <= 0).ToList();
            return new AllocatedModelCollection(result);
        }
        public AllocatedModelCollection afterDateInclusive(DateTime date)
        {
            List<AllocatedModel> result = Items.Where(x => x.getDate().CompareTo(date) >= 0).ToList();
            return new AllocatedModelCollection(result);
        }
        public AllocatedModelCollection betweenDatesInclusive(DateTime start, DateTime end)
        {
            List<AllocatedModel> result = Items
                    .Where(x => x.getDate().CompareTo(start) >= 0
                                && x.getDate().CompareTo(end) <= 0
                    ).ToList();
            return new AllocatedModelCollection(result);
        }
        public AllocatedModelCollection fromDepartment(string deptCode)
        {
            List<AllocatedModel> result = Items.Where(x => x.getDepartment().dept_code == deptCode).ToList();
            return new AllocatedModelCollection(result);
        }
        public AllocatedModelCollection fromDepartments(params string[] deptCodes)
        {
            List<AllocatedModel> result = Items.Where(x => deptCodes.Contains(x.getDepartment().dept_code)).ToList();
            return new AllocatedModelCollection(result);
        }
        public AllocatedModelCollection byUser(string username)
        {
            List<AllocatedModel> result = Items.Where(x => x.getUserModel().Username == username).ToList();
            return new AllocatedModelCollection(result);
        }
    }
}
