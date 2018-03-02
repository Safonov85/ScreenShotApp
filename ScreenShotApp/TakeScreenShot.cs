using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ScreenShotApp
{
	public class TakeScreenShot
	{
		public Int64 qualityAmount = 100L;
		Form form;
		bool ctrlDown = false;

		public TakeScreenShot()
		{
			form = new Form();
			form.MouseWheel += new MouseEventHandler(form_MouseWheel);
			form.Click += new System.EventHandler(form_Click);
		}

		private void form_Click(object sender, EventArgs e)
		{
			form.Close();
		}

		// WORKING !!!!!!!!!!!!!!!!!
		private void form_MouseWheel(object sender, MouseEventArgs e)
		{
			Debug.WriteLine(DateTime.Now.Second.ToString());
			if (e.Delta == 120)
			{
				if(ctrlDown == false)
				{
					// rectangle expand
				}
			}
			else
			{
				
			}
		}

		public void TakeSaveScreenshoot(int quality, string format)
		{
			Bitmap screen = new Bitmap(SystemInformation.VirtualScreen.Width,
							 SystemInformation.VirtualScreen.Height);
			Graphics graphics = Graphics.FromImage(screen);

			graphics.CopyFromScreen(SystemInformation.VirtualScreen.X,
							 SystemInformation.VirtualScreen.Y,
							 0, 0, screen.Size);
			string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

			if (format == ".jpg")
			{
				ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
				System.Drawing.Imaging.Encoder myEncoder =
					System.Drawing.Imaging.Encoder.Quality;
				EncoderParameters myEncoderParameters = new EncoderParameters(1);

				EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, qualityAmount);
				myEncoderParameters.Param[0] = myEncoderParameter;
				screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
					DateTime.Now.Second.ToString() + "Quality_" + quality.ToString() + format, jpgEncoder, myEncoderParameters);
				CreateNewScreen(screen);
				graphics.Dispose();
			}
			else if(format == ".png")
			{
				screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
					DateTime.Now.Second.ToString() + format, ImageFormat.Png);
				CreateNewScreen(screen);
				graphics.Dispose();
			}
		}

		private ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo codec in codecs)
			{
				if (codec.FormatID == format.Guid)
				{
					return codec;
				}
			}
			return null;
		}

		void CreateNewScreen(Image picture)
		{
			
			form.Text = "Screenshot Viewer";
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = picture;
			pictureBox.Dock = DockStyle.Fill;
			form.Controls.Add(pictureBox);
			form.Cursor = CreateCursor();
			Debug.WriteLine(DateTime.Now.Second.ToString());
			form.FormBorderStyle = FormBorderStyle.None;
			form.WindowState = FormWindowState.Maximized;
			form.ShowDialog();
			//form.Cursor = Cursors.Hand;
			//Console.Write("");
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

			//this.Cursor = CreateCursor(image);

			image.Dispose();
			graphics.Dispose();
		}

		Cursor CreateCursor()
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
			return new Cursor(image.GetHicon());
		}

	}
}
