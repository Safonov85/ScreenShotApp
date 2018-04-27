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
		int cursorSizeX, cursorSizeY;
		PictureBox pictureBox = new PictureBox();
		public string format = ".jpg";
		string manualTutorial = "Scroll";

		public TakeScreenShot()
		{
			cursorSizeX = 400;
			cursorSizeY = 400;
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
			//pictureBox.
		}

		private void picturebox_Click(object sender, EventArgs e)
		{
			MouseEventArgs me = (MouseEventArgs)e;
			if (me.Button == MouseButtons.Left)
			{
				TakeCurrentScreenshoot(Control.MousePosition.X, Control.MousePosition.Y, format, qualityAmount);
				form.Close();
				return;
			}
			if(me.Button == MouseButtons.Right)
			{
				form.Close();
			}
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
			// CTRL Down
			if (e.KeyCode == Keys.ControlKey)
			{
				ctrlDown = false;
				Debug.WriteLine("keyUp pressed");
			}

			// Esc press EXIST!!!
			if(e.KeyCode == Keys.Escape)
			{
				form.Close();
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
			manualTutorial = "Scroll + Ctrl";
			if (e.Delta == 120)
			{
				if (ctrlDown == false)
				{
					cursorSizeX += 20;
					form.Cursor = CreateCursor(cursorSizeX, cursorSizeY);
					// rectangle expand
				}
				else
				{
					cursorSizeY += 20;
					form.Cursor = CreateCursor(cursorSizeX, cursorSizeY);
				}
			}
			else
			{
				if (ctrlDown == false)
				{
					cursorSizeX -= 20;
					form.Cursor = CreateCursor(cursorSizeX, cursorSizeY);
					// rectangle expand
				}
				else
				{
					cursorSizeY -= 20;
					form.Cursor = CreateCursor(cursorSizeX, cursorSizeY);
				}
			}
		}

		public void TakeCurrentScreenshoot(int cursorPosX, int cursorPosY, string format, Int64 quality)
		{
			Bitmap screen = new Bitmap(cursorSizeX, cursorSizeY);
			Graphics graphics = Graphics.FromImage(screen);

			float screenStartX = cursorPosX - (screen.Size.Width / 2);
			float screenStartY = cursorPosY - (screen.Size.Height / 2);

			// Crops the Exact Region
			graphics.CopyFromScreen((int)screenStartX, (int)screenStartY, 0, 0, screen.Size);

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
			}
			else if (format == ".png")
			{
				screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
					DateTime.Now.Second.ToString() + format, ImageFormat.Png);
			}
		}

		public void PutScreenshootOnScreen()
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
			CreateNewScreen(screen);
			graphics.Dispose();
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
			form.Cursor = CreateCursor(cursorSizeX, cursorSizeY);
			Debug.WriteLine(DateTime.Now.Second.ToString());
			form.FormBorderStyle = FormBorderStyle.None;
			form.WindowState = FormWindowState.Maximized;
			form.ShowDialog();
		}

		// Test
		void SaveGraphics()
		{
			Bitmap image = new Bitmap(100, 100);
			Graphics graphics = Graphics.FromImage(image);
			
			Pen pen = new Pen(Color.Red, 1);
			pen.DashPattern = new float[] { 2.0F, 2.0F, 2.0F, 2.0F };
			Point myPoint1 = new Point(10, 20);
			Point myPoint2 = new Point(30, 40);
			graphics.DrawRectangle(pen, 0, 0, 99, 99);

			image.Dispose();
			graphics.Dispose();
		}

		Cursor CreateCursor(int x, int y)
		{
			Bitmap image = new Bitmap(x, y);
			Graphics graphics = Graphics.FromImage(image);
			
			Pen pen = new Pen(Color.Red, 2);
			pen.DashPattern = new float[] { 2.0F, 2.0F, 2.0F, 2.0F };
			Point myPoint1 = new Point(10, 20);
			Point myPoint2 = new Point(30, 40);
			graphics.DrawRectangle(pen, 0, 0, x - 1, y - 1);

			// blue circle
			//graphics.DrawEllipse(Pens.Blue, 0, 0, 50, 50);

			// Text
			graphics.DrawString(manualTutorial, new Font("Calibri", 20, FontStyle.Regular), Brushes.Black, 0, y - 50);

			return new Cursor(image.GetHicon());
		}
	}
}
