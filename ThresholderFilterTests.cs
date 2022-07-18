using System.Collections.Generic;
using NUnit.Framework;

namespace Recognizer
{
    public class ThresholderFilterTests
    {
        public static IEnumerable<TestCaseData> GetBWColorsCaseData
        {
            get
            {
                var original = new double[,]
                {
                    {0, 1, 2 },
                    {3, 4, 5 }
                };
                double thresholdColor = 1.0;
                var expectedResult = new double[,]
                {
                    {0, 1, 2 },
                    {3, 4, 5 }
                };
                yield return new TestCaseData(
                    original, thresholdColor, expectedResult)
                    .SetName("First method check");
            }
        }

        [TestCaseSource("GetBWColorsCaseData")]
        public void GetBWColorsTests (double[,] original, 
            double thresholdColor, double[,] expected)
        {
            var actualResult = ThresholdFilterTask
                .GetBWColors(original, thresholdColor);
            Assert.AreEqual(expected, actualResult);
        }
    }
}
