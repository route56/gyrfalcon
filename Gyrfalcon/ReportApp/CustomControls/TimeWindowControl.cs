﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportApp.CustomControls
{
	public partial class TimeWindowControl : UserControl
	{
		private TimeWindowCore _timeWindow;

		public DateTime Start 
		{ 
			get 
			{ 
				return _timeWindow.StartTime; 
			} 
			set 
			{ 
				_timeWindow = new TimeWindowCore(value);
				SetTimeWindowLabel();
			} 
		}

		public DateTime End { get { return _timeWindow.EndTime; } }

		public TimeWindowControl()
			: this(DateTime.Now)
		{
		}

		public TimeWindowControl(DateTime start)
		{
			InitializeComponent();
			_timeWindow = new TimeWindowCore(start);
			SetTimeWindowLabel();
		}

		private void SetTimeWindowLabelAndRaiseEvent()
		{
			SetTimeWindowLabel();

			if (TimeWindowChanged != null)
			{
				TimeWindowChanged(this, new EventArgs());
			}
		}

		private void SetTimeWindowLabel()
		{
			lblTimeWindow.Text = string.Format("{0} - {1}", _timeWindow.StartTime.ToShortDateString(), _timeWindow.EndTime.ToShortDateString());
		}

		public override void Refresh()
		{
			base.Refresh();
			SetTimeWindowLabel();
		}

		public event EventHandler TimeWindowChanged;

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			_timeWindow.GoPrevious();
			SetTimeWindowLabelAndRaiseEvent();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			_timeWindow.GoNext();
			SetTimeWindowLabelAndRaiseEvent();
		}

		private void linkLabelYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_timeWindow.GoYear();
			SetTimeWindowLabelAndRaiseEvent();
		}

		private void linkLabelMonth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_timeWindow.GoMonth();
			SetTimeWindowLabelAndRaiseEvent();
		}

		private void linkLabelWeek_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_timeWindow.GoWeek();
			SetTimeWindowLabelAndRaiseEvent();
		}

		private void linkLabelDay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_timeWindow.GoDay();
			SetTimeWindowLabelAndRaiseEvent();
		}
	}
}
