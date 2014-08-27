using System;
using System.IO;
using Avro.codegen.CodeGen;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Avro.Specs
{
    [TestFixture]
    public class When_sort_dependency_with_topological_sort
    {
        [Test]
        public void Should_return_all_sorted_dependencies()
        {
            var gen = new AvroCodeGenerator();

            gen.Generate(@"D:\ETP\ETP-CTP-Release\CodeGen\Energistics", @"D:\ETP\ETP-CTP-Release\CodeGen\src");
        }

        [Test]
        public void Should_return_all_fields_in_schema()
        {
            var record = JsonConvert.DeserializeObject<Record>(File.ReadAllText("Schemas/ChannelMetadataRecord.avsc"));
            Assert.AreEqual(10, record.Fields.Count);
            //Assert.AreEqual(3, record.Dependencies.Count);

            var indexes = record.Fields[2];

            Assert.AreEqual(1, indexes.Dependencies.Count);

        }
    }
}
