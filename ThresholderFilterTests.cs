using System.Collections.Generic;
using NUnit.Framework;

namespace Recognizer
{
    public class ThresholderFilterTests
    {
        

        public static IEnumerable<TestCaseData> FiltersTestsCaseData
        {
            get
            {
                var original = new double[,]
                {
                    {0.3 }
                };
                double whitePixelsFraction = 0.0;
                var expectedResult = new double[,]
                {
                    {0}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Test threshold filter with 1x1");

                original = new double[,]
                {
                    {123 }
                };
                whitePixelsFraction = 0.0;
                expectedResult = new double[,]
                {
                    {0}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Test threshold filter with 1x1 with black color");

                original = new double[,]
                {
                    {123 }
                };
                whitePixelsFraction = 1;
                expectedResult = new double[,]
                {
                    {1}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Test threshold filter with 1x1 with white color");

                whitePixelsFraction = 0.5;
                expectedResult = new double[,]
                {
                    {0}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Threshold filter 1x1 with 0,5 whitePixelsFraction");

                original = new double[,]
                {
                    {1, 2 }
                };
                whitePixelsFraction = 1;
                expectedResult = new double[,]
                {
                    {1, 1}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Threshold filter 1x2 with 1 whitePixelsFraction");

                original = new double[,]
                {
                    {1, 0 }
                };
                whitePixelsFraction = 0.5;
                expectedResult = new double[,]
                {
                    {1, 0}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Threshold filter 1x2 with 0.5 whitePixelsFraction");

                original = new double[,]
                {
                    {1, 2 }
                };
                whitePixelsFraction = 0.5;
                expectedResult = new double[,]
                {
                    {0, 1}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Threshold filter 1x2 with 0.5 whitePixelsFraction(2)");

                original = new double[,]
                {
                    {1, 2, 2, 3 }
                };
                whitePixelsFraction = 0.5;
                expectedResult = new double[,]
                {
                    {0, 1, 1, 1}
                };
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("Threshold filter 1x4 with 0.5 whitePixelsFraction");
            }
        }

        [TestCaseSource("FiltersTestsCaseData")]
        public void ThresholdFilter(double[,] original, 
            double whitePixelsFraction, double[,] expectedResult)
        {
            var actualResult = ThresholdFilterTask
                .ThresholdFilter(original, whitePixelsFraction);
            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<TestCaseData> GetThreshholdColorCaseData
        {
            get
            {
                var original = new double[,]
                {
                    {0.3 }
                };
                double whitePixelsFraction = 0.0;
                double expectedResult = double.MaxValue;
                yield return new TestCaseData(original, whitePixelsFraction, expectedResult)
                    .SetName("GetThreshholdColor test1");
            }
        }

        [TestCaseSource("GetThreshholdColorCaseData")]
        public void GetThreshholdColorTests(double[,] original, 
            double whitePixelsFraction, double expectedResult)
        {
            var actualResult = ThresholdFilterTask
                .GetThreshholdColor(original, whitePixelsFraction);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
