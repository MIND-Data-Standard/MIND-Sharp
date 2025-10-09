using NUnit.Framework;
using MindFrame.Core.Primitives;
using MindFrame.Serialization;

namespace MindFrame.Tests
{
    public class CoreSerializationTests
    {
        [Test]
        public void Vector3_SerializeRoundTrip()
        {
            var v = new MindVector3(1.23f, -4.5f, 6.78f);
            string json = MindJson.Serialize(v);
            var v2 = MindJson.Deserialize<MindVector3>(json);
            Assert.AreEqual(v.X, v2.X);
            Assert.AreEqual(v.Y, v2.Y);
            Assert.AreEqual(v.Z, v2.Z);
        }
    }
}
