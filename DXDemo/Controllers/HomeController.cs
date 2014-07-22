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

namespace DXDemo.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }

    }
}