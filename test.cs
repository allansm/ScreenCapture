using System.Drawing.Imaging;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Threading;

public class LoopCapture{
	public static bool isTheEnd;
	public static int y = SystemInformation.VirtualScreen.Height;
	public static int x = SystemInformation.VirtualScreen.Width;

	static void Capture(string filename){
		Bitmap captureBitmap = new Bitmap(x, y, PixelFormat.Format16bppRgb555);
		Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
		Graphics captureGraphics = Graphics.FromImage(captureBitmap);
		captureGraphics.CopyFromScreen(captureRectangle.Left,captureRectangle.Top,0,0,captureRectangle.Size);
		captureBitmap.Save(filename+".png",ImageFormat.Png);
		captureGraphics.Dispose();
	}

	public static void Main(){
		int name = 0;
		while(true){
			Capture((name++)+"");
			System.Threading.Thread.Sleep(33);
		}
	}
}
