using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using MindFrame.Core.Primitives;
using MindFrame.Validation;

namespace MindFrame.Tests
{
    public class ValidationTests
    {
        [Test]
        public void TimestampSequenceValidator_DetectsNonMonotonic()
        {
            var seq = new List<MindTimestamp>
            {
                new MindTimestamp(1.0f),
                new MindTimestamp(2.0f),
                new MindTimestamp(1.5f),
                new MindTimestamp(3.0f)
            };
            var validator = new TimestampSequenceValidator();
            var issues = validator.Validate(seq).ToList();
            Assert.IsTrue(issues.Any(i => i.Code == "timestamps.nonMonotonic"));
        }
    }
}
