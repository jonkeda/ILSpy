using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class JavaWriter : CodeWriter
	{
		public string Package { get; set; }
		public ImportDictionary Imports { get; set; } = new ImportDictionary();

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			if (!string.IsNullOrEmpty(Package))
				builder.AppendLine($"package {Package};");

			builder.AppendLine();
			builder.AppendLine(Imports.ToString(Package));
			builder.AppendLine(base.ToString());

			return builder.ToString();
		}

		public JavaWriter() : base("{", "}", "/*", "*/")
		{
		}

		public JavaWriter(int spaces) : this()
		{
			Spaces = spaces;
		}

		public void AddImport(string importText)
		{
			Imports.Add(importText);
		}
	}
}