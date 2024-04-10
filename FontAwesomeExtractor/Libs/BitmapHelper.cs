using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace AbinLibs
{
	/// <summary>
	/// Bitmap图像处理帮助类
	/// </summary>
	public class BitmapHelper
	{
		/// <summary>
		/// 抓取屏幕指定区域
		/// </summary>
		/// <param name="x">x坐标</param>
		/// <param name="y">y坐标</param>
		/// <param name="width">区域宽度</param>
		/// <param name="height">区域高度</param>		
		/// <param name="dpi">画质DPI，0表示系统默认值</param>	
		/// <returns>抓取成功返回Bitmap对象，否则返回null</returns>
		public static Bitmap CaptureScreen(int x, int y, int width, int height, int dpi = 0)
		{
			if (width < 1 || height < 1)
			{
				return null;
			}			

			Bitmap bmp = null;

			bmp = new Bitmap(width, height);			
			if (dpi > 0)
			{
				bmp.SetResolution(dpi, dpi);
			}

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(x, y, 0, 0, new Size(width, height));
			}

			return bmp;
		}

		/// <summary>
		/// 抓取某窗口客户区域中指定区域
		/// </summary>
		/// <param name="hwnd">窗口句柄</param>
		/// <param name="x">x坐标</param>
		/// <param name="y">y坐标</param>
		/// <param name="width">区域宽度</param>
		/// <param name="height">区域高度</param>
		/// <param name="dpi">画质DPI，0表示系统默认值</param>	
		/// <returns>抓取成功返回Bitmap对象，否则返回null</returns>
		public static Bitmap CaptureClient(IntPtr hwnd, int x, int y, int width, int height, int dpi = 0)
		{
			if (hwnd != IntPtr.Zero)
			{
				Point pt;
				if (!ClientToScreen(hwnd, out pt))
				{
					return null;
				}

				x += pt.X;
				y += pt.Y;
			}

			return CaptureScreen(x, y, width, height, dpi);
		}

		/// <summary>
		/// 抓取某窗口中指定区域
		/// </summary>
		/// <param name="hwnd">窗口句柄</param>
		/// <param name="x">x坐标</param>
		/// <param name="y">y坐标</param>
		/// <param name="width">区域宽度</param>
		/// <param name="height">区域高度</param>
		/// <param name="dpi">画质DPI，0表示系统默认值</param>	
		/// <returns>抓取成功返回Bitmap对象，否则返回null</returns>
		public static Bitmap CaptureWindow(IntPtr hwnd, int x, int y, int width, int height, int dpi = 0)
		{
			if (hwnd != IntPtr.Zero)
			{				
				Rectangle rect = new Rectangle();
				if (GetWindowRect(hwnd, out rect))
				{
					x = rect.X;
					y = rect.Y;
				}				
			}

			return CaptureScreen(x, y, width, height, dpi);
		}

		/// <summary>
		/// 抓取鼠标位置指定区域
		/// </summary>
		/// <param name="x">水平偏移值</param>
		/// <param name="y">垂直偏移值</param>
		/// <param name="width">区域宽度</param>
		/// <param name="height">区域高度</param>
		/// <param name="dpi">画质DPI，0表示系统默认值</param>	
		/// <returns>抓取成功返回Bitmap对象，否则返回null</returns>
		public static Bitmap CaptureCursor(int x, int y, int width, int height, int dpi = 0)
		{
			Point pt;
			GetCursorPos(out pt);
			return CaptureScreen(pt.X + x, pt.Y + y, width, height, dpi);
		}

		/// <summary>
		/// 图像拼接
		/// </summary>
		/// <param name="imageList">待拼接的图像列表</param>
		/// <param name="vertical">是否垂直拼接</param>
		/// <param name="gutter">拼接间隙</param>
		/// <returns>拼接后的图像</returns>
		public static Bitmap Merge(IEnumerable<Bitmap> imageList, bool vertical = true, int gutter = 0)
		{
			return Merge(imageList, Color.Black, vertical, gutter);
		}

		/// <summary>
		/// 图像拼接
		/// </summary>
		/// <param name="imageList">待拼接的图像列表</param>
		/// <param name="background">背景填充色</param>
		/// <param name="direction">拼接方向</param>
		/// <param name="gutter">拼接间隙</param>
		/// <returns>拼接后的图像</returns>
		public static Bitmap Merge(IEnumerable<Bitmap> imageList, Color background, bool vertical = true, int gutter = 0)
		{           
            var list = imageList?.ToList();

			if (list == null || list.Count == 0)
			{
				return null;
			}

			if (list.Count == 1)
			{
				return list[0];
			}

			if (gutter < 1)
			{
				gutter = 0;
			}

			int totalGutter = (list.Count - 1) * gutter;

			int outputWidth;
			int outputHeight;			

			if (vertical)
			{
				outputWidth = list.Max(x => x.Width);
				outputHeight = list.Sum(x => x.Height) + totalGutter;
			}
			else
			{
				outputWidth = list.Sum(x => x.Width) + totalGutter;
				outputHeight = list.Max(x => x.Height);
			}

			var outputImage = new Bitmap(outputWidth, outputHeight);
			var graphic = Graphics.FromImage(outputImage);
			Brush brush = new SolidBrush(background);
			graphic.FillRectangle(brush, 0, 0, outputWidth, outputHeight);

			if (vertical)
			{
				int y = 0;
				foreach (Bitmap image in list)
				{
					graphic.DrawImage(image, 0, y);
					y += image.Height;
					y += gutter;
				}
			}
			else
			{
				int x = 0;
				foreach (Bitmap image in list)
				{
					graphic.DrawImage(image, x, 0);
					x += image.Width;
					x += gutter;
				}
			}			

			return outputImage;
		}

		/// <summary>
		/// 图像裁剪
		/// </summary>
		/// <param name="bmp">待裁剪图像</param>
		/// <param name="x">裁剪X起点</param>
		/// <param name="y">裁剪Y起点</param>
		/// <param name="width">保留宽度</param>
		/// <param name="height">保留高度</param>
		/// <returns>裁剪后的图像</returns>
		public static Bitmap Crop(Bitmap bmp, int x, int y, int width, int height)
		{
			if (bmp == null)
			{
				throw new ArgumentNullException("bmp is required.");
			}

			var cropRect = new Rectangle(x, y, width, height);
			var outputImage = new Bitmap(width, height);
			var graphic = Graphics.FromImage(outputImage);
			Brush brush = new SolidBrush(Color.Black);
			graphic.FillRectangle(brush, 0, 0, width, height);
			graphic.DrawImage(bmp, 0, 0, cropRect, GraphicsUnit.Pixel);
			return outputImage;
		}

		/// <summary>
		/// 图像缩放
		/// </summary>
		/// <param name="bmp">源图像</param>
		/// <param name="zoom">缩放比例</param>		
		/// <returns>操作成功返回新图像，失败返回null</returns>
		public static Bitmap Resize(Bitmap bmp, double zoom)
		{
			if (bmp == null)
			{
				return null;
			}			

			if (zoom < 0)
			{
				zoom = -zoom;
			}

			int width = (int)(bmp.Width * zoom);
			int height = (int)(bmp.Height * zoom);
			if (width == bmp.Width || height == bmp.Height)
			{
				return bmp;
			}

			if (width < 1 || height < 1)
			{
				return null;
			}

			Bitmap newBmp = new Bitmap(width, height);
			newBmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

			using (Graphics g = Graphics.FromImage(newBmp))
			{
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.DrawImage(bmp, 0, 0, width, height);
			}

			return newBmp;
		}		

		/// <summary>
		/// 图像灰度化
		/// </summary>
		/// <param name="bmp">源图像</param>		
		/// <returns>操作成功返回新图像，失败返回null</returns>
		public static Bitmap Greyscale(Bitmap bmp)
		{
			if (bmp == null)
			{
				return null;
			}

			bmp = new Bitmap(bmp);
			int width = bmp.Width;
			int height = bmp.Height;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Color c = bmp.GetPixel(x, y);
					int rgb = (int)Math.Round(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
					c = Color.FromArgb(rgb, rgb, rgb);
					bmp.SetPixel(x, y, c);
				}
			}

			return bmp;
		}

		/// <summary>
		/// 图像二值化
		/// </summary>
		/// <param name="bmp">源图像</param>
		/// <param name="threshold">二值化亮度阈值，小于此值设为黑色，大于等于此值设为白色，默认为0.72</param>
		/// <returns>操作成功返回新图像，失败返回null</returns>
		public static Bitmap Binarisation(Bitmap bmp, double threshold = 0.72)
		{
			if (bmp == null)
			{
				return null;
			}

			bmp = new Bitmap(bmp);
			int width = bmp.Width;
			int height = bmp.Height;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Color c = bmp.GetPixel(x, y);
					double brightness = c.GetBrightness();
					if (brightness < threshold)
					{
						c = Color.Black;
					}
					else
					{
						c = Color.White;
					}

					bmp.SetPixel(x, y, c);
				}
			}

			return bmp;
		}

		/// <summary>
		/// 生成圆角图片
		/// </summary>
		/// <param name="bmp">源图像</param>
		/// <param name="radius">圆角半径（像素）</param>
		/// <returns>生成的圆角图像</returns>
		public static Bitmap RoundCorner(Bitmap bmp, int radius)
		{
			if (bmp == null)
			{
				return null;
			}

			if (radius < 1)
			{
				return bmp;
			}

			Bitmap output = new Bitmap(bmp.Width, bmp.Height);
			Graphics g = Graphics.FromImage(output);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, bmp.Width, bmp.Height));

			using (var path = CreateRoundedRectanglePath(bmp.Width, bmp.Height, radius))
			{
				g.SetClip(path);
			}

			g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
			g.Dispose();

			return output;
		}

		static GraphicsPath CreateRoundedRectanglePath(int width, int height, int cornerRadius)
		{
			int diameter = cornerRadius * 2;
			var path = new GraphicsPath();

			// 左上角
			path.AddArc(0, 0, diameter, diameter, 180, 90);
			path.AddLine(diameter, 0, width - diameter, 0);

			// 右上角
			path.AddArc(width - diameter, 0, diameter, diameter, 270, 90);
			path.AddLine(width, diameter, width, height - diameter);

			// 右下角
			path.AddArc(width - diameter, height - diameter, diameter, diameter, 0, 90);
			path.AddLine(width - diameter, height, diameter, height);

			// 左下角
			path.AddArc(0, height - diameter, diameter, diameter, 90, 90);
			path.AddLine(0, height - diameter, 0, diameter);

			path.CloseFigure();
			return path;
		}

		/// <summary>
		/// 将图像转换为ICO格式图标
		/// </summary>
		/// <param name="bmp">源图像</param>
		/// <returns>生成的ICO对象</returns>
		public static Icon ConvertToIcon(Image bmp)
		{
			if (bmp == null)
			{
				return null;
			}

			using (MemoryStream msImg = new MemoryStream(), msIco = new MemoryStream())
			{
				bmp.Save(msImg, ImageFormat.Png);

				using (var bin = new BinaryWriter(msIco))
				{
					//写图标头部
					bin.Write((short)0);           //0-1保留
					bin.Write((short)1);           //2-3文件类型。1=图标, 2=光标
					bin.Write((short)1);           //4-5图像数量（图标可以包含多个图像）

					bin.Write((byte)bmp.Width);	   //6图标宽度
					bin.Write((byte)bmp.Height);   //7图标高度
					bin.Write((byte)0);            //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
					bin.Write((byte)0);            //9保留。必须为0
					bin.Write((short)0);           //10-11调色板
					bin.Write((short)32);          //12-13位深
					bin.Write((int)msImg.Length);  //14-17位图数据大小
					bin.Write(22);                 //18-21位图数据起始字节

					//写图像数据
					bin.Write(msImg.ToArray());

					bin.Flush();
					bin.Seek(0, SeekOrigin.Begin);
					return new Icon(msIco);
				}
			}
		}

		/// <summary>
		/// 将图像的指定区域扫描为一维像素列表
		/// </summary>
		/// <param name="bmp">待扫描图像</param>
		/// <param name="x">扫描起始X</param>
		/// <param name="y">扫描起始Y</param>
		/// <param name="width">扫描区域宽度</param>
		/// <param name="height">扫描区域高度</param>
		/// <returns>扫描结果</returns>
		public static List<Color> Scan(Bitmap bmp, int x, int y, int width, int height)
		{
			if (bmp == null)
			{
				return null;
			}

			x = Math.Max(x, 0);
			y = Math.Max(y, 0);
			width = Math.Min(width, bmp.Width - x);
			height = Math.Min(height, bmp.Height - y);

			List<Color> colorList = new List<Color>();
			if (width < 1 || height < 1)
			{
				return colorList;
			}

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					var color = bmp.GetPixel(x + i, y + j);
					colorList.Add(color);
				}
			}		

			return colorList;
		}		

		[DllImport("user32.dll")]
		extern static bool ClientToScreen(IntPtr hwnd, out Point lpPoint);

		[DllImport("user32.dll")]
		static extern int GetCursorPos(out Point point);

		[DllImport("user32.dll")]
		static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);
	}
}
