using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ScreenShotApp
{
	public class TakeScreenShot
	{
		public Int64 qualityAmount = 100L;

		public void TakeSaveScreenshoot(int quality, string format)
		{
			Bitmap screen = new Bitmap(SystemInformation.VirtualScreen.Width,
							 SystemInformation.VirtualScreen.Height);
			Graphics graphics = Graphics.FromImage(screen);

			graphics.CopyFromScreen(SystemInformation.VirtualScreen.X,
							 SystemInformation.VirtualScreen.Y,
							 0, 0, screen.Size);

			ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
			System.Drawing.Imaging.Encoder myEncoder =
				System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters myEncoderParameters = new EncoderParameters(1);

			EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, qualityAmount);
			myEncoderParameters.Param[0] = myEncoderParameter;

			string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
			screen.Save(desktopPath + "\\" + DateTime.Now.ToShortDateString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
				DateTime.Now.Second.ToString() + "Quality_" + quality.ToString() + format, jpgEncoder, myEncoderParameters);
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
			Form form = new Form();
			form.Text = "Screenshot Viewer";
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = picture;
			pictureBox.Dock = DockStyle.Fill;
			form.Controls.Add(pictureBox);
			form.ShowDialog();
		}
	}
}
