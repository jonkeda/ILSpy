using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class TextWriter : CodeWriter
	{
		public TextWriter() : base("", "", "", "")
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

			Write("\t");

			WriteEnd(value);
		}

		public TextWriter(int spaces) : this()
		{
			Spaces = spaces;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
