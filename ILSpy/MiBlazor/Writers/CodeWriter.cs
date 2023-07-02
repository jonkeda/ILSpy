using System;
using System.Text;

namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public abstract class CodeWriter
	{
		public string BlockOpen { get; }
		public string BlockClose { get; }
		public string CommentOpen { get; }
		public string CommentClose { get; }
		public string SpaceString { get; }

		protected CodeWriter(string blockOpen, string blockClose, string commentOpen, string commentClose)
		{
			BlockOpen = blockOpen;
			BlockClose = blockClose;
			CommentOpen = commentOpen;
			CommentClose = commentClose;
			SpaceString = "   ";
		}

		protected CodeWriter(string blockOpen, string blockClose, string commentOpen, string commentClose, string spaceString)
		{
			BlockOpen = blockOpen;
			BlockClose = blockClose;
			CommentOpen = commentOpen;
			CommentClose = commentClose;
			SpaceString = spaceString;
		}

		private readonly StringBuilder _sb = new StringBuilder();
		protected int Spaces;

		public void Write(string text)
		{
			if (text != null)
			{
				_sb.Append(text);
			}
		}

		public void WriteStart(string text)
		{
			if (text != null)
			{
				LineStart();
				_sb.Append(text);
			}
		}

		public void WriteEnd(string text)
		{
			if (text != null)
			{
				_sb.Append(text);
				_sb.Append('\n');
			}
		}

		public void WriteLine(string text)
		{
			if (text != null)
			{
				LineStart();
				_sb.Append(text);
				_sb.Append('\n');
			}
		}

		public void WriteLines(string text)
		{
			if (text != null)
			{
				string[] lines = text.Split('\n');
				foreach (string line in lines)
				{
					WriteLine(line);
				}
			}
		}

		public void WriteLinesRemoveComments(string text)
		{
			if (text != null)
			{
				string[] lines = text.Split('\n');
				foreach (string line in lines)
				{
					if (line.StartsWith("//"))
					{

					}
					else
					{
						WriteLine(line);
					}
				}
			}
		}

		public void WriteException(Exception ex)
		{
			_sb.Append(ex.Message);
			_sb.Append('\n');
		}

		public void WriteLine()
		{
			_sb.Append('\n');
		}

		public virtual void Empty()
		{
			_sb.Append('\n');
		}

		public void Open()
		{
			Open(BlockOpen);
		}

		public void Open(string open)
		{
			LineStart();
			_sb.Append(open);
			_sb.Append('\n');
			Spaces++;
		}

		public void LineStart()
		{
			for (int i = 0; i < Spaces; i++)
			{
				_sb.Append(SpaceString);
			}
		}

		public void Close()
		{
			Close(BlockClose);
		}

		public void Close(string close)
		{
			if (Spaces > 0)
				Spaces--;
			LineStart();
			_sb.Append(close);
			_sb.Append('\n');
		}

		public override string ToString()
		{
			return _sb.ToString();
		}

		public void Generated()
		{
			Comment("Generated");
		}

		public void Comment(string comment)
		{
			WriteLine($"{CommentOpen} {comment} {CommentClose}");
		}
	}
}
