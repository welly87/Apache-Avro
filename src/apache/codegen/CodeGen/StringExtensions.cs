using System.Collections.Generic;
using Newtonsoft.Json;

namespace Avro.codegen.CodeGen
{
    public static class StringExtensions
    {
        static readonly IList<string> PrimitiveList = new List<string>
        {
            "boolean", "int", "long", "double", "float", "string", "fixed", "bytes", "null", "enum"
        };

        public static bool IsPrimitiveType(this string type)
        {
            return PrimitiveList.Contains(type);
        }

        public static Record ToRecord(this string schema)
        {
            var content = System.IO.File.ReadAllText(schema);

            var record = JsonConvert.DeserializeObject<Record>(content);

            record.Schema = content;

            return record;
        }
    }
}