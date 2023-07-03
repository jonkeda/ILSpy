using ICSharpCode.Decompiler.TypeSystem;

namespace ICSharpCode.ILSpy.MiBlazor.Generator;

public abstract class GeneratorBase
{
	public abstract bool CanGenerate(ITypeDefinition typeDefinition);

	public abstract void Generate(ITypeDefinition typeDefinition, GeneratorContext ctx);

}