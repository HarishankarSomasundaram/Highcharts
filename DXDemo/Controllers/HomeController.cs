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
            
            
            var ChartData = DataSource.EmployeeOrderCountList();
            var allseries = DataSource.ToChartSeries(ChartData);


            Highcharts chart = new Highcharts("chart")
                 .InitChart(new Chart
                 {
                     DefaultSeriesType = ChartTypes.Line,
                     MarginRight = 130,
                     MarginBottom = 25,
                     ClassName = "chart"
                 })
                 .SetTitle(new Title
                 {
                     Text = "Monthly Average Temperature",
                     X = -20
                 })
                 .SetSubtitle(new Subtitle
                 {
                     Text = "Source: WorldClimate.com",
                     X = -20
                 })
                 .SetXAxis(new XAxis { Categories = DataSource.Years })
                 .SetYAxis(new YAxis
                 {
                     Title = new YAxisTitle { Text = "Total Orders Count" },
                     PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    }
                 })
                 .SetTooltip(new Tooltip
                 {
                     Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                 })
                 .SetLegend(new Legend
                 {
                     Layout = Layouts.Vertical,
                     Align = HorizontalAligns.Right,
                     VerticalAlign = VerticalAligns.Top,
                     X = -10,
                     Y = 100,
                     BorderWidth = 0
                 })
                 .SetSeries(new[] 
                {
                   new Series { Name = ChartData.FirstOrDefault().Key, Data = new Data(allseries) },
                }
                 );

            return View(chart);
        }

        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }
    
    }
}