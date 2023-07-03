using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.ILSpy.MiBlazor.CodeFiles;
using ICSharpCode.ILSpy.MiBlazor.Extensions;
using ICSharpCode.ILSpy.MiBlazor.Writers;

namespace ICSharpCode.ILSpy.MiBlazor.Generator
{
	public class ControlSkeletonGenerator : GeneratorBase
	{
		public override bool CanGenerate(ITypeDefinition typeDefinition)
		{
			return typeDefinition.IsSubclassOf("System.Web.UI.Control");
		}

		public override void Generate(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			GenerateRazor(typeDefinition, ctx);

			GenerateCodeProperties(typeDefinition, ctx);

			GenerateCodeMethods(typeDefinition, ctx);
		}

		private void GenerateRazor(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			RazorWriter output = new();

			CodeFile codeFile = new(
				ctx.CreatePath(typeDefinition, ".razor"),
				output.ToString(), true);
			ctx.Files.Add(codeFile);
		}

		private void GenerateCodeProperties(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			CsWriter output = new() {
				Namespace = typeDefinition.Namespace
			};

			output.WriteLine($"public partial class {typeDefinition.Name}");
			output.Open();

/*			foreach (var field in typeDefinition.Fields)
			{
				GenerateCodeField(field, ctx, output);
			}*/

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
			//ctx.Decompile(field, output);

			/*			output.WriteStart(output.ToAccessibility(field.Accessibility));
						output.Write(" ");
						output.Write(output.GetType(field.ReturnType));
						output.Write(" ");
						output.Write(field.Name);
						output.WriteEnd(";");
			*/
		}

		private void GenerateCodeProperty(IProperty property, GeneratorContext ctx, CsWriter output)
		{
			output.Usings.Add("Microsoft.AspNetCore.Components");
			output.WriteLine("[Parameter]");


			output.WriteStart(output.ToAccessibility(property.Accessibility));
			output.Write(" ");
			output.Write(output.GetType(property.ReturnType));
			output.Write(" ");
			output.Write(property.Name);
			output.Write(" {");
			if (property.CanGet)
			{
				output.Write(" get;");
			}
			if (property.CanSet)
			{
				output.Write(" set;");
			}
			output.WriteEnd(" }");
			output.WriteLine();

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
