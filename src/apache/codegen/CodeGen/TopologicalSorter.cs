using System.Collections.Generic;
using System.Linq;

namespace Avro.codegen.CodeGen
{
    public class TopologicalSorter
    {
        private readonly IList<string> _visited = new List<string>();

        private readonly IList<Record> _sorted = new List<Record>();

        private IDictionary<string, Record> _recordMap = new Dictionary<string, Record>();

        public IList<Record> Sort(IList<Record> records)
        {
            _recordMap = records.ToDictionary(x => x.Fullname, x => x);

            foreach (var record in records)
            {
                if (_visited.Contains(record.Fullname)) continue;

                Visit(record);
            }

            return _sorted;
        }

        private void Visit(Record record)
        {
            if (_visited.Contains(record.Fullname)) return;

            _visited.Add(record.Fullname);

            foreach (var dependency in record.Dependencies)
                Visit(_recordMap[dependency]);

            _sorted.Add(record);
        }
    }
}