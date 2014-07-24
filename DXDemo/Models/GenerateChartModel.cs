using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;
using DotNet.Highcharts;
using System.Drawing;

namespace DXDemo.Models
{
    public class GenerateChartModel
    {

        public static Series[] GetSeriesSet()
        {
            Series[] seriesSet = new Series[3];
           
            for (int i = 0; i < 3; i++)
            {
                int j = i + 1;
                string name = "sample";
                seriesSet[i] = new Series();
                Random rnd = new Random();
                List<object> li = new List<object>();
                li.Add(rnd.Next(100).ToString());
                li.Add((rnd.Next(100)+i*23).ToString());
                li.Add((rnd.Next(100)+i*35).ToString());
                object[] obj = li.ToArray();
                name = name + j.ToString();
                seriesSet[i].Name = name;
                seriesSet[i].Data = new Data(obj);
            }
            return seriesSet;
        }

        public static Highcharts BasicLine(ChartProperty objChartProperty)
        {
            //Series[] SeriesSet = GetSeriesSet();
            //object[] data = DataDictionary.Values.FirstOrDefault().ToArray();
         
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
                   Text = "Northwind User Order Details",
                   X = -20
               })
               .SetSubtitle(new Subtitle
               {
                   Text = "Source: Northwind",
                   X = -20
               })
               .SetXAxis(new XAxis { Categories = objChartProperty.xAxisData })

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
                                    this.x +': '+ this.y ;
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
               .SetSeries( 
                
                   //new Series { Name = DataDictionary.FirstOrDefault().Key, Data = new Data( data )}
                 objChartProperty.SeriesSet
                
               );
            return chart;
        }

        public static Highcharts BasicArea(ChartProperty objChartProperty)
        {
           
            
            Highcharts chart = new Highcharts("chart")
             .InitChart(new Chart { DefaultSeriesType = ChartTypes.Area })
             .SetTitle(new Title { Text = objChartProperty.Title })
             .SetSubtitle(new Subtitle { Text = objChartProperty.Subtitle})
                // Labels = new XAxisLabels { Formatter = "function() { return this.value;  }" }
             .SetXAxis(new XAxis {  Categories = objChartProperty.xAxisData })
             .SetYAxis(new YAxis
             {
                 Title = new YAxisTitle { Text = objChartProperty.yAxisTitle },
                 PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    },
                 Labels = new YAxisLabels { Formatter = "function() { return this.value; }" }
             })
             .SetPlotOptions(new PlotOptions
             {
                 Area = new PlotOptionsArea
                 {  
                    
                     Marker = new PlotOptionsAreaMarker
                     {
                         Enabled = false,
                         Symbol = "circle",
                         Radius = 2,
                         States = new PlotOptionsAreaMarkerStates
                         {
                             Hover = new PlotOptionsAreaMarkerStatesHover { Enabled = true }
                         }
                     }
                 }
             })
             .SetSeries(objChartProperty.SeriesSet);

            return chart;
        }

        public static Highcharts BasicBar(ChartProperty objChartProperty)
        {
           

            Highcharts chart = new Highcharts("chart")
                  .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
                  .SetTitle(new Title { Text = objChartProperty.Title})
                  .SetSubtitle(new Subtitle { Text = objChartProperty.Subtitle })
                  .SetXAxis(new XAxis
                  {
                      Categories = objChartProperty.xAxisData,
                      Title = new XAxisTitle { Text = objChartProperty.xAxisTitle }
                  })
                  .SetYAxis(new YAxis
                  {
                      Min = 0,
                      Title = new YAxisTitle
                      {
                          Text = objChartProperty.yAxisTitle,
                          Align = AxisTitleAligns.High
                      }
                  })
                  .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y ; }" })
                  .SetPlotOptions(new PlotOptions
                  {
                      Bar = new PlotOptionsBar
                      {
                          DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                      }
                  })
                  .SetLegend(new Legend
                  {
                      Layout = Layouts.Vertical,
                      Align = HorizontalAligns.Right,
                      VerticalAlign = VerticalAligns.Top,
                      X = -100,
                      Y = 100,
                      Floating = true,
                      BorderWidth = 1,
                      BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                      Shadow = true
                  })
                  .SetCredits(new Credits { Enabled = false })
                  .SetSeries(
                
                    objChartProperty.SeriesSet
                );

            return chart;
        }

        public static Highcharts BasicColumn(ChartProperty objChartProperty)
        {
           // Series[] SeriesSet = GetSeriesSet();

            Highcharts chart = new Highcharts("chart")
                          .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                          .SetTitle(new Title { Text = objChartProperty.Title })
                          .SetSubtitle(new Subtitle { Text = objChartProperty.Subtitle })
                          .SetXAxis(new XAxis { Categories = objChartProperty.xAxisData})
                          .SetYAxis(new YAxis
                          {
                              Min = 0,
                              Title = new YAxisTitle { Text = objChartProperty.yAxisTitle}
                          })
                          .SetLegend(new Legend
                          {
                              Layout = Layouts.Vertical,
                              Align = HorizontalAligns.Left,
                              VerticalAlign = VerticalAligns.Top,
                              X = 100,
                              Y = 70,
                              Floating = true,
                              BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                              Shadow = true
                          })
                          .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y; }" })
                          .SetPlotOptions(new PlotOptions
                          {
                              Column = new PlotOptionsColumn
                              {
                                  PointPadding = 0.2,
                                  BorderWidth = 0
                              }
                          })
                          .SetSeries(objChartProperty.SeriesSet
                );


            return chart;
        }
    }
}