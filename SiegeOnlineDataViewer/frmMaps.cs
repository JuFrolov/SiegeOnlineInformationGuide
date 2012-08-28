using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnlineDataViewer.Properties;
using SiegeOnlineDataViewer.SiegeDataFiles;

namespace SiegeOnlineDataViewer
{
	public partial class frmMaps : Form
	{
		private List<SiegeArtefactDescStructure> MapsCode = new List<SiegeArtefactDescStructure>();

		public frmMaps()
		{
			InitializeComponent();

			LoadMapsList();
		}

		public frmMaps(string map)
		{
			InitializeComponent();

			LoadMapsList();

			cmbMaps.SelectedText = map;
		}

		private void LoadMapsList()
		{
			foreach (KeyValuePair<string, SiegeArtefactDescStructure> map in SiegeDataBase.DataArtefacts)
			{
				if (map.Value.MapData == null)
					continue;

				var index = cmbMaps.Items.Add(map.Value.Description);

				MapsCode.Insert(index, map.Value);
			}

		}

		private void cmbMaps_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbMaps.SelectedIndex >= 0 && cmbMaps.SelectedIndex < MapsCode.Count)
			{
				var map = MapsCode[cmbMaps.SelectedIndex];


				bool first;
				var mapImage = GetImage(SiegeDataBase.DataObjectsUi.Get(map.MapData.MapSprite), out first);

				//pictMap.Image = mapImage;


				/*
				var fileName = SiegeDataBase.DataObjectsUi.Get(map.MapData.MapSprite) ?? "";
				if (File.Exists(fileName))
				{
					try
					{
						GraphicsDevice graphics = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, Handle,
																	 new PresentationParameters());

						var t = Texture2D.FromFile(graphics, fileName);

						pictMap.Image = FastTextureToBitmap(t);
					}
					catch (Exception ex)
					{
						pictMap.Image = null;

						MessageBox.Show(
							string.Format("Во время загрузки карты произошла ошибка:{0}{1}", Environment.NewLine, ex.Message),
							string.Format("Ошибка открытия файла '{0}'", fileName), MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				*/

				/*foreach (SiegeMapStructure.MarkerClass marker in map.MapData.Markers)
				{
					PictureBox point = new PictureBox
					                   	{
					                   		SizeMode = PictureBoxSizeMode.Zoom,
					                   		Left = 10,
					                   		Top = 10,
											Width = 32,
											Height = 32,
					                   		Image = Resources.Map_Point_Small
					                   	};

					pictMap.Controls.Add(point);

				}*/


			}
		}

		private Image GetImage(string file, out bool first)
		{
			first = false;

			if (string.IsNullOrEmpty(file))
				return null;

			var path = Path.Combine(SiegeDataBase.GamePath, @"Data\Textures\");

			var pathImage = Path.Combine(Application.CommonAppDataPath, Path.GetFileName(file).Replace(".tga", ".jpg"));

			var pathDds = Path.Combine(path, file.Replace(".tga", ".dds"));

			if (!File.Exists(pathImage))
			{
				if (!File.Exists(pathDds))
					return null;

				first = true;

				try
				{
					GraphicsDevice graphics = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, Handle,
				                                             new PresentationParameters());

					using (var t = Texture2D.FromFile(graphics, pathDds))
					{
						t.Save(pathImage, ImageFileFormat.Jpg);
					}

					/*
					cmbMaps.Enabled = false;
					var dt = DateTime.Now;
					while (DateTime.Now - dt < TimeSpan.FromSeconds(5))
						Application.DoEvents();
					cmbMaps.Enabled = true;
					*/
				}
				catch(Exception)
				{
					return null;
				}
			}

			Image result;

			try
			{
				using (var image = new Bitmap(pathImage))
				{
					result = image;

					pictMap.Image = image;
				}

				File.Delete(pathImage);
			}
			catch(Exception)
			{
				return null;
			}

			return result;
		}

		private Microsoft.Xna.Framework.Graphics.Color[] GetColorDataFromTexture(Texture2D texture)
		{
			var colors = new Microsoft.Xna.Framework.Graphics.Color[texture.Width * texture.Height];
			texture.GetData(colors);
			return colors;
		}

		private Bitmap FastTextureToBitmap(Texture2D texture)
		{
			// Setup pointer back to bitmap
			Bitmap newBitmap = new Bitmap(texture.Width, texture.Height);

			// Get color data from the texture
			var textureColors = GetColorDataFromTexture(texture);

			System.Drawing.Imaging.BitmapData bmpData =
				newBitmap.LockBits(new System.Drawing.Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
				                   System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			// Loop through pixels and set values
			unsafe
			{
				byte* bmpPointer = (byte*) bmpData.Scan0;
				for (int y = 0; y < texture.Height; y++)
				{
					for (int x = 0; x < texture.Width; x++)
					{

						bmpPointer[0] = textureColors[x + y*texture.Width].B;
						bmpPointer[1] = textureColors[x + y*texture.Width].G;
						bmpPointer[2] = textureColors[x + y*texture.Width].R;
						bmpPointer[3] = textureColors[x + y*texture.Width].A;

						bmpPointer += 4;

					}

					bmpPointer += bmpData.Stride - (bmpData.Width*4);
				}
			}

			textureColors = null;
			newBitmap.UnlockBits(bmpData);

			return newBitmap;
		}

		private void frmMaps_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Hide();

				e.Cancel = true;
			}
		}
	}
}
