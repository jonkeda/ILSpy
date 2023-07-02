using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class YamlWriter : CodeWriter
	{
		public YamlWriter() : base("", "", "#", "")
		{
		}

		public void Add(string name, string value)
		{
			if (value == null)
			{
				return;
			}
			LineStart();

			Write(name);

			Write(": ");

			Write("\"");
			Write(value);
			WriteEnd("\"");
		}

		public YamlWriter(int spaces) : this()
		{
			Spaces = spaces;
		}

		public void Open(string open)
		{
			LineStart();
			Write(open);
			WriteEnd(":");
			Spaces++;
		}

		public void Close(string close)
		{
			if (Spaces > 0)
				Spaces--;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}