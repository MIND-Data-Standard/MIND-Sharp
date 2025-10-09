using System.Collections.Generic;
using System.IO;
using MindFrame.Serialization;
namespace MindFrame.ModelIO
{
    public static class JsonlExporter
    {
        public static void WriteRecords<T>(IEnumerable<T> records, TextWriter writer)
        {
            if (writer == null || records == null) return;
            foreach (var r in records)
            { writer.WriteLine(MindJson.Serialize(r)); }
        }
        public static void WriteRecord<T>(T record, TextWriter writer)
        { if (writer == null) return; writer.WriteLine(MindJson.Serialize(record)); }
    }
}

