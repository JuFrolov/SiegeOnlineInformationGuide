namespace SiegeOnlineDataViewer
{
	partial class frmMaps
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaps));
			this.pictMap = new System.Windows.Forms.PictureBox();
			this.cmbMaps = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictMap)).BeginInit();
			this.SuspendLayout();
			// 
			// pictMap
			// 
			this.pictMap.BackColor = System.Drawing.Color.Transparent;
			this.pictMap.Location = new System.Drawing.Point(12, 33);
			this.pictMap.Name = "pictMap";
			this.pictMap.Size = new System.Drawing.Size(505, 436);
			this.pictMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictMap.TabIndex = 1;
			this.pictMap.TabStop = false;
			// 
			// cmbMaps
			// 
			this.cmbMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbMaps.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cmbMaps.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMaps.FormattingEnabled = true;
			this.cmbMaps.Location = new System.Drawing.Point(121, 6);
			this.cmbMaps.MaxDropDownItems = 25;
			this.cmbMaps.Name = "cmbMaps";
			this.cmbMaps.Size = new System.Drawing.Size(396, 21);
			this.cmbMaps.Sorted = true;
			this.cmbMaps.TabIndex = 2;
			this.cmbMaps.SelectedIndexChanged += new System.EventHandler(this.cmbMaps_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Карты территорий:";
			// 
			// frmMaps
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(529, 481);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbMaps);
			this.Controls.Add(this.pictMap);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMaps";
			this.Text = "Осада Онлайн: карта территорий";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMaps_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictMap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictMap;
		private System.Windows.Forms.ComboBox cmbMaps;
		private System.Windows.Forms.Label label1;

	}
}