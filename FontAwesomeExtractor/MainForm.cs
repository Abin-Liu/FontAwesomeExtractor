using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using AbinLibs;
using FontAwesomeExtractor.Data;

namespace FontAwesomeExtractor
{
	public partial class MainForm : Form
	{
		static readonly string FontAwesomeUrl = "https://fontawesome.com.cn/v4/icons";
		static readonly string FontFolder = AppDomain.CurrentDomain.BaseDirectory + "Fonts";
		static readonly int[] FontSizes = { 16, 32, 64, 128, 256 };

		ConfigHandler m_config = new ConfigHandler();
		FontAwesome m_fontAwesome = new FontAwesome();
		PrivateFontCollection m_pfc = new PrivateFontCollection();
		string m_code = null;

		public MainForm()
		{
			InitializeComponent();

			m_fontAwesome.LoadCSS(FontFolder + "\\font-awesome.css");
			cmbFontName.DataSource = m_fontAwesome.GetFontNames();

			m_pfc.AddFontFile(FontFolder + "/fontawesome-webfont.ttf");
			lblIcon.BorderStyle = BorderStyle.None;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			m_config.Load();			
			cmbFontName.Text = m_config.FontName;			
			chkForeTransparent.Checked = m_config.ForeTransparent;
			chkBackTransparent.Checked = m_config.BackTransparent;
			numCornerRadius.Value = m_config.Radius;
			UpdateColorPickers();
			InitFontSizeList();
			ExtractCode();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_config.Save();
		}

		private void btnForeColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = m_config.ForeColor;
			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			m_config.ForeColor = dlg.Color;
			m_config.SetModified();
			btnForeColor.BackColor = dlg.Color;
			DrawImage();
		}

		private void btnBackgroundColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = m_config.BackColor;
			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			m_config.BackColor = dlg.Color;
			m_config.SetModified();
			btnBackgroundColor.BackColor = dlg.Color;
			DrawImage();
		}

		private void btnExtract_Click(object sender, EventArgs e)
		{
			ExtractCode();
		}

		void ExtractCode()
		{
			var fontName = NormalizeFontName(cmbFontName.Text);
			var code = m_fontAwesome[fontName];
			if (m_code == code)
			{
				return;
			}

			m_code = code;
			m_config.FontName = fontName;
			m_config.SetModified();
			DrawImage();
		}

		void DrawImage()
		{
			if (string.IsNullOrEmpty(m_code))
			{
				pictureBox1.Image = null;				
			}	
			else
			{
				lblIcon.Text = m_code;
				ApplyImageSize(m_config.ImageSize);
				lblIcon.ForeColor = GetActualColor(m_config.ForeColor, m_config.ForeTransparent);
				lblIcon.BackColor = GetActualColor(m_config.BackColor, m_config.BackTransparent);

				var image = new Bitmap(lblIcon.Width, lblIcon.Height);
				lblIcon.DrawToBitmap(image, new Rectangle(0, 0, image.Width, image.Height));
				image.MakeTransparent(SystemColors.Control);
				image = BitmapHelper.RoundCorner(image, m_config.Radius);
				pictureBox1.Image = image;
			}			
		}

		string NormalizeFontName(string fontName)
		{
			fontName = fontName?.Trim()?.ToLower();
			if (string.IsNullOrEmpty(fontName))
			{
				return string.Empty;
			}

			if (!fontName.StartsWith("fa-"))
			{
				fontName = "fa-" + fontName;
			}

			return fontName;
		}		

		void ApplyImageSize(int size)
		{
			lblIcon.Width = size;
			lblIcon.Height = size;
			lblIcon.Font = new Font(m_pfc.Families[0], (int)(size * 0.6));
			lblIcon.Padding = new Padding((int)(size / 16.0), 0, 0, 0);

			pictureBox1.Width = size;
			pictureBox1.Height = size;
		}	
		
		static Color GetActualColor(Color color, bool transparent)
		{
			if (transparent)
			{
				return SystemColors.Control;
			}

			return color;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var img = pictureBox1.Image;
			if (img == null)
			{
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.FileName = m_config.FontName;
			dlg.Filter = "Png File (*.png)|*.png|Icon File (*.ico)|*.ico|Bitmap file (*.bmp)|*.bmp|Jpeg File (*.jpg)|*.jpg|Gif File (*.gif)|*.gif";
			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			var filePath = dlg.FileName;
			var ext = Path.GetExtension(filePath).ToLower();
			switch (ext)
			{
				case ".jpg":
					img.Save(filePath, ImageFormat.Jpeg);
					break;

				case ".png":
					img.Save(filePath, ImageFormat.Png);
					break;

				case ".gif":
					img.Save(filePath, ImageFormat.Gif);
					break;

				case ".ico":
					SaveIcon(img, filePath);
					break;

				default:
					img.Save(filePath, ImageFormat.Bmp);
					break;
			}
		}

		private static void SaveIcon(Image image, string filePath)
		{
			var icon = BitmapHelper.ConvertToIcon(image);
			using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				icon.Save(fs);
				fs.Flush();				
			}
		}

		private void lnkFontAwesome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(FontAwesomeUrl);
		}

		private void chkForeTransparent_CheckedChanged(object sender, EventArgs e)
		{
			m_config.ForeTransparent = chkForeTransparent.Checked;
			m_config.SetModified();
			UpdateColorPickers();
			DrawImage();
		}

		private void chkBackTransparent_CheckedChanged(object sender, EventArgs e)
		{
			m_config.BackTransparent = chkBackTransparent.Checked;
			m_config.SetModified();
			UpdateColorPickers();
			DrawImage();
		}

		private void ddlFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = ddlFontSize.SelectedIndex;
			if (index == -1)
			{
				return;
			}

			m_config.SetModified();
			m_config.ImageSize = FontSizes[index];
			DrawImage();
		}

		private void numCornerRadius_ValueChanged(object sender, EventArgs e)
		{
			m_config.Radius = (int)numCornerRadius.Value;
			m_config.SetModified();
			DrawImage();
		}		

		void UpdateColorPickers()
		{
			if (m_config.ForeTransparent)
			{
				btnForeColor.BackColor = SystemColors.Control;
				btnForeColor.Enabled = false;
			}
			else
			{
				btnForeColor.BackColor = m_config.ForeColor;
				btnForeColor.Enabled = true;
			}

			if (m_config.BackTransparent)
			{
				btnBackgroundColor.BackColor = SystemColors.Control;
				btnBackgroundColor.Enabled = false;
			}
			else
			{
				btnBackgroundColor.BackColor = m_config.BackColor;
				btnBackgroundColor.Enabled = true;
			}
		}

		void InitFontSizeList()
		{
			foreach (int size in FontSizes)
			{
				ddlFontSize.Items.Add($"{size} * {size}");
			}

			int index = Array.IndexOf(FontSizes, m_config.ImageSize);
			if (index == -1)
			{
				index = FontSizes.Length - 1;
			}

			ddlFontSize.SelectedIndex = index;
		}
	}
}
