using System.IO;

namespace ICSharpCode.ILSpy.MiBlazor.CodeFiles
{
	public class CodeFile
	{
		public CodeFile()
		{
		}

		public CodeFile(string path, string code)
		{
			Path = path;
			Code = code;
			Overwrite = true;
		}

		public CodeFile(string path, string code, bool overwrite)
		{
			Path = path;
			Code = code;
			Overwrite = overwrite;
		}

		public string Path { get; set; }

		public string Code { get; set; }

		public bool Overwrite { get; set; }

		public void Save()
		{
			FileInfo file = new (Path);
			file.Directory.Create();

			if (!file.Exists || Overwrite)
			{
				File.WriteAllText(Path, Code);
			}
		}
	}
}