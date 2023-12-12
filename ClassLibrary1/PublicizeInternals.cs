using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using System.IO;

namespace ClassLibrary1
{
    public class PublicizeInternals : Task
    {
        [Required]
        public ITaskItem SourceReference { get; set; }

        [Required]
        public string IntermediateOutputPath { get; set; }

        [Output]
        public ITaskItem GeneratedReference { get; set; }

        public override bool Execute()
        {
            //System.Diagnostics.Debugger.Launch();

            var assembly = AssemblyDefinition.ReadAssembly(SourceReference.ItemSpec);

            foreach (var type in assembly.MainModule.GetTypes())
            {
                if (!type.IsNested && type.IsNotPublic)
                {
                    type.IsPublic = true;
                }
                else if (type.IsNested && type.IsNestedPublic)
                {
                    type.IsNestedPublic = true;
                }

                foreach (var field in type.Fields)
                {
                    if (!field.IsPublic)
                    {
                        field.IsPublic = true;
                    }
                }

                foreach (var method in type.Methods)
                {
                    if (!method.IsPublic)
                    {
                        method.IsPublic = true;
                    }
                }
            }

            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), IntermediateOutputPath, "Generated");

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var filename = SourceReference.GetMetadata("Filename");
            var extension = SourceReference.GetMetadata("Extension");

            var filePath = Path.Combine(dirPath, $"{filename}{extension}");

            assembly.Write(filePath);

            GeneratedReference = new TaskItem(filePath);

            return true;
        }
    }
}
