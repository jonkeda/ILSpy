using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class PlantUmlWriter : CodeWriter
	{
		public string Package { get; set; }
		public ImportDictionary Imports { get; set; } = new ImportDictionary();

		public PlantUmlWriter() : base("{", "}", "/*", "*/")
		{
		}

		public void Start()
		{
			WriteLine("@startuml");
			WriteLine();
			WriteLine("' hide the spot");
			WriteLine("hide circle");
			WriteLine();
			WriteLine("' either show or hide members");
			WriteLine("' hide members");
			WriteLine("' show members");
			WriteLine();
			WriteLine("' avoid problems with angled crows feet");
			WriteLine("skinparam linetype ortho");
			WriteLine();
		}

		public void End()
		{
			WriteLine("@enduml");
		}

		public PlantUmlWriter(int spaces) : this()
		{
			Spaces = spaces;
		}

		public void AddImport(string importText)
		{
			Imports.Add(importText);
		}

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
	}
}