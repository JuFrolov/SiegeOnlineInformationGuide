namespace SiegeOnlineKeyMaker
{
	partial class frmMain
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
			this.txtSystemInfo = new System.Windows.Forms.TextBox();
			this.txtHash = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnGetSysInfo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtSystemInfo
			// 
			this.txtSystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSystemInfo.Location = new System.Drawing.Point(12, 12);
			this.txtSystemInfo.Multiline = true;
			this.txtSystemInfo.Name = "txtSystemInfo";
			this.txtSystemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSystemInfo.Size = new System.Drawing.Size(413, 276);
			this.txtSystemInfo.TabIndex = 0;
			// 
			// txtHash
			// 
			this.txtHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtHash.Location = new System.Drawing.Point(431, 12);
			this.txtHash.Multiline = true;
			this.txtHash.Name = "txtHash";
			this.txtHash.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtHash.Size = new System.Drawing.Size(283, 276);
			this.txtHash.TabIndex = 0;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerate.Location = new System.Drawing.Point(305, 294);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(120, 23);
			this.btnGenerate.TabIndex = 1;
			this.btnGenerate.Text = "MD5 ->";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnGetSysInfo
			// 
			this.btnGetSysInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGetSysInfo.Location = new System.Drawing.Point(12, 294);
			this.btnGetSysInfo.Name = "btnGetSysInfo";
			this.btnGetSysInfo.Size = new System.Drawing.Size(120, 23);
			this.btnGetSysInfo.TabIndex = 1;
			this.btnGetSysInfo.Text = "Систем.Инфо";
			this.btnGetSysInfo.UseVisualStyleBackColor = true;
			this.btnGetSysInfo.Click += new System.EventHandler(this.btnGetSysInfo_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 326);
			this.Controls.Add(this.btnGetSysInfo);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.txtHash);
			this.Controls.Add(this.txtSystemInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmMain";
			this.Text = "SiegeOnlineKeyMaker";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSystemInfo;
		private System.Windows.Forms.TextBox txtHash;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.Button btnGetSysInfo;
	}
}

