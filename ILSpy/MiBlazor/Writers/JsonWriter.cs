namespace ICSharpCode.ILSpy.MiBlazor.Writers
{
	public class JsonWriter : CodeWriter
	{
		private bool _addComma = false;
		private string _name = "\"";
		private string _value = "\"";

		public JsonWriter() : base("{", "}", "/*", "*/")
		{
		}

		public JsonWriter(string nameEnclosure, string valueEnclosure) : base("{", "}", "/*", "*/")
		{
			_name = nameEnclosure;
			_value = valueEnclosure;
		}

		public void Open(string open)
		{
			AddComma();
			_addComma = false;
			LineStart();
			Write(open);
			Spaces++;
		}

		private void AddComma()
		{
			if (_addComma)
			{
				WriteEnd(",");
			}
			else
			{
				WriteEnd("");
			}
		}

		public void Close(string close)
		{
			WriteEnd("");
			Spaces--;
			LineStart();
			Write(close);
			_addComma = true;
		}

		public void Add(string name)
		{
			AddComma();
			LineStart();
			Write(_name);
			Write(name);
			Write(_name);
			_addComma = true;
		}

		private string Encode(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Replace("'", "\\'");
		}

		public void Add(string name, string value)
		{
			if (value == null)
			{
				return;
			}
			AddComma();
			LineStart();

			Write(_name);
			Write(name);
			Write(_name);

			Write(":");

			Write(_value);
			Write(Encode(value));
			Write(_value);

			_addComma = true;
		}

		public void AddRaw(string name, string value)
		{
			if (value == null)
			{
				return;
			}
			AddComma();
			LineStart();

			Write(_name);
			Write(name);
			Write(_name);

			Write(":");

			Write(value);

			_addComma = true;
		}

		public void AddObject(string name, string value)
		{
			if (value == null)
			{
				return;
			}
			AddComma();
			LineStart();
			Write(BlockOpen);
			Write(_name);
			Write(name);
			Write(_name);
			Write(":");
			Write(_value);
			Write(value);
			Write(_value);
			Write(BlockClose);
			_addComma = true;
		}

		public void OpenArray(string name)
		{
			Open($"{name}{_name}: [");
		}

		public void CloseArray()
		{
			Close("]");
		}

		public void OpenObject(string name)
		{
			Open($"{name}{_name}: {{");
		}

		public void CloseObject()
		{
			Close("}");
		}
	}
}
