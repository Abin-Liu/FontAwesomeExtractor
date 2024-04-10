using System;
using System.Drawing;
using AbinLibs;

namespace FontAwesomeExtractor.Data
{
	class ConfigHandler
	{
		public string FontName { get; set; }
		public Color ForeColor { get; set; }
		public Color BackColor { get; set; }
		public bool ForeTransparent { get; set; }		
		public bool BackTransparent { get; set; }
		public int ImageSize { get; set; }
		public int Radius { get; set; }
		public bool Modified { get; private set; }

		public void Load()
		{
			RegistryHelper reg = new RegistryHelper();
			reg.Open("Abin", "FontAwesomeExtractor");
			FontName = reg.ReadString("FontName", "fa-home");
			var foreColorName = reg.ReadString("ForeColor", "#337AB7");
			var backColorName = reg.ReadString("BackColor", "#fff");
			ForeTransparent = reg.ReadBool("ForeTransparent", false);
			BackTransparent = reg.ReadBool("BackTransparent", false);
			ImageSize = reg.ReadInt("ImageSize", 256);
			Radius = reg.ReadInt("Radius", 0);
			reg.Close();

			ForeColor = ColorTranslator.FromHtml(foreColorName);
			BackColor = ColorTranslator.FromHtml(backColorName);
		}

		public void Save()
		{
			if (!Modified)
			{
				return;
			}

			var foreColorName = ColorTranslator.ToHtml(ForeColor);
			var backColorName = ColorTranslator.ToHtml(BackColor);

			RegistryHelper reg = new RegistryHelper();
			reg.Open("Abin", "FontAwesomeExtractor", true);
			reg.WriteString("FontName", FontName);
			reg.WriteString("ForeColor", foreColorName);
			reg.WriteString("BackColor", backColorName);
			reg.WriteBool("ForeTransparent", ForeTransparent);
			reg.WriteBool("BackTransparent", BackTransparent);
			reg.WriteInt("ImageSize", ImageSize);
			reg.WriteInt("Radius", Radius);
			reg.Close();

			Modified = false;
		}

		public void SetModified()
		{
			Modified = true;
		}
	}
}
