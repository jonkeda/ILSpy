using System.Linq;

using ICSharpCode.Decompiler.TypeSystem;

namespace ICSharpCode.ILSpy.MiBlazor.Extensions
{
	internal static class TypeExtension
	{
		public static bool IsSubclassOf(this IType type, string baseTypeName)
		{
			if (type.FullName == baseTypeName)
			{
				return true;
			}

			return type.DirectBaseTypes.Any(baseType => IsSubclassOf(baseType, baseTypeName));
		}

	}
}
