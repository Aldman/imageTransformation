using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Recognizer
{
    [TestFixture]
    class SobelFilterTests
    {
        public static IEnumerable<TestCaseData> GetTranspositionArrayCaseData
        {
            get
            {
                var arrayToTransposition = new double[,]
                {
                    { 7, 3, -12, 0, 34}
                };
                var expectedResult = new double[,]
                {
                    { 7},
                    {3 },
                    {-12 },
                    {0 },
                    {34 }
                };
                yield return new TestCaseData(arrayToTransposition, expectedResult)
                    .SetName("Transposition vector");

                arrayToTransposition = new double[,]
                {
                    { -1, 0, 2},
                    {-5, 4, -7 },
                    {6, -4, -6 }
                };
                expectedResult = new double[,]
                {
                    { -1, -5, 6},
                    {0, 4, -4 },
                    {2, -7, -6 }
                };
                yield return new TestCaseData(arrayToTransposition, expectedResult)
                    .SetName("Transposition 3x3");
            }


        }

        [TestCaseSource("GetTranspositionArrayCaseData")]
        public void GetTranspositionArrayTests(double[,] arrayToTransposition, double[,] expectedResult)
        {
            var actualResult = SobelFilterTask.GetTranspositionArray(arrayToTransposition);
            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<TestCaseData> Mul2DimensionalArrayToNumberCaseData
        {
            get
            {
                var arrayToMul = new double[,]
                {
                    { 7, 3, -12, 0, 34}
                };
                double numberToMul = 10;
                var expectedResult = new double[,]
                {
                    { 70, 30, -120, 0, 340}
                };
                yield return new TestCaseData
                    (arrayToMul, numberToMul, expectedResult)
                    .SetName("Mul vector to 10");

                arrayToMul = new double[,]
                {
                    { -1, 0, 2},
                    {-5, 4, -7 },
                    {6, -4, -6 }
                };
                expectedResult = new double[,]
                {
                    { -10, 0, 20},
                    {-50, 40, -70 },
                    {60, -40, -60 }
                };
                yield return new TestCaseData
                    (arrayToMul, numberToMul, expectedResult)
                    .SetName("Mul array 3x3 to 10");
            }
        }

        [TestCaseSource("Mul2DimensionalArrayToNumberCaseData")]
        public void Mul2DimensionalArrayToNumberTests(
            double[,] arrayToMul, double numberToMul, double[,] expectedResult)
        {
            var actualResult = SobelFilterTask
                .Mul2DimensionalArrayToNumber(arrayToMul, numberToMul);
            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<TestCaseData> GetSquareSubArrayCaseData
        {
            get
            {
                var originalArray = new double[,]
                {
                    { -1, 0, 2},
                    {-5, 4, -7 },
                    {6, -4, -6 }
                };
                int subArraySide = 1;
                int x = 1, y = 1;
                var expectedResult = new double[,]
                {
                    { 4}
                };
                yield return new TestCaseData
                    (originalArray, subArraySide, x, y, expectedResult)
                    .SetName("Get subarray 1x1 from 3x3");

                //originalArray = new double[,]
                //{
                //    { -1, 0, 2},
                //    {-5, 4, -7 },
                //    {6, -4, -6 }
                //};
                subArraySide = 3;
                x = 1; y = 1;
                expectedResult = new double[,]
                {
                    { -1, 0, 2},
                    {-5, 4, -7 },
                    {6, -4, -6 }
                };
                yield return new TestCaseData
                    (originalArray, subArraySide, x, y, expectedResult)
                    .SetName("Get subarray 3x3 from 3x3");

                originalArray = new double[,]
                {
                    { -1, 0, 2, 3, 4, 5, 6, 7, 8},
                    {-5, 4, -7, 6, 5, 4, 3, 2, 1 },
                    {6, -4, -6, 5, 4, 3, 2, 1, 0 },
                    { -1, 0, 2, 3, 4, 5, 6, 7, 8},//
                    {-5, 4, -7, 6, 5, 4, 3, 2, 1 },
                    {6, -4, -6, 5, 4, 3, 2, 1, 0 },
                    { -1, 0, 2, 3, 4, 5, 6, 7, 8},
                    {-5, 4, -7, 6, 5, 4, 3, 2, 1 },
                    {6, -4, -6, 5, 4, 3, 2, 1, 0 }
                };
                subArraySide = 3;
                x = 3; y = 3;
                expectedResult = new double[,]
                {
                    { -6, 5, 4},
                    {  2, 3, 4 },
                    { -7, 6, 5 }
                };
                yield return new TestCaseData
                    (originalArray, subArraySide, x, y, expectedResult)
                    .SetName("Get subarray 3x3 from 9x9");
            }
        }

        [TestCaseSource("GetSquareSubArrayCaseData")]
        public void GetSquareSubArrayTests
            (double[,] originalArray, int subArraySide,
            int x, int y, double[,] expectedResult)
        {
            var actualResult = SobelFilterTask
                .GetSquareSubArray(originalArray, subArraySide, x, y);
            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<TestCaseData> CalculateElByElSquareArrayMulWithAddCaseData
        {
            get
            {
                var array1 = new double[,]
                {
                    { 1, 2, 3}
                };
                var array2 = new double[,]
                {
                    { 10, 1, 0}
                };
                double expectedNumber = 12;
                yield return new TestCaseData(array1, array2, expectedNumber)
                    .SetName("Simple mul with add");
            }
        }

        [TestCaseSource("CalculateElByElSquareArrayMulWithAddCaseData")]
        public void CalculateElByElSquareArrayMulWithAddTests
            (double[,] array1, double[,] array2, double expectedResult)
        {
            var actualResult = SobelFilterTask
                .CalculateElByElSquareArrayMulWithAdd(array1, array2);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
