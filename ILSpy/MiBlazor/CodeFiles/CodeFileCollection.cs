using System.Collections.Generic;

namespace ICSharpCode.ILSpy.MiBlazor.CodeFiles
{
	public class CodeFileCollection : List<CodeFile>
	{
		public void Save()
		{
			foreach (CodeFile codeFile in this)
			{
				codeFile.Save();
			}
		}
	}
}