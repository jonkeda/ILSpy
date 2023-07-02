using System.Text;

using ICSharpCode.Decompiler.TypeSystem;

using Accessibility = ICSharpCode.Decompiler.TypeSystem.Accessibility;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class CsWriter : CodeWriter
	{
		public string Namespace { get; set; }
		public UsingDictionary Usings { get; set; } = new ();

		public override string ToString()
		{
			StringBuilder builder = new();

			if (!Usings.Empty)
			{
				builder.AppendLine(Usings.ToString(Namespace));
				builder.AppendLine();
			}

			if (!string.IsNullOrEmpty(Namespace))
				builder.AppendLine($"namespace {Namespace};");

			builder.AppendLine();
			builder.AppendLine(base.ToString());

			return builder.ToString();
		}

		public CsWriter() : base("{", "}", "/*", "*/")
		{
		}

		public CsWriter(int spaces) : this()
		{
			Spaces = spaces;
		}

		public void AddImport(string importText)
		{
			Usings.Add(importText);
		}

		public string GetType(IType iType)
		{
			return iType.FullName;
		}

		public string ToAccessibility(Decompiler.TypeSystem.Accessibility accessibility)
		{
			switch (accessibility)
			{
				case Decompiler.TypeSystem.Accessibility.Public:
				{
					return "public";
				}
				case Decompiler.TypeSystem.Accessibility.Protected:
				{
					return "protected";
				}
				case Decompiler.TypeSystem.Accessibility.Internal:
				{
					return "internal";
				}
				case Decompiler.TypeSystem.Accessibility.Private:
				{
					return "private";
				}
				case Decompiler.TypeSystem.Accessibility.ProtectedAndInternal:
				{
					return "private protected";
				}
				case Decompiler.TypeSystem.Accessibility.ProtectedOrInternal:
				{
					return "protected internal";
				}
			}

			return null;
		}
	}
}