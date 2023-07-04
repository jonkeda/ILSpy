using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Decompiler.TypeSystem;

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

		public void AddUsing(string importText)
		{
			Usings.Add(importText);
		}

		public string GetType(IType iType)
		{
			if (Types.TryGetValue(iType.FullName, out CommonType value))
			{
				AddUsing(value.Namespace);
				return value.Name;
			}
			AddUsing(iType.Namespace);
			return iType.Name;
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


		public record CommonType(string Namespace, string Name);

		private static readonly Dictionary<string, CommonType> Types = new();

		static CsWriter()
		{
			Add(typeof(string), "string");
			Add(typeof(bool), "bool");
			Add(typeof(int), "int");
			Add(typeof(long), "long");
			Add(typeof(float), "float");
			Add(typeof(double), "double");
		}

		private static void Add(Type type, string name)
		{
			Types.Add(type.FullName, new CommonType(type.Namespace, name));
		}

	}
}