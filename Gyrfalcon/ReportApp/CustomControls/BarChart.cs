using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace ReportApp.CustomControls
{
	class BarChart
	{
		internal Chart NewChart()
		{
			return new Chart();
		}

		/// <summary>
		/// Disables vertical grid
		/// Shows area
		/// Shows stacked, multiple series
		/// Legend shown
		/// </summary>
		/// <returns></returns>
		internal Chart AreaChart()
		{
			var chart1 = new Chart();

			// Create Chart Area
			ChartArea chartArea1 = new ChartArea();

			chartArea1.AxisX.MinorGrid.Enabled = false;
			chartArea1.AxisX.MajorGrid.Enabled = false;

			chartArea1.AxisX.Interval = 1;
			chartArea1.AxisX.IsLabelAutoFit = true;
			chartArea1.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30 | LabelAutoFitStyles.LabelsAngleStep45 | LabelAutoFitStyles.LabelsAngleStep90;

			chartArea1.AxisY.LabelStyle.Format = "{0} h";

			chartArea1.AxisX.IsMarginVisible = false;

			chart1.ChartAreas.Add(chartArea1);

			Random rand = new Random();
			// Create a data series
			Series series1 = GetSeries(rand);
			series1.LegendText = "Visual studio";
			// Add series to the chart
			chart1.Series.Add(series1);
			Series series2 = GetSeries(rand);
			series2.LegendText = "Chrome";
			chart1.Series.Add(series2);
			Series series3 = GetSeries(rand);
			series3.LegendText = "apps.facebook.com";
			chart1.Series.Add(series3);

			// Legend show
			chart1.Legends.Add(new Legend() { Name = "Visual studio", Docking = Docking.Bottom });
			chart1.Legends.Add(new Legend() { Name = "Chrome", Docking = Docking.Bottom });
			chart1.Legends.Add(new Legend() { Name = "apps.facebook.com", Docking = Docking.Bottom });

			// Set chart control location
			chart1.Location = new System.Drawing.Point(16, 48);

			// Set Chart control size
			chart1.Size = new System.Drawing.Size(500, 400);//(360, 260);
			return chart1;
		}

		private static Series GetSeries(Random rand)
		{
			Series series1 = new Series();

			series1.ChartType = SeriesChartType.StackedArea;

			// Add data points to the first series
			series1.Points.Add(new DataPoint() { AxisLabel = "Sun, Mar 11", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Mon, Mar 12", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Tue, Mar 13", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Wed, Mar 14", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Thu, Mar 15", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Fri, Mar 16", YValues = new double[] { rand.Next(10, 90) } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Sat, Mar 17", YValues = new double[] { rand.Next(10, 90) } });
			return series1;
		}

		/// <summary>
		/// POC of basic dashboard rendering.
		/// labeling of y axis to h
		/// Showing text x axis below
		/// Coloring of bars
		/// bar chart
		/// fitting of text
		/// </summary>
		/// <returns></returns>
		internal Chart Dashboard()
		{
			var chart1 = new Chart();

			// Create Chart Area
			ChartArea chartArea1 = new ChartArea();

			chartArea1.AxisX.MinorGrid.Enabled = false;
			chartArea1.AxisX.MajorGrid.Enabled = false;

			//chartArea1.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90;
			//chartArea1.AxisX.LabelStyle.Angle = 30;
			chartArea1.AxisX.Interval = 1;
			chartArea1.AxisX.IsLabelAutoFit = true;
			chartArea1.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30 | LabelAutoFitStyles.LabelsAngleStep45 | LabelAutoFitStyles.LabelsAngleStep90;

			chartArea1.AxisY.LabelStyle.Format = "{0} h";
			//chartArea1.AxisX.CustomLabels.Add(
			// Add Chart Area to the Chart
			chart1.ChartAreas.Add(chartArea1);

			// Create a data series
			Series series1 = new Series();

			var foo = new DataPoint();
			// Add data points to the first series
			series1.Points.Add(new DataPoint() { AxisLabel = "Visual Studio", YValues = new double[] { 60 }, Color = Color.Red });
			series1.Points.Add(new DataPoint() { AxisLabel = "Chrome", YValues = new double[] { 30 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Dos", YValues = new double[] { 15 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Word", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Notepad", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "MS Excel", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "MS Outlook", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "Google talk", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "somesite.com", YValues = new double[] { 10 } });
			series1.Points.Add(new DataPoint() { AxisLabel = "apps.facebook.com", YValues = new double[] { 10 } });

			//series1.Points.Add(60);
			//series1.Points.Add(50);
			//series1.Points.Add(30);
			//series1.Points.Add(20);
			//series1.Points.Add(10);
			//series1.Points.Add(9);
			//series1.Points.Add(8);
			//series1.Points.Add(6);
			//series1.Points.Add(2);
			//series1.Points.Add(1);

			// Add series to the chart
			chart1.Series.Add(series1);

			// Set chart control location
			chart1.Location = new System.Drawing.Point(16, 48);

			// Set Chart control size
			chart1.Size = new System.Drawing.Size(360, 260);
			return chart1;
		}

		internal Chart TestedOnceAndWorks()
		{
			var chart1 = new Chart();

			// Create Chart Area
			ChartArea chartArea1 = new ChartArea();

			// Add Chart Area to the Chart
			chart1.ChartAreas.Add(chartArea1);

			// Create a data series
			Series series1 = new Series();
			Series series2 = new Series();

			// Add data points to the first series
			series1.Points.Add(34);
			series1.Points.Add(24);
			series1.Points.Add(32);
			series1.Points.Add(28);
			series1.Points.Add(44);

			// Add data points to the second series
			series2.Points.Add(1);
			series2.Points.Add(5);
			series2.Points.Add(10);
			series2.Points.Add(100);
			series2.Points.Add(50);

			// Add series to the chart
			chart1.Series.Add(series1);
			chart1.Series.Add(series2);

			// Set chart control location
			chart1.Location = new System.Drawing.Point(16, 48);

			// Set Chart control size
			chart1.Size = new System.Drawing.Size(360, 260);
			return chart1;
		}
	}

}

