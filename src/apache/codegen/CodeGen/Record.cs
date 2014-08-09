using System.Collections.Generic;
using System.Linq;

namespace Avro.codegen.CodeGen
{
    public class Record
    {
        public string Schema { get; set; }

        public string Namespace { get; set; }

        public string Name { get; set; }

        public string Fullname
        {
            get
            {
                return string.Format("{0}.{1}", Namespace, Name);
            }
        }

        public IList<Field> Fields { get; set; }

        public Record()
        {
            Fields = new List<Field>();
        }

        public IList<string> Dependencies
        {
            get
            {
                return Fields.SelectMany(x => x.Dependencies).Where(x => !x.IsPrimitiveType()).Distinct().ToList();
            }
        }
    }
}