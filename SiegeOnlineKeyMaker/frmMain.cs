using System;
using System.Windows.Forms;

namespace SiegeOnlineKeyMaker
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnGetSysInfo_Click(object sender, EventArgs e)
		{
			var sb = SiegeOnlineDataViewer.SystemInfo.Info.GetInfo();

			txtSystemInfo.Text = sb.ToString();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			var hash = SiegeOnlineDataViewer.SystemInfo.SecurCode.GetHash(txtSystemInfo.Text);

			txtHash.Text = hash;
		}



	}
}
