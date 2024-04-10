using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FontAwesomeExtractor.Data
{
	internal class FontAwesome
	{
		Dictionary<string, string> m_map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public string this[string key]
		{
			get
			{
				string value;
				if (m_map.TryGetValue(key, out value))
				{
					return value;
				}
				return null;
			}
		}

		public void LoadCSS(string cssPath)
		{
			var contents = File.ReadAllLines(cssPath);
			m_map.Clear();

			List<string> section = new List<string>();			
			foreach (var line in contents)
			{
				var text = line.Trim();
				if (text.Length == 0)
				{
					continue;
				}

				if (text == "}")
				{
					section.Clear();
					continue;
				}

				var match = Regex.Match(text, @"^\.fa-([a-zA-Z0-9\-]+):before[,| ]{?$");
				if (match.Success)
				{
					section.Add(match.Groups[1].Value);
					continue;
				}				

				match = Regex.Match(text, @"^content: ""\\([a-f0-9]{4})"";");
				if (match.Success)
				{
					var code = match.Groups[1].Value;					
					foreach (var key in section)
					{
						m_map["fa-" + key] = GetUnicodeString(code);
					}

					section.Clear();
				}
			}
		}

		public string[] GetFontNames()
		{
			var list = m_map.Keys.ToList();
			list.Sort();
			return list.ToArray();
		}

		static string GetUnicodeString(string code)
		{
			code = "\\u" + code;
			string dst = "";
			string src = code;
			int len = code.Length / 6;
			for (int i = 0; i <= len - 1; i++)
			{
				string str = src.Substring(0, 6).Substring(2);
				src = src.Substring(6);
				byte[] bytes = new byte[2];
				bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
				bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
				dst += Encoding.Unicode.GetString(bytes);
			}
			return dst;
		}
	}
}
