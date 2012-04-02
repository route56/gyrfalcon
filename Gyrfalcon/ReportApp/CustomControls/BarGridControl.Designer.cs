namespace ReportApp.CustomControls
{
	partial class BarGridControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.rankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timeSpanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.activityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.barGridControlDataFormatBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barGridControlDataFormatBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rankDataGridViewTextBoxColumn,
            this.timeSpanDataGridViewTextBoxColumn,
            this.activityDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.barGridControlDataFormatBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(352, 189);
			this.dataGridView1.TabIndex = 0;
			// 
			// rankDataGridViewTextBoxColumn
			// 
			this.rankDataGridViewTextBoxColumn.DataPropertyName = "Rank";
			this.rankDataGridViewTextBoxColumn.HeaderText = "Rank";
			this.rankDataGridViewTextBoxColumn.Name = "rankDataGridViewTextBoxColumn";
			this.rankDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// timeSpanDataGridViewTextBoxColumn
			// 
			this.timeSpanDataGridViewTextBoxColumn.DataPropertyName = "TimeSpan";
			this.timeSpanDataGridViewTextBoxColumn.HeaderText = "TimeSpan";
			this.timeSpanDataGridViewTextBoxColumn.Name = "timeSpanDataGridViewTextBoxColumn";
			this.timeSpanDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// activityDataGridViewTextBoxColumn
			// 
			this.activityDataGridViewTextBoxColumn.DataPropertyName = "Activity";
			this.activityDataGridViewTextBoxColumn.HeaderText = "Activity";
			this.activityDataGridViewTextBoxColumn.Name = "activityDataGridViewTextBoxColumn";
			this.activityDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// barGridControlDataFormatBindingSource
			// 
			this.barGridControlDataFormatBindingSource.DataSource = typeof(DataStore.RankedDataFormat);
			// 
			// BarGridControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView1);
			this.Name = "BarGridControl";
			this.Size = new System.Drawing.Size(352, 189);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barGridControlDataFormatBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn timeSpanDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn activityDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource barGridControlDataFormatBindingSource;
	}
}
