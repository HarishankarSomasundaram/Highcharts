using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;
using DotNet.Highcharts;

namespace DXDemo.Models
{
    public class ChartProperty
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string xAxisTitle { get; set; }
        public string yAxisTitle { get; set; }
        public Series[] SeriesSet { get; set; }
        public string[] xAxisData { get; set; }

    }
    public class PieChartProperty
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Data[] PieData { get; set; }
    }
    public class ChartObjects
    {
        public object[] objxAxis { get; set; }
        public object[] objyAxis { get; set; }
        public object[] objSeriesList { get; set; }
    }
    public class ChartAxis
    {
        public class User
        {
            public string AxisX { get; set; }
            public string AxisY { get; set; }
            public string Series { get; set; }
        }
    }
}