using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportApp.CustomControls
{
	class Data
	{
		public int Rank { get; set; }
		public string TimeSpan { get; set; }
		public string Activity { get; set; } // click define
	}

	class GridData
	{
		internal DataGridView NewGrid()
		{
			var dataGridView1 = new DataGridView();
			Random rand = new Random();

			List<Data> dataList = new List<Data>();

			dataList.Add(new Data() { Rank = 1, TimeSpan = "10h 20m", Activity = "Visual studio" });
			dataList.Add(new Data() { Rank = 2, TimeSpan = "5h 10m", Activity = "apps.facebook.com" });
			dataList.Add(new Data() { Rank = 1, TimeSpan = "2h 11m", Activity = "Chrome" });

			dataGridView1.DataSource = dataList;

			dataGridView1.Location = new System.Drawing.Point(16, 48);

			dataGridView1.Size = new System.Drawing.Size(360, 260);
			return dataGridView1;
		}
	}
}
