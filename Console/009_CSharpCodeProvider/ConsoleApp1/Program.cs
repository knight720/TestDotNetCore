using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Test1();
            Test2();

            Console.ReadLine();
        }

        private static void Test1()
        {
            string filePath = @"D:\Code\Test\TestDotNetCore\Console\009_CSharpCodeProvider\ConsoleApp1\UserEntity.cs";
        }

        private static Action<string> Write = Console.WriteLine;

        private static void Test2()
        {
            Write("Let's compile!");

            string codeToCompile = @"
            using System;
            namespace RoslynCompileSample
            {
                public class Writer
                {
                    public void Write(string message)
                    {
                        //Console.WriteLine($""you said '{message}!'"");
                    }
                }
            }";

            codeToCompile = File.ReadAllText(@"D:\Code\Test\TestDotNetCore\Console\009_CSharpCodeProvider\ConsoleApp1\UserEntity.cs");

            Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location)
            };

            Write("Compiling ...");
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    Write("Compilation failed!");
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    Write("Compilation successful! Now instantiating and executing the code ...");
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("ConsoleApp1.UserEntity");
                    var instance = assembly.CreateInstance("ConsoleApp1.UserEntity");
                    //var meth = type.GetMember("Write").First() as MethodInfo;
                    //meth.Invoke(instance, new[] { "joel" });

                    Console.WriteLine("The {0} type has the following properties: ",
                        type.Name);
                    foreach (var prop in type.GetProperties())
                        Console.WriteLine("   {0} ({1})", prop.Name,
                            prop.PropertyType.Name);
                }
            }
        }
    }
}