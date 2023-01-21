using System;
using System.Collections.Generic;
using System.Linq;
using Qol.Core;
using Qol.Core.Symbols;
using Qol.Core.Syntax;
using System.IO;
using Qol.Core.IO;
using Mono.Options;

namespace QCompiler
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var outputPath = (string) null;
            var moduleName = (string) null;
            var referencePaths = new List<string>();
            var sourcePaths = new List<string>();
            var helpRequested = false;

            var options = new OptionSet
            {
                "usage: QCompiler <source-paths> [options]",
                {   "r=", "The {path} of an assembly to reference.", v => referencePaths.Add(v)  },
                {   "o=", "The output {path} of an assembly to create.", v => outputPath = v },
                {   "m=", "The {name} of the module.", v => moduleName = v },
                {   "?|h|help", "Prints help", v => helpRequested = true },
                {   "<>", v => sourcePaths.Add(v) }
                
            };
            options.Parse(args);

            referencePaths.Add(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0\ref\netcoreapp3.1\System.Runtime.dll");
            referencePaths.Add(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0\ref\netcoreapp3.1\System.Runtime.Extensions.dll");
            referencePaths.Add(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0\ref\netcoreapp3.1\System.Console.dll");

            if(helpRequested)
            {
                options.WriteOptionDescriptions(Console.Out);
                return 0;
            }

            if(sourcePaths.Count == 0)
            {
                Console.Error.WriteLine("error: need at least one source file.");
                return 1;
            }

            if(outputPath == null)
                outputPath = Path.ChangeExtension(sourcePaths[0], ".exe");


            if(moduleName == null)
                moduleName = Path.GetFileNameWithoutExtension(outputPath);

            var syntaxTrees = new List<SyntaxTree>();
            var hasErrors = false;

            foreach (var path in sourcePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"error: file '{path}' doesn't exist");
                    hasErrors = true;
                    continue;
                }
                var syntaxTree = SyntaxTree.Load(path);
                syntaxTrees.Add(syntaxTree);
            }

            foreach (var path in referencePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"error: file '{path}' doesn't exist");
                    hasErrors = true;
                    continue;
                }
            }

            if (hasErrors)
                return 1;

            var compilation = Compilation.Create(syntaxTrees.ToArray());
            var diagnostics = compilation.Emit(moduleName, referencePaths.ToArray(), outputPath);

            if (diagnostics.Any())
            {
                Console.Error.WriteDiagnostics(diagnostics);
                return 1;
            }
            return 0;
        }
    }
}
