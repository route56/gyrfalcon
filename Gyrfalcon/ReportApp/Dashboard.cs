using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MockGyrfalconService;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReportApp
{
	public partial class Dashboard : Form
	{
		public Dashboard()
		{
			InitializeComponent();

			// this.Controls.AddRange(new TimeSpanPicker().NewTimeSpan());

			//this.Controls.AddRange(new System.Windows.Forms.Control[] { new GridData().NewGrid() });

			//this.Controls.AddRange(new System.Windows.Forms.Control[] { new BarChart().NewChart() });
		}
	}
}
