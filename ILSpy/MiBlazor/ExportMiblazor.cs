// Copyright (c) 2023 Daan
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.IO;
using System.Linq;

using ICSharpCode.Decompiler;
using ICSharpCode.ILSpy.MiBlazor.CodeFiles;
using ICSharpCode.ILSpy.MiBlazor.Generator;
using ICSharpCode.ILSpy.Properties;
using ICSharpCode.ILSpy.TreeNodes;
using ICSharpCode.TreeView;

namespace ICSharpCode.ILSpy.MiBlazor;

[ExportContextMenuEntry(Header = "Export MiBlazor", Category = nameof(Resources.Shell))]
internal sealed class ExportMiblazor : IContextMenuEntry
{
	public bool IsVisible(TextViewContext context)
	{
		return true;
	}

	public bool IsEnabled(TextViewContext context)
	{
		return true;
	}

	public void Execute(TextViewContext context)
	{
		var selectedNode = context.SelectedTreeNodes.FirstOrDefault();

		
		GeneratorContext generatorContext = new (
			MainWindow.Instance.CurrentLanguage,
			MainWindow.Instance.CreateDecompilationOptions());

		MigrateNode(selectedNode, generatorContext);

		generatorContext.Files.Save();
	}

	private void MigrateNode(SharpTreeNode selectedNode, GeneratorContext generatorContext)
	{
		if (selectedNode is AssemblyTreeNode assemblyTreeNode)
		{
			MigrateAssemblyTreeNode(assemblyTreeNode, generatorContext);
		}
		else if (selectedNode is NamespaceTreeNode namespaceTreeNode)
		{
			MigrateNamespaceTreeNode(namespaceTreeNode, generatorContext);
		}
		else if (selectedNode is TypeTreeNode typeTreeNode)
		{
			MigrateTypeTreeNode(typeTreeNode, generatorContext);
		}
	}

	private void MigrateAssemblyTreeNode(AssemblyTreeNode assemblyTreeNode, GeneratorContext generatorContext)
	{
		foreach (SharpTreeNode child in assemblyTreeNode.Children)
		{
			MigrateNode(child, generatorContext);
		}
	}

	private void MigrateNamespaceTreeNode(NamespaceTreeNode namespaceTreeNode, GeneratorContext generatorContext)
	{
		foreach (SharpTreeNode child in namespaceTreeNode.Children)
		{
			MigrateNode(child, generatorContext);
		}
	}

	private ControlGenerator ControlGenerator = new ();

	private void MigrateTypeTreeNode(TypeTreeNode typeTreeNode, GeneratorContext generatorContext)
	{
		if (ControlGenerator.CanGenerate(typeTreeNode.TypeDefinition))
		{
			ControlGenerator.Generate(typeTreeNode, generatorContext);
			return;
		}

		var language = typeTreeNode.Language;
		
		var output = new PlainTextOutput();

		language.DecompileType(typeTreeNode.TypeDefinition, output, generatorContext.DecompilationOptions);

		CodeFile codeFile = new(
			generatorContext.CreatePath(typeTreeNode), 
			output.ToString(), true);
		generatorContext.Files.Add(codeFile);
	}


}