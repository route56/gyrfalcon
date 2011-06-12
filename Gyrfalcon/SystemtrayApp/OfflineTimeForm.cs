using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopClient;
using System.Diagnostics;

namespace SystemtrayApp
{
	public partial class OfflineTimeForm : Form
	{
		private OfflineDialogInterface _connector;
		private List<string> _buttonsText;
		private int _buttonNumberSelected;

		public OfflineTimeForm()
		{
			InitializeComponent();

			_connector = new OfflineDialogInterface();

			_buttonsText = _connector.GetButtonText();

			Debug.Assert(_buttonsText.Count == 6, "GetButtonText returned count != 6");

			// any better way to do this
			this.button1.Text = _buttonsText[0];
			this.button2.Text = _buttonsText[1];
			this.button3.Text = _buttonsText[2];
			this.button4.Text = _buttonsText[3];
			this.button5.Text = _buttonsText[4];
			this.button6.Text = _buttonsText[5];

			_DebugDisableAlwaysOnTop();
		}

		[Conditional("DEBUG")]
		private void _DebugDisableAlwaysOnTop()
		{
			this.TopMost = false;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			// TODOL get datetime here.
			_connector.Submit(_buttonsText[_buttonNumberSelected], this.textBox1.Text, DateTime.Now, DateTime.Now);
			this.Close();
		}

		private void _HideDontSubmitAndShowSubCommentFor(int buttonNumber)
		{
			this._buttonNumberSelected = buttonNumber - 1;
			this.button7.Visible = false;
			this.textBox1.Text = "Optional details for this " + _buttonsText[_buttonNumberSelected];
			this.textBox1.Visible = true;
			this.button8.Visible = true;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			// Ignore whole thing and exit.
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(2);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(3);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(4);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(5);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			_HideDontSubmitAndShowSubCommentFor(6);
		}
	}
}
