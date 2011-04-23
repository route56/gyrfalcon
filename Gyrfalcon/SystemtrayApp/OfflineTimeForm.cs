using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemtrayApp
{
	public partial class OfflineTimeForm : Form
	{
		public OfflineTimeForm()
		{
			InitializeComponent();
			
		}

		protected override CreateParams CreateParams
		{

			get
			{

				CreateParams param = base.CreateParams;

				param.ClassStyle = param.ClassStyle | 0x200;

				return param;

			}

		}

		private void button8_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
