using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Avro.codegen.CodeGen
{
    public class Field
    {
        public string Name { get; set; }
        public object Type { get; set; }

        public IList<string> Dependencies
        {
            get
            {
                var dependencies = new List<string>();

                // union
                if (Type is JArray)
                {
                    var obj = (JArray)Type;

                    dependencies.AddRange(obj.Values<string>());
                }
                else if (Type is JObject)
                {
                    var obj = (JObject) Type;

                    var type = obj["type"].ToString();
                    
                    if (type == "map")
                    {
                        var ty = obj["values"].ToString();
                        dependencies.Add(ty);
                    }
                    else if (type == "array")
                    {
                        var ty = obj["items"].ToString();
                        dependencies.Add(ty);
                    }
                }
                else if (Type is string)
                {
                    var type = Type.ToString();

                    if (!type.IsPrimitiveType())
                    {
                        dependencies.Add(type);
                    }
                }

                return dependencies.Distinct().ToList();
            }
        }
    }
}