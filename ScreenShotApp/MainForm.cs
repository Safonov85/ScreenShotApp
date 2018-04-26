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
using System.Diagnostics;

namespace ScreenShotApp
{
	public partial class MainForm : Form
	{
		TakeScreenShot takeScreenShot = new TakeScreenShot();

		public MainForm()
		{
			InitializeComponent();
			PicFormatComboBox.SelectedIndex = 0;
		}

		private void TakeSSButton_Click(object sender, EventArgs e)
		{
			PrepareScreenShotMethod();
		}

		private void PrepareScreenShotMethod()
		{
			this.Opacity = 0;
			takeScreenShot.format = PicFormatComboBox.SelectedItem.ToString();
			takeScreenShot.PutScreenshootOnScreen();
			this.Opacity = 100;
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			takeScreenShot.qualityAmount = trackBar1.Value;
			QualityAmountLabel.Text = trackBar1.Value.ToString();
		}

		private void MainForm_MouseEnter(object sender, EventArgs e)
		{

		}

		private void MainForm_Scroll(object sender, ScrollEventArgs e)
		{
			Debug.WriteLine(DateTime.Now.Second.ToString());
		}

		private void MainForm_MouseClick(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("Clicked MAIN");
		}
	}
}
