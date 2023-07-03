using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.ILSpy.MiBlazor.CodeFiles;

namespace ICSharpCode.ILSpy.MiBlazor.Generator
{
	public class TypeFullGenerator : GeneratorBase
	{
		public override bool CanGenerate(ITypeDefinition typeDefinition)
		{
			return true;
		}

		public override void Generate(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			var language = ctx.Language;

			var output = new PlainTextOutput();

			language.DecompileType(typeDefinition, output, ctx.DecompilationOptions);

			CodeFile codeFile = new(
				ctx.CreatePath(typeDefinition, ".cs"),
				output.ToString(), true);
			ctx.Files.Add(codeFile);
		}
	}
}
