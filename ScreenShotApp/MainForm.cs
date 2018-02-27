﻿using System;
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
		bool changeCursor = false;
		
		TakeScreenShot takeScreenShot = new TakeScreenShot();
		public MainForm()
		{
			InitializeComponent();
			PicFormatComboBox.SelectedIndex = 0;
			MessageBox.Show("Sorry temporarly not working. Coming back soon.");
		}

		public void DrawLineInt(PaintEventArgs e)
		{

			// Create pen.
			Pen blackPen = new Pen(Color.Black, 3);

			// Create coordinates of points that define line.
			int x1 = 100;
			int y1 = 100;
			int x2 = 500;
			int y2 = 100;

			// Draw line to screen.
			e.Graphics.DrawLine(blackPen, x1, y1, x2, y2);
		}

		private void TakeSSButton_Click(object sender, EventArgs e)
		{
			if(changeCursor == false)
			{
				Cursor = new Cursor("C:\\Windows\\Cursors\\size4_r.cur");
				
				changeCursor = true;
			}
			else
			{
				Cursor = new Cursor("C:\\Windows\\Cursors\\arrow_i.cur");
				changeCursor = false;
			}
			//TakeScreenShotMethod();
		}

		private void TakeScreenShotMethod()
		{
			this.Opacity = 0;
			if (PicFormatComboBox.SelectedIndex == 0)
			{
				takeScreenShot.TakeSaveScreenshoot(trackBar1.Value, ".jpg");
			}
			else if (PicFormatComboBox.SelectedIndex == 1)
			{
				takeScreenShot.TakeSaveScreenshoot(trackBar1.Value, ".png");
			}
			this.Opacity = 100;
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			takeScreenShot.qualityAmount = trackBar1.Value;
			QualityAmountLabel.Text = trackBar1.Value.ToString();
		}

	}
}
