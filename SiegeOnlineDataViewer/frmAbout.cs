using System;
using System.Text;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer
{
	public partial class frmAbout : Form
	{
		public frmAbout()
		{
			InitializeComponent();
		}

		private void frmAbout_Load(object sender, EventArgs e)
		{
			txtAbout.Text = SystemInfo.Info.GetInfo().ToString();
		}
	}
}
