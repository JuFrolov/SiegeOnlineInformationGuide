using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SiegeOnlineDataViewer.Utils
{
	public class TgaImages
	{
		// StructLayout; LayoutKind; Marshal;
		// ----------------------------------------------------------------------

		// ----------------------------------------------------------------------
		//  Developed by: S@nek[BoR]
		//  Creation Date: 18.08.2008
		//  Last Update: 10.05.2009
		// ----------------------------------------------------------------------

		// ----------------------------------------------------------------------
		// # DATA/IMAGE TYPES:
		//  0  -  No image data included.
		//  1  -  Uncompressed, color-mapped images.
		//  2  -  Uncompressed, Unmapped RGB images.
		//  3  -  Uncompressed, black and white images.
		//  9  -  Run length encoded, color-mapped images.
		// 10  -  Run length encoded, RGB images.
		// 11  -  Compressed, black and white images.
		// ----------------------------------------------------------------------

		// ----------------------------------------------------------------------

		public static Bitmap GetFromFile(string FilePath)
		{
			Bitmap bmp = null;
			FileStream fs = null;
			byte[] header = null;
			// ----
			if (!File.Exists(FilePath)) return null;
			// ----
			try
			{
				fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
				// ----
				int iHeaderSize = 0;
				unsafe
				{
					iHeaderSize = sizeof (HEADER_TGA);
				}
				header = new byte[iHeaderSize]; // header;
				// ----
				int bLength = (int) fs.Length - iHeaderSize;
				var buffer = new byte[bLength];
				// ----
				fs.Position = 0;
				fs.Read(header, 0, iHeaderSize); // Маркер сдвинут на 18 байтов, так как произошло чтение;
				fs.Read(buffer, 0, bLength);
				fs.Close();
				// ----
				var sgHeader = new HEADER_TGA();
				unsafe
				{
					Marshal.Copy(header, 0, new IntPtr(&sgHeader), sizeof (HEADER_TGA));
				}
				// ----
				if (sgHeader.PixelDepth == 16 || sgHeader.PixelDepth == 24 || sgHeader.PixelDepth == 32)
				{
					switch (sgHeader.ImageType)
					{
						case 0:
							bmp = GetUnCompressed(buffer, sgHeader);
							break;
						case 1:
							break;
						case 2:
							bmp = GetUnCompressed(buffer, sgHeader);
							break;
						case 3:
							bmp = GetUnCompressed(buffer, sgHeader);
							break;
						case 9:
							break;
						case 10:
							bmp = GetCompressed(buffer, sgHeader);
							break;
						case 11:
							break;
						default:
							break;
					}
				}
			}
			catch (Exception)
			{
				bmp = null;
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
					fs.Dispose();
				}
			}
			// ----
			return bmp;
		}

		// ----------------------------------------------------------------------

		// ----------------------------------------------------------------------
		// The functions of processing *.tga files.
		// ----------------------------------------------------------------------

		private static unsafe Bitmap GetUnCompressed(byte[] buffer, HEADER_TGA sgHeader)
		{
			Bitmap bmp = null;
			int Width, Height, bpp, bytesPerPixel, stride;
			// ----
			try
			{
				// Определяем ширину TGA изображения;
				Width = sgHeader.Width;
				// Определяем высоту TGA изображения;
				Height = sgHeader.Height;
				// Получаем кол-во бит на пиксель; (16, 24, 32);
				bpp = sgHeader.PixelDepth;
				// Делим на 8 для получения байт/пиксель;
				bytesPerPixel = bpp/8;
				// Вычисляем количество байт в строке;
				stride = Width*bytesPerPixel;
				// ----
				const int Isb32p = 32;
				// ----
				byte* row = null;
				IntPtr rowP = IntPtr.Zero;
				int x = 0, y = 0, pt = 0, iStep = 0;
				var line = new byte[stride];
				// ----
				bool LeftToRight = ((sgHeader.ImageDescriptor & (1 << (8 - 4))) == 0);
				bool TopToBottom = ((sgHeader.ImageDescriptor & (1 << (8 - 5))) == 0);
				if (bpp < 32) TopToBottom = !TopToBottom;
				// ----
				PixelFormat px = PixelFormat.Format32bppArgb;
				switch (bpp)
				{
					case 24:
						px = PixelFormat.Format24bppRgb;
						break;
					case 32:
						px = PixelFormat.Format32bppArgb;
						break;
				}
				bmp = new Bitmap(Width, Height, px);
				var rect = new Rectangle(0, 0, Width, Height);
				BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
				// ----
				if (LeftToRight) // Вывод изображения слева на право;
				{
					if (TopToBottom) // Вывод изобрадения сверху вниз;
					{
						for (y = 0; y < Height; y++) // Проход по всем строкам;
						{
							/*
                            Array.Copy(buffer, iStep, line, 0, stride);
                            iStep += stride;
                            x = 0;
                            // ----
                            row = (byte*)bmpData.Scan0 + (y * bmpData.Stride);
                            // ----
                            for (pt = 0; pt < stride; pt += bytesPerPixel, x += bytesPerPixel) // Проход по строке;
                            {
                                row[x + 0] = line[pt + 0];
                                row[x + 1] = line[pt + 1];
                                row[x + 2] = line[pt + 2];
                                if (bpp == 32) row[x + 3] = line[pt + 3];
                            }
                            */
							// ----
							///*
							rowP = (IntPtr) ((int) bmpData.Scan0 + (y*bmpData.Stride));
							// ----
							Marshal.Copy(buffer, iStep, rowP, stride);
							iStep += stride;
							//*/
						}
					}
					else // Вывод изобрадения сниху вверх;
					{
						for (y = Height - 1; y >= 0; y--) // Проход по всем строкам;
						{
							/*
                            Array.Copy(buffer, iStep, line, 0, stride);
                            iStep += stride;
                            x = 0;
                            // ----
                            row = (byte*)bmpData.Scan0 + (y * bmpData.Stride);
                            // ----
                            for (pt = 0; pt < stride; pt += bytesPerPixel, x += bytesPerPixel) // Проход по строке;
                            {
                                row[x + 0] = line[pt + 0];
                                row[x + 1] = line[pt + 1];
                                row[x + 2] = line[pt + 2];
                                if (bpp == 32) row[x + 3] = line[pt + 3];
                            }
                            */
							// ----
							///*
							rowP = (IntPtr) ((int) bmpData.Scan0 + (y*bmpData.Stride));
							// ----
							Marshal.Copy(buffer, iStep, rowP, stride);
							iStep += stride;
							//*/
						}
					}
				}
				else // Вывод изображения справо на лево;
				{
					if (TopToBottom) // Вывод изобрадения сверху вниз;
					{
						for (y = 0; y < Height; y++) // Проход по всем строкам;
						{
							Array.Copy(buffer, iStep, line, 0, stride);
							iStep += stride;
							x = 0;
							// ----
							row = (byte*) bmpData.Scan0 + (y*bmpData.Stride);
							// ----
							for (pt = stride - 1; pt >= 0; pt -= bytesPerPixel, x += bytesPerPixel) // Проход по строке;
							{
								row[x + 0] = line[pt - 3];
								row[x + 1] = line[pt - 2];
								row[x + 2] = line[pt - 1];
								if (bpp == Isb32p) row[x + 3] = line[pt - 0];
							}
						}
					}
					else // Вывод изобрадения сниху вверх;
					{
						for (y = Height - 1; y >= 0; y--) // Проход по всем строкам;
						{
							Array.Copy(buffer, iStep, line, 0, stride);
							iStep += stride;
							x = 0;
							// ----
							row = (byte*) bmpData.Scan0 + (y*bmpData.Stride);
							// ----
							for (pt = stride - 1; pt >= 0; pt -= bytesPerPixel, x += bytesPerPixel) // Проход по строке;
							{
								row[x + 0] = line[pt - 3];
								row[x + 1] = line[pt - 2];
								row[x + 2] = line[pt - 1];
								if (bpp == Isb32p) row[x + 3] = line[pt - 0];
							}
						}
					}
				}
				// ----
				bmp.UnlockBits(bmpData);
			}
			catch (Exception)
			{
				bmp = null;
			}
			// ----
			return bmp;
		}

		#region Compress

		private static Bitmap GetCompressed(byte[] buffer, HEADER_TGA sgHeader)
		{
			Bitmap bmp = null;
			MemoryStream nms = null;
			int Width, Height, bpp, bytesPerPixel;
			// ----
			try
			{
				// Определяем ширину TGA изображения;
				Width = sgHeader.Width;
				// Определяем высоту TGA изображения;
				Height = sgHeader.Height;
				// Получаем кол-во бит на пиксель; (16, 24, 32);
				bpp = sgHeader.PixelDepth;
				// Делим на 8 для получения байт/пиксель;
				bytesPerPixel = bpp/8;
				// ----
				const int iRAWSection = 127;
				// ----
				uint j = 0;
				int iStep = 0;
				int bpCount = 0;
				uint currentpixel = 0;
				uint pixelcount = Convert.ToUInt32(Width*Height); // Количество пикселей в изображении;
				var colorbuffer = new byte[bytesPerPixel];
				byte chunkheader = 0;
				// ----
				nms = new MemoryStream();
				// ----
				while (currentpixel < pixelcount)
				{
					chunkheader = buffer[iStep];
					iStep++;
					// ----
					if (chunkheader <= iRAWSection) // Если секция является 'RAW' секцией;
					{
						chunkheader++; // Добавляем единицу для получения количества RAW пикселей;
						// ----
						/*
                        for (j = 0; j < chunkheader; j++)
                        {
                            Array.Copy(buffer, iStep, colorbuffer, 0, bytesPerPixel);
                            iStep += bytesPerPixel;
                            // ----
                            nms.Write(colorbuffer, 0, bytesPerPixel);
                        }
                        */
						// ----
						//
						bpCount = bytesPerPixel*chunkheader;
						nms.Write(buffer, iStep, bpCount);
						iStep += bpCount;
						//
						// ----
						currentpixel += chunkheader;
					}
					else // Если это RLE идентификатор;
					{
						chunkheader -= iRAWSection; // Вычитаем 127 для получения количества повторений;
						// ----
						Array.Copy(buffer, iStep, colorbuffer, 0, bytesPerPixel); // Считываем 1 пиксель;
						iStep += bytesPerPixel;
						// ----
						for (j = 0; j < chunkheader; j++) nms.Write(colorbuffer, 0, bytesPerPixel);
						// ----
						currentpixel += chunkheader;
					}
				}
				// ----
				var contentBuffer = new byte[nms.Length];
				nms.Position = 0; //Сброс позиции старта
				nms.Read(contentBuffer, 0, contentBuffer.Length);
				bmp = GetUnCompressed(contentBuffer, sgHeader);
			}
			catch (Exception)
			{
				bmp = null;
			}
			finally
			{
				if (nms != null)
				{
					nms.Close();
					nms.Dispose();
				}
			}
			// ----
			return bmp;
		}

		// ----------------------------------------------------------------------*/

		#endregion

		#region Nested type: HEADER_TGA

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct HEADER_TGA // 18 bytes;
		{
			public byte IDLength; /* 00h  Size of Image ID field */
			public byte ColorMapType; /* 01h  Color map type */
			public byte ImageType; /* 02h  Image type code */
			public ushort CMapStart; /* 03h  Color map origin */
			public ushort CMapLength; /* 05h  Color map length */
			public byte CMapDepth; /* 07h  Depth of color map entries */
			public ushort XOffset; /* 08h  X origin of image */
			public ushort YOffset; /* 0Ah  Y origin of image */
			public ushort Width; /* 0Ch  Width of image */
			public ushort Height; /* 0Eh  Height of image */
			public byte PixelDepth; /* 10h  Image pixel size */
			public byte ImageDescriptor; /* 11h  Image descriptor byte */
		}

		#endregion

		// ----------------------------------------------------------------------
	}
}