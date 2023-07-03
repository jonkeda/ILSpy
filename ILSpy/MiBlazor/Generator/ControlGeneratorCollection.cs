
using System.Collections.ObjectModel;

using ICSharpCode.Decompiler.TypeSystem;

namespace ICSharpCode.ILSpy.MiBlazor.Generator
{
	public class ControlGeneratorCollection : Collection<GeneratorBase>
	{
		public bool Generate(ITypeDefinition typeDefinition, GeneratorContext ctx)
		{
			foreach (var generator in this)
			{
				if (generator.CanGenerate(typeDefinition))
				{
					generator.Generate(typeDefinition, ctx);
					return true;
				}
			}

			return false;
		}
	}
}
