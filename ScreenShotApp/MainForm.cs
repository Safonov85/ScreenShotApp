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

		void SaveGraphics()
		{
			Bitmap image = new Bitmap(100, 100);
			Graphics graphics = Graphics.FromImage(image);

			//Graphics _g = pictureBox1.CreateGraphics();
			Pen pen = new Pen(Color.Red, 1);
			//pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
			pen.DashPattern = new float[] { 2.0F, 2.0F, 2.0F, 2.0F };
			Point myPoint1 = new Point(10, 20);
			Point myPoint2 = new Point(30, 40);
			graphics.DrawRectangle(pen, 0, 0, 99, 99);

			this.Cursor = CreateCursor(image);
			
			image.Dispose();
			graphics.Dispose();
		}

		Cursor CreateCursor(Bitmap bitmap)
		{
			return new Cursor(bitmap.GetHicon());
		}

		private void TakeSSButton_Click(object sender, EventArgs e)
		{
			TakeScreenShotMethod();
			//Cursor.Current = Cursors.Hand;
			//SaveGraphics();
		}

		private void TakeScreenShotMethod()
		{
			this.Opacity = 0;
			takeScreenShot.TakeSaveScreenshoot();
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
