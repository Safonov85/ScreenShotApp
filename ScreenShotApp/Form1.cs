using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ScreenShotApp
{
	public partial class MainForm : Form
	{
		TakeScreenShot takeScreenShot = new TakeScreenShot();
		public MainForm()
		{
			InitializeComponent();
		}

		private void TakeSSButton_Click(object sender, EventArgs e)
		{
			this.Opacity = 0;
			takeScreenShot.TakeSaveScreenshoot(trackBar1.Value);
			this.Opacity = 100;
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			takeScreenShot.qualityAmount = trackBar1.Value;
			QualityAmountLabel.Text = trackBar1.Value.ToString();
		}
	}
}
