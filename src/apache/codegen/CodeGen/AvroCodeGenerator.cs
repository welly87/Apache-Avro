using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Avro.codegen.CodeGen
{
    public class AvroCodeGenerator
    {
        public void Generate(string schemasDirectory, string targetDirectory)
        {
            var sorter = new TopologicalSorter();

            var schemas = Directory.GetFiles(schemasDirectory, "*.avsc", SearchOption.AllDirectories);

            var records = schemas.Select(x => x.ToRecord()).ToList();

            var results = sorter.Sort(records).ToList();

            if (Directory.Exists(targetDirectory))
                Directory.Delete(targetDirectory, true);

            GenerateSchemas(results, targetDirectory);
        }

        private void GenerateSchemas(IEnumerable<Record> results, string outDir)
        {
            var codegen = new Avro.CodeGen();

            var schemaNames = new SchemaNames();

            foreach (var result in results)
            {
                try
                {
                    Console.WriteLine("Start processing : {0}", result.Fullname);

                    var scheme = Schema.Parse(result.Schema, schemaNames);

                    schemaNames.Add((NamedSchema)scheme);

                    codegen.AddSchema(scheme);
                }
                catch (SchemaParseException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Generate : {0}", result.Fullname);
            }

            codegen.GenerateCode();
            codegen.WriteTypes(outDir);
            Console.WriteLine("Completed.");
        }

    }
}