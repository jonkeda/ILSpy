using System.IO;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.ILSpy.MiBlazor.CodeFiles;
using ICSharpCode.ILSpy.MiBlazor.Writers;

namespace ICSharpCode.ILSpy.MiBlazor.Generator;

public class GeneratorContext
{
	public GeneratorContext(Language language, DecompilationOptions decompilationOptions)
	{
		Language = language;
		DecompilationOptions = decompilationOptions;
	}

	public Language Language { get; }

	public DecompilationOptions DecompilationOptions { get; }

	public CodeFileCollection Files { get; } = new();

	public string ProjectPath { get; set; } = @"C:\Github\MiBlazor\MiBlazor\MiBlazorExample\";

	public string CreatePath(ITypeDefinition typeDefinition, string extension)
	{
		return $@"{ProjectPath}{typeDefinition.FullName.Replace('.', Path.DirectorySeparatorChar)}{extension}";
	}

	public void Decompile(IMethod method, CsWriter output)
	{
		var text = new PlainTextOutput();

		Language.DecompileMethod(method, text, DecompilationOptions);

		output.WriteLinesRemoveComments(text.ToString());
	}

	public void Decompile(IProperty property, CsWriter output)
	{
		var text = new PlainTextOutput();

		Language.DecompileProperty(property, text, DecompilationOptions);

		output.WriteLinesRemoveComments(text.ToString());
	}

	public void Decompile(IField field, CsWriter output)
	{
		var text = new PlainTextOutput();

		Language.DecompileField(field, text, DecompilationOptions);

		output.WriteLinesRemoveComments(text.ToString());
	}

}