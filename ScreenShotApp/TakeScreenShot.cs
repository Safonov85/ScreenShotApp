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
		int formX, formY;
		PictureBox pictureBox = new PictureBox();

		public TakeScreenShot()
		{
			formX = 400;
			formY = 400;
			form = new Form();
			form.MouseWheel += new MouseEventHandler(form_MouseWheel);
			form.Click += new System.EventHandler(form_Click);
			form.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClick);
			form.MouseDown += new System.Windows.Forms.MouseEventHandler(mouseDown);
			form.MouseUp += new System.Windows.Forms.MouseEventHandler(mouseUp);
			form.KeyDown += new KeyEventHandler(form_KeyDown);
			form.KeyPress += new KeyPressEventHandler(form_KeyPress);
			form.KeyUp += new KeyEventHandler(form_KeyUp);
			form.KeyPreview = true;

			// pictureBox CLICK WORKING!!!!!!!!!!
			pictureBox.Click += new EventHandler(picturebox_Click);
		}

		private void picturebox_Click(object sender, EventArgs e)
		{
			form.Close();
		}

		private void mouseUp(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("UP m");
		}

		private void mouseDown(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("DOWN m");
		}

		private void mouseClick(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("Clicked");
		}

		private void form_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				ctrlDown = false;
				Debug.WriteLine("keyUp pressed");
			}
		}

		private void form_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				ctrlDown = true;
				Debug.WriteLine("keyDown pressed");
			}
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
				if (ctrlDown == false)
				{
					formX += 20;
					form.Cursor = CreateCursor(formX, formY);
					// rectangle expand
				}
				else
				{
					formY += 20;
					form.Cursor = CreateCursor(formX, formY);
				}
			}
			else
			{
				if (ctrlDown == false)
				{
					formX -= 20;
					form.Cursor = CreateCursor(formX, formY);
					// rectangle expand
				}
				else
				{
					formY -= 20;
					form.Cursor = CreateCursor(formX, formY);
				}
			}
		}

		public void TakeSaveScreenshoot()
		{
			Bitmap screen = new Bitmap(SystemInformation.VirtualScreen.Width,
							 SystemInformation.VirtualScreen.Height);
			Graphics graphics = Graphics.FromImage(screen);

			graphics.CopyFromScreen(SystemInformation.VirtualScreen.X,
							 SystemInformation.VirtualScreen.Y,
							 0, 0, screen.Size);
			string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);


			ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
			System.Drawing.Imaging.Encoder myEncoder =
				System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters myEncoderParameters = new EncoderParameters(1);

			EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, qualityAmount);
			myEncoderParameters.Param[0] = myEncoderParameter;
			//screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
			//	DateTime.Now.Second.ToString() + "Quality_" + quality.ToString() + format, jpgEncoder, myEncoderParameters);
			CreateNewScreen(screen);
			graphics.Dispose();

		}

		void SaveCurrentPositionToPicture(int quality, string format)
		{
			Bitmap screen = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

		//	Graphics graphics = Graphics.FromImage(screen);

		//	graphics.CopyFromScreen(SystemInformation.VirtualScreen.X,
		//					 SystemInformation.VirtualScreen.Y,
		//					 0, 0, screen.Size);
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
			}
			else if (format == ".png")
			{
				screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
					DateTime.Now.Second.ToString() + format, ImageFormat.Png);
				CreateNewScreen(screen);
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

			pictureBox.Image = picture;
			pictureBox.Dock = DockStyle.Fill;
			form.Controls.Add(pictureBox);
			form.Cursor = CreateCursor(400, 400);
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

		Cursor CreateCursor(int x, int y)
		{
			Bitmap image = new Bitmap(x, y);
			Graphics graphics = Graphics.FromImage(image);

			//Graphics _g = pictureBox1.CreateGraphics();
			Pen pen = new Pen(Color.Red, 2);
			//pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
			pen.DashPattern = new float[] { 2.0F, 2.0F, 2.0F, 2.0F };
			Point myPoint1 = new Point(10, 20);
			Point myPoint2 = new Point(30, 40);
			graphics.DrawRectangle(pen, 0, 0, x - 1, y - 1);
			return new Cursor(image.GetHicon());
		}
	}
}
