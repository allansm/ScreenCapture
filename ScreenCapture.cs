using System.Drawing.Imaging;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Threading;

public class ScreenCapture{
	public static bool isTheEnd;
	public static int y = SystemInformation.VirtualScreen.Height;
	public static int x = SystemInformation.VirtualScreen.Width;
	
	static ImageCodecInfo GetEncoderInfo(string mime_type){
		ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
		for (int i = 0; i <= encoders.Length; i++)
		{
			if (encoders[i].MimeType == mime_type) return encoders[i];
		}
		return null;
	}
	static void Capture(string filename){
		Bitmap captureBitmap = new Bitmap(x, y, PixelFormat.Format16bppRgb555);
		Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
		Graphics captureGraphics = Graphics.FromImage(captureBitmap);
		captureGraphics.CopyFromScreen(captureRectangle.Left,captureRectangle.Top,0,0,captureRectangle.Size);
		captureBitmap.Save(filename+".png",ImageFormat.Png);
		captureGraphics.Dispose();
	}
	static void Capture(string path,int count,long start,int fps,bool isLast){
		//Thread.Sleep(1);
		Bitmap captureBitmap = new Bitmap(x, y, PixelFormat.Format16bppRgb555);//new Bitmap(x, y, PixelFormat.Format32bppArgb);
		try{
			//Console.Clear();
			Console.WriteLine("capture:"+count);
			Console.WriteLine("elapsed time:"+ElapsedTime(start));
			Console.WriteLine("current fps:"+((count/ElapsedTime(start))));
			Thread.Sleep(1);
		}catch(Exception e){
			
		}
		Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
		Graphics captureGraphics = Graphics.FromImage(captureBitmap);
		captureGraphics.CopyFromScreen(captureRectangle.Left,captureRectangle.Top,0,0,captureRectangle.Size);
		//captureGraphics.DrawImage(captureBitmap,0,0,100,100);
		//Bitmap cb2 = new Bitmap(100,100);
		//Graphics cg2 = Graphics.FromImage(cb2);
		//cg2.DrawImage(captureBitmap,0,0,100,100);
		//Thread.Sleep(2000);
		
		captureBitmap.Save(path+count+".png",ImageFormat.Png);
		
		captureGraphics.Dispose();
		if(isLast){
			isTheEnd = true;
		}
	}
	static long ElapsedTimeMs(long start){
		return (DateTimeOffset.Now.ToUnixTimeMilliseconds()-start);
	}
	static int ElapsedTime(long start){
		return (int)ElapsedTimeMs(start)/1000;
	}
	static void RealtimeCapture(){
		try{
			long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			int fps = (30*60); 
			bool isLast;
			int i = 0;
			while(true){ // for(int i=0;i<fps;i++)
				isLast = (i == (fps-1))?true:false;
				Thread t = new Thread(new ThreadStart(() => Capture(@"",i,start,fps,isLast)));
				t.IsBackground = true;
				t.Start();
				Thread.Sleep(33);
				i++;
			}
			long endStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			while(!isTheEnd){
				Console.WriteLine("end time"+ElapsedTime(endStart));
			}
			Console.WriteLine("end");
		}catch (Exception ex){
			Console.WriteLine("error");
		}
		Console.Read();
	}
	public static void Main(){
		RealtimeCapture();
	}
}
