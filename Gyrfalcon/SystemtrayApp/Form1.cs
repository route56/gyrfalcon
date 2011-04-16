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
	//    public partial class Form1 : Form
	//    {
	//        public Form1()
	//        {
	//            InitializeComponent();
	//        }
	//    }
	//}
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			Use_Notify(); // Setting up all Property of Notifyicon 

		}

		private void Use_Notify()
		{
			notifyIcon1.ContextMenuStrip = contextMenuStrip1;
			notifyIcon1.BalloonTipText = "GyrFalcon running...";
			notifyIcon1.BalloonTipTitle = "GyrFalcon";
			notifyIcon1.ShowBalloonTip(1);
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			// Hide The Form when it's minimized

			if (FormWindowState.Minimized == WindowState)
				Hide();
		}
		private void MyNotify_DoubleClick(object sender, System.EventArgs e)
		{
			// Show the form when Dblclicked on Notifyicon

			Show();
			WindowState = FormWindowState.Normal;
		}
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Will Close Your Application 

			notifyIcon1.Dispose();
			Application.Exit();
		}

		private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Will Restore Your Application 

			Show();
			WindowState = FormWindowState.Normal;
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Settings 1

			MessageBox.Show("Your Application Settings 1");
		}
	}
}

//private void settings2ToolStripMenuItem_Click(object sender, EventArgs e)
//{
////Settings 2