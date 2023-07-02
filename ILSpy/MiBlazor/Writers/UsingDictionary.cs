using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class UsingDictionary
	{
		private readonly Dictionary<string, string> usings = new();

		public bool Empty {
			get {
				return usings.Count == 0;
			}
		}

		public void Add(string aUsing)
		{
			if (usings.ContainsKey(aUsing))
			{
				return;
			}
			usings.Add(aUsing, aUsing);
		}

		public string ToString(string aPackage)
		{
			StringBuilder sb = new();

			foreach (string key in usings.Keys
				.OrderBy(i => i).ToList())
			{
				sb.AppendFormat("using {0};\n", key);
			}

			return sb.ToString();
		}
	}
}
