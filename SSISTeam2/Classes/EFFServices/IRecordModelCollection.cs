//using System;
//using System.Collections.Generic;

//namespace SSISTeam2.Classes.Models
//{
//    public interface IRecordModelCollection<T>
//    {
//        AllocatedModelCollection<T> afterDateInclusive(DateTime date);
//        AllocatedModelCollection<T> beforeDateInclusive(DateTime date);
//        AllocatedModelCollection<T> betweenDatesInclusive(DateTime start, DateTime end);
//        AllocatedModelCollection<T> byUser(string username);
//        AllocatedModelCollection<T> fromDepartment(string deptCode);
//        AllocatedModelCollection<T> fromDepartments(params string[] deptCodes);
//        List<T> getPageList(int currentPage, int perPageNumber);
//    }
//}