using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;


namespace DXDemo.Models
{
    // DXCOMMENT: Configure a data model (In this sample, we do this in file NorthwindDataProvider.cs. You would better create your custom file for a data model.)
    public static class NorthwindDataProvider
    {
        const string NorthwindDataContextKey = "DXNorthwindDataContext";

        public static NorthwindDataContext DB
        {
            get
            {
                if (HttpContext.Current.Items[NorthwindDataContextKey] == null)
                    HttpContext.Current.Items[NorthwindDataContextKey] = new NorthwindDataContext();
                return (NorthwindDataContext)HttpContext.Current.Items[NorthwindDataContextKey];
            }
        }

        public static IEnumerable GetCustomers()
        {
            return from customer in DB.Customers select customer;

        }
        public static List<Customer> CustomerList()
        {

            return DB.Customers.ToList();
        }
        public static int?[] ListfromDatabase()
        {

            var Employeelist = (from o in DB.Orders

                                select o.EmployeeID).Distinct().ToArray();

            //var activemembers = (from c in DB.Customers
            //                     where c.City == "London"
            //                     select c).Count();
            //var Data = new object[] { paidmembers, activemembers };
            return Employeelist;
        }
    }
    public static class DataSource
    {
        public static string[] Years = { "1996", "1997", "1998" };

        public static Dictionary<string, object[]> EmployeeOrderCountList()
        {
            Dictionary<string, object[]> employeeDetail = new Dictionary<string, object[]>();
            var list = NorthwindDataProvider.ListfromDatabase();
            int Count;
            List<string> yearList = null;

            foreach (int empID in list)
            {
                yearList = new List<string>();
                foreach (string year in Years)
                {
                    Count = (from o in NorthwindDataProvider.DB.Orders
                             where (o.OrderDate.Value.Year.ToString() == year) && (o.EmployeeID == empID)
                             select empID).Count();
                    yearList.Add(Count.ToString());
                }

                employeeDetail.Add(empID.ToString(), yearList.ToArray());
                yearList = null;
            }

            return employeeDetail;
        }
        public static object[] PieDataSource()
        {
            //var list = NorthwindDataProvider.ListfromDatabase();
            // List<string> yearList = null;
            // foreach (int empID in list)
            // {
            //     yearList=new List<string>();
            //     foreach (string year in Years)
            //     {
            //         Count = (from o in NorthwindDataProvider.DB.Orders
            //                  where (o.OrderDate.Value.Year.ToString() == year) && (o.EmployeeID == empID)
            //                  select empID).Count();
            //         yearList.Add(Count.ToString());
            //     }

            //     employeeDetail.Add(empID.ToString(), yearList.ToArray());
            //     yearList = null;
            // }
            return null;
        }

        //    public static object[] ToChartSeries(Dictionary<string, string> data)
        //    {
        //        var returnObject = new List<object>();

        //        foreach (var item in data)
        //        {
        //            returnObject.Add(new object[] { item.Key, item.Value });
        //        }

        //        return returnObject.ToArray();
        //    }
        //}


    }
}