using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXDemo.Models;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;
using DotNet.Highcharts;
using System.Drawing;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Configuration;

namespace DXDemo.Controllers
{
    public class HomeController : Controller
    {
        Series[] seriesSet = null;
        SqlConnection con = null;

        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            return View(NorthwindDataProvider.GetCustomers());
        }

        public ActionResult Demo()
        {
            ChartProperty objChartProperty = new ChartProperty();
            objChartProperty.Title = "Northwind Customer Order Chart";
            objChartProperty.Subtitle = "Customer Details from Northwind";
            objChartProperty.xAxisTitle = "years";
            objChartProperty.yAxisTitle = "Number of Orders";
            objChartProperty.SeriesSet = GenerateChartModel.GetSeriesSet();
            objChartProperty.xAxisData = DataSource.Years;


            var ChartData = DataSource.EmployeeOrderCountList();
            //var allseries = DataSource.ToChartSeries(DataSource.Years,ChartData.Keys.ToArray(),ChartData.Values.);

            //Highcharts charts = GenerateChartModel.BasicLine(objChartProperty);
            //Highcharts charts = GenerateChartModel.BasicArea(objChartProperty);
            //Highcharts charts = GenerateChartModel.BasicBar(objChartProperty);
            Highcharts charts = GenerateChartModel.BasicColumn(objChartProperty);

            return View(charts);
        }

        //public static Series[] GetSeriesSet()
        //{


        //    for (int i = 0; i < 3; i++)
        //    {
        //        int j = i + 1;
        //        string name = "sample";
        //        seriesSet[i] = new Series();
        //        Random rnd = new Random();
        //        List<object> li = new List<object>();
        //        li.Add(rnd.Next(100).ToString());
        //        li.Add((rnd.Next(100) + i * 23).ToString());
        //        li.Add((rnd.Next(100) + i * 35).ToString());
        //        object[] obj = li.ToArray();
        //        name = name + j.ToString();
        //        seriesSet[i].Name = name;
        //        seriesSet[i].Data = new Data(obj);
        //    }
        //    return seriesSet;
        //}
        //[HttpPost]
        //public JsonResult Demo(List<String> values)
        //{


        //    string xAxis = values[0];
        //    string yAxis = values[1];
        //    string series = values[2];

        //    ChartObjects oChatObj = new ChartObjects();
        //    oChatObj.objSeriesList = NorthwindDataProvider.DB.Invoices.Select(s =>
        //    s.GetType().GetProperty(series).GetValue(s, null)
        //    ).ToArray();
        //    oChatObj.objxAxis = NorthwindDataProvider.DB.Invoices.Select(s =>
        //    s.GetType().GetProperty(xAxis).GetValue(s, null)
        //    ).ToArray();
        //    oChatObj.objyAxis = NorthwindDataProvider.DB.Invoices.Select(s =>
        //    s.GetType().GetProperty(yAxis).GetValue(s, null)
        //    ).ToArray();

        //    var DistinctSeriesList = oChatObj.objSeriesList.Distinct().ToArray();
        //    var DistinctxAxis = Array.ConvertAll<object, string>(oChatObj.objxAxis, ConvertObjectToString).Distinct().ToArray();
        //    var DistinctyAxis = Array.ConvertAll<object, string>(oChatObj.objyAxis, ConvertObjectToString).Distinct().ToArray();

        //    seriesSet = new Series[DistinctSeriesList.Count()];
        //    int i = 0;
        //    foreach (object DistinctSeries in DistinctSeriesList)
        //    {
        //        var dummyData = from a in NorthwindDataProvider.DB.Invoices
        //                        where a.ShipCity == DistinctSeries.ToString()
        //                        group a by a.Country into g
        //                        select new { Country = g.Key, Count = g.Count() };
        //        var DataDictionary = dummyData.ToDictionary(x => x.Country, x => x.Count);
        //        seriesSet[i] = new Series();
        //        seriesSet[i].Name = DistinctSeries.ToString();
        //        var xaxis = dummyData.Select(x => x.Country).ToString();
        //        var yaxis = dummyData.Select(y => y.Count).ToString();
        //        object[] obj = { xaxis, yaxis };
        //        seriesSet[i].Data = new Data(obj);
        //        i++;
        //    }

        //    //   NorthwindDataProvider.DB.Invoices.Select(s => s.OrderID).ToArray();



        //    ChartProperty objChartProperty = new ChartProperty();
        //    objChartProperty.Title = "Northwind Customer Order Chart";
        //    objChartProperty.Subtitle = "Customer Details from Northwind";
        //    objChartProperty.xAxisTitle = xAxis;
        //    objChartProperty.yAxisTitle = yAxis;
        //    objChartProperty.SeriesSet = seriesSet;
        //    objChartProperty.xAxisData = DistinctxAxis;
        //    Highcharts charts = GenerateChartModel.BasicLine(objChartProperty);

        //    // return  View(charts);
        //    // return View("DisplayChart", charts);
        //    object msg = null;


        //        msg = new
        //        {
        //            obj="success"
        //        };

        //    return Json(msg);

        //}

        [HttpPost]
        public ActionResult Demo(string xAxis, string yAxis, string series)
        {

            ChartObjects oChatObj = new ChartObjects();
            oChatObj.objSeriesList = NorthwindDataProvider.DB.Invoices.Select(s =>
            s.GetType().GetProperty(series).GetValue(s, null)
            ).ToArray();
            oChatObj.objxAxis = NorthwindDataProvider.DB.Invoices.Select(s =>
            s.GetType().GetProperty(xAxis).GetValue(s, null)
            ).ToArray();
            oChatObj.objyAxis = NorthwindDataProvider.DB.Invoices.Select(s =>
            s.GetType().GetProperty(yAxis).GetValue(s, null)
            ).ToArray();

            var DistinctSeriesList = oChatObj.objSeriesList.Distinct().ToArray();
            var DistinctxAxis = Array.ConvertAll<object, string>(oChatObj.objxAxis, ConvertObjectToString).Distinct().ToArray();
            var DistinctyAxis = Array.ConvertAll<object, string>(oChatObj.objyAxis, ConvertObjectToString).Distinct().ToArray();


        
             con = new SqlConnection(ConfigurationManager.ConnectionStrings["NWindConnectionString1"].ConnectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;   //This will specify that we are passing query from application
            string query = "select count(OrderDate) from Invoices where Country='Argentina' group by  ShipCity";
            cmd.CommandText = query;
            var sql = this.ExecuteSelectCommand(query, cmd.CommandType);
            
            seriesSet = new Series[DistinctSeriesList.Count()];
            int i = 0;
            foreach (object DistinctSeries in DistinctSeriesList)
            {
                var dummyData = from a in NorthwindDataProvider.DB.Invoices
                                where a.ShipCity == DistinctSeries.ToString()
                                group a by a.Country into g
                                select new { Country = g.Key, Count = g.Count() };
                var DataDictionary = dummyData.ToDictionary(x => x.Country, x => x.Count);
                seriesSet[i] = new Series();
                seriesSet[i].Name = DistinctSeries.ToString();
                var xaxis = dummyData.Select(x => x.Country).ToString();
                var yaxis = dummyData.Select(y => y.Count).ToString();
                object[] obj = { xaxis, yaxis };
                seriesSet[i].Data = new Data(obj);
                i++;
            }

            //   NorthwindDataProvider.DB.Invoices.Select(s => s.OrderID).ToArray();



            ChartProperty objChartProperty = new ChartProperty();
            objChartProperty.Title = "Northwind Customer Order Chart";
            objChartProperty.Subtitle = "Customer Details from Northwind";
            objChartProperty.xAxisTitle = xAxis;
            objChartProperty.yAxisTitle = yAxis;
            objChartProperty.SeriesSet = seriesSet;
            objChartProperty.xAxisData = DistinctxAxis;
            Highcharts charts = GenerateChartModel.BasicLine(objChartProperty);
            return View(charts);
        }

        public DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType)
        {
            SqlCommand cmd = null;
            DataTable table = new DataTable();

            cmd = con.CreateCommand();

            cmd.CommandType = cmdType;
            cmd.CommandText = CommandName;

            try
            {
                con.Open();

                SqlDataAdapter da = null;
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                con.Close();
            }

            return table;
        }

        

        string ConvertObjectToString(object obj)
        {
            return (obj == null) ? string.Empty : obj.ToString(); // FIXME
        }
        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }

    }
}