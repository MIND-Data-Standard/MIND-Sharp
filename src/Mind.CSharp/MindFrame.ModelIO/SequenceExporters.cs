using System.Collections.Generic;
using System.IO;
using MindFrame.Core.Primitives;
using MindFrame.Core.Types;
namespace MindFrame.ModelIO
{
    public static class SequenceExporters
    {
        public static void WriteTimestamps(IEnumerable<MindTimestamp> values, TextWriter writer) => JsonlExporter.WriteRecords(values, writer);
        public static void WriteVector3s(IEnumerable<MindVector3> values, TextWriter writer) => JsonlExporter.WriteRecords(values, writer);
        public static void WriteQuaternions(IEnumerable<MindQuaternion> values, TextWriter writer) => JsonlExporter.WriteRecords(values, writer);
        public static void WritePoses(IEnumerable<MindPose> values, TextWriter writer) => JsonlExporter.WriteRecords(values, writer);
        public static void WriteTransforms(IEnumerable<MindTransform> values, TextWriter writer) => JsonlExporter.WriteRecords(values, writer);
        public static void Write<T>(IEnumerable<T> records, TextWriter writer) => JsonlExporter.WriteRecords(records, writer);
    }
}

