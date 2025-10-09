using NUnit.Framework;
using System.IO;
using System.Text;
using System.Collections.Generic;
using MindFrame.Core.Primitives;
using MindFrame.ModelIO;

namespace MindFrame.Tests
{
    public class ModelIOTests
    {
        [Test]
        public void JsonlExporter_WritesOneLinePerRecord()
        {
            var records = new List<MindVector3>
            {
                new MindVector3(1,2,3),
                new MindVector3(4,5,6)
            };
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            JsonlExporter.WriteRecords(records, writer);
            var output = sb.ToString().TrimEnd('\n','\r');
            var lines = output.Split('\n');
            Assert.AreEqual(2, lines.Length);
            Assert.IsTrue(lines[0].Contains("\"x\"")); // basic sanity
        }
    }
}
