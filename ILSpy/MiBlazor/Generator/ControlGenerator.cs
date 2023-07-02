using System;

using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.ILSpy.MiBlazor.CodeFiles;
using ICSharpCode.ILSpy.MiBlazor.Writers;
using ICSharpCode.ILSpy.TreeNodes;

namespace ICSharpCode.ILSpy.MiBlazor.Generator
{
	public class ControlGenerator : GeneratorBase
	{
		public override bool CanGenerate(ITypeDefinition typeDefinition)
		{
			return true;
		}

		public void Generate(TypeTreeNode typeTreeNode, GeneratorContext ctx)
		{

			GenerateRazor(typeTreeNode.TypeDefinition, ctx);

			GenerateCodeProperties(typeTreeNode.TypeDefinition, ctx);

			GenerateCodeMethods(typeTreeNode.TypeDefinition, ctx);

		}

		private void GenerateRazor(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			RazorWriter output = new ();

			CodeFile codeFile = new(
				ctx.CreatePath(typeDefinition, ".razor"),
				output.ToString(), true);
			ctx.Files.Add(codeFile);
		}

		private void GenerateCodeProperties(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			CsWriter output = new();

			output.Namespace = typeDefinition.Namespace;

			output.WriteLine($"public partial class {typeDefinition.Name}");
			output.Open();

			foreach (var field in typeDefinition.Fields)
			{
				GenerateCodeField(field, ctx, output);
			}

			foreach (var property in typeDefinition.Properties)
			{
				GenerateCodeProperty(property, ctx, output);
			}

			output.Close();

			CodeFile codeFile = new(
				ctx.CreatePath(typeDefinition, ".properties.razor.cs"),
				output.ToString(), true);
			ctx.Files.Add(codeFile);

		}

		private void GenerateCodeField(IField field, GeneratorContext ctx, CsWriter output)
		{
			ctx.Decompile(field, output);

/*			output.WriteStart(output.ToAccessibility(field.Accessibility));
			output.Write(" ");
			output.Write(output.GetType(field.ReturnType));
			output.Write(" ");
			output.Write(field.Name);
			output.WriteEnd(";");
*/		}

		private void GenerateCodeProperty(IProperty property, GeneratorContext ctx, CsWriter output)
		{
			output.Usings.Add("Microsoft.AspNetCore.Components");
			output.WriteLine("[Parameter]");

			ctx.Decompile(property, output);

			/*			output.WriteStart(output.ToAccessibility(property.Accessibility));
						output.Write(" ");
						output.Write(output.GetType(property.ReturnType));
						output.Write(" ");
						output.Write(property.Name);
						output.Write(" {");
						if (property.CanGet)
						{
							if (property.Getter != null)
							{
								output.Open("get");
								ctx.Decompile(property.Getter, output);
								output.Close("");
							}
							else
							{
								output.Write(" get;");
							}
						}
						if (property.CanSet)
						{
							if (property.Setter != null)
							{
								output.Open("set");
								ctx.Decompile(property.Setter, output);
								output.Close("");
							}
							else
							{
								output.Write(" set;");
							}
						}
						output.WriteEnd(" }");
						output.WriteLine();
			*/
		}

		private void GenerateCodeMethods(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			CsWriter output = new();

			output.Namespace = typeDefinition.Namespace;

			output.WriteLine($"public partial class {typeDefinition.Name}");
			output.Open();

			output.Close();

			CodeFile codeFile = new(
				ctx.CreatePath(typeDefinition, ".methods.razor.cs"),
				output.ToString(), true);
			ctx.Files.Add(codeFile);

		}
	}
}
