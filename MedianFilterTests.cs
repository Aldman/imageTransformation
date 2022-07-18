using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Recognizer
{
    [TestFixture]
    public class MedianFilterTests
    {
        public static IEnumerable<TestCaseData> MedianFilterMethodTestsParams
        {
            get
            {
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var expectedResult = new double[,]
                {
                    {3, 3.5, 4 },
                    {4.5, 5, 5.5 },
                    {6, 6.5, 7 }
                };
                yield return new TestCaseData(original, expectedResult)
                    .SetName("Median from image 3x3");

                original = new double[,]
                {
                    {0.1},
                    {0.3}
                };
                expectedResult = new double[,]
                {
                    {0.2},
                    {0.2}
                };
                yield return new TestCaseData(original, expectedResult)
                    .SetName("Median from image 1x2");
            }
        }

        [TestCaseSource("MedianFilterMethodTestsParams")]
        public void MedianFilterMethodTests
            (double[,] original, double[,] expectedResult)
        {
            var actualResult = MedianFilterTask.MedianFilter(original);

            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<TestCaseData>
            CanUpAndAddUpperPixelTestCaseData
        {
            get
            {
                var nearPixels = new List<double>() { 5 };
                int heightPosition = 1, widthPosition = 1,
                width = 3, height = 3;
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var resultNearPixels = new List<double>() { 5, 2 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, width, height,
                    original, resultNearPixels).SetName("Add upper value test");

                nearPixels = new List<double>() { 8 };
                heightPosition = 2; widthPosition = 1;
                width = 3; height = 3;
                resultNearPixels = new List<double>() { 8, 5 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, width, height,
                    original, resultNearPixels).SetName("Add upper value with lower number test");

                nearPixels = new List<double>() { 2 };
                heightPosition = 0; widthPosition = 1;
                width = 3; height = 3;
                resultNearPixels = new List<double>() { 2 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, width, height,
                    original, resultNearPixels).SetName("Add upper value with upper number test");
            }
        }

        [TestCaseSource("CanUpAndAddUpperPixelTestCaseData")]
        public void CanUpAndAddUpperPixelTests(List<double> nearPixels,
            int heightPosition, int widthPosition,
            int width, int height, double[,] original,
            List<double> resultNearPixels)
        {
            MedianFilterTask.CanUpAndAddUpperPixel(
                nearPixels, heightPosition, widthPosition, width,
                height, original);
            Assert.AreEqual(resultNearPixels, nearPixels);
        }

        public static IEnumerable<TestCaseData> CanLeftAndAddLefterAndCornerPixelsCaseData
        {
            get
            {
                var nearPixels = new List<double>() { 5, 2 };
                int heightPosition = 1, widthPosition = 1;
                var canUp = true;
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var resultNearPixels = new List<double>() { 5, 2, 4, 1 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, canUp,
                    original, resultNearPixels)
                    .SetName("Add lefter and corner values test");

                nearPixels = new List<double>() { 2 };
                heightPosition = 0; widthPosition = 1;
                canUp = false;
                resultNearPixels = new List<double>() { 2, 1 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, canUp,
                    original, resultNearPixels)
                    .SetName("Add only lefter value test");
            }
        }

        [Test]
        [TestCaseSource("CanLeftAndAddLefterAndCornerPixelsCaseData")]
        public void CanLeftAndAddLefterAndCornerPixels(List<double> nearPixels,
            int heightPosition, int widthPosition, bool canUp, double[,] original,
            List<double> resultNearPixels)
        {
            MedianFilterTask.CanLeftAndAddLefterAndCornerPixels(nearPixels,
                heightPosition, widthPosition, canUp, original);
            Assert.AreEqual(resultNearPixels, nearPixels);
        }

        public static IEnumerable<TestCaseData> CanRightAndAddRighterAndCornerPixelsCaseData
        {
            get
            {
                var nearPixels = new List<double>() { 5, 2 };
                int heightPosition = 1, widthPosition = 1,
                    width = 3;
                var canUp = true;
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var resultNearPixels = new List<double>() { 5, 2, 6, 3 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, canUp, width, original, resultNearPixels)
                    .SetName("Add righter and corner pixels");

                nearPixels = new List<double>() { 2 };
                heightPosition = 0; widthPosition = 1;
                canUp = false;
                resultNearPixels = new List<double>() { 2, 3 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, canUp, width, original, resultNearPixels)
                    .SetName("Add only righter pixel");
            }
        }

        [TestCaseSource("CanRightAndAddRighterAndCornerPixelsCaseData")]
        public void CanRightAndAddRighterAndCornerPixelsTests(List<double> nearPixels,
            int heightPosition, int widthPosition, bool canUp,
            int width, double[,] original,
            List<double> resultNearPixels)
        {
            MedianFilterTask.CanRightAndAddRighterAndCornerPixels(
                nearPixels, heightPosition, widthPosition, canUp,
                width, original);
            Assert.AreEqual(resultNearPixels, nearPixels);
        }

        public static IEnumerable<TestCaseData> AddDownLinePixelsCaseData
        {
            get
            {
                var nearPixels = new List<double>() { 5 };
                int heightPosition = 1, widthPosition = 1,
                    height = 3;
                bool canRight = true, canLeft = true;
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var resultNearPixels = new List<double>() { 5, 8, 7, 9 };
                yield return new TestCaseData(nearPixels, heightPosition,
                    widthPosition, canRight, canLeft, height,
                    original, resultNearPixels)
                    .SetName("Add down line");
            }
        }

        [TestCaseSource("AddDownLinePixelsCaseData")]
        public void AddDownLinePixelsTests(List<double> nearPixels,
            int heightPosition, int widthPosition,
            bool canRight, bool canLeft,
            int height, double[,] original,
            List<double> resultNearPixels)
        {
            MedianFilterTask.AddDownLinePixels(
                nearPixels, heightPosition, widthPosition,
                canRight, canLeft, height, original);
            Assert.AreEqual(resultNearPixels, nearPixels);
        }

        public static IEnumerable<TestCaseData> GetSortedNearPixelsCaseData
        {
            get
            {
                int heightPosition = 1, widthPosition = 1,
                    height = 3, width = 3;
                var original = new double[,]
                {
                    {1, 2, 3 },
                    {4, 5, 6 },
                    {7, 8, 9 }
                };
                var sortedPixels = new List<double>()
                { 1, 2, 3, 4, 5, 6, 7, 8, 9};
                yield return new TestCaseData(heightPosition,
                    widthPosition, width, height, original,
                    sortedPixels)
                    .SetName("GetSortedList1");
            }
        }

        [TestCaseSource("GetSortedNearPixelsCaseData")]
        public void GetSortedNearPixelsTests(
            int heightPosition, int widthPosition,
            int width, int height, double[,] original,
            List<double> expectedList)
        {
            var result = MedianFilterTask.GetSortedNearPixels(
                heightPosition, widthPosition, width,
                height, original);
            Assert.AreEqual(expectedList, result);
        }

        public static IEnumerable<TestCaseData> GetMedianCaseData
        {
            get
            {
                var sortedPixels = new List<double>()
                { 1, 2, 3, 4, 5, 6, 7, 8, 9};
                double expectedMedian = 5;
                yield return new TestCaseData(
                    sortedPixels, expectedMedian)
                    .SetName("GetMedian1");
            }
        }

        [TestCaseSource("GetMedianCaseData")]
        public void GetMedianTests(List<double> sortedNearPixels,
            double expectedMedian)
        {
            var result = MedianFilterTask.GetMedian(sortedNearPixels);
            Assert.AreEqual(expectedMedian, result);
        }
    }
}
