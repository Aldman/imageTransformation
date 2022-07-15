using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Recognizer
{
    [TestFixture]
    public class GrayscaleTaskTests
    {
        public static IEnumerable<TestCaseData> Tests
        {
            get
            {
                var testImage1 = new Pixel[,] { { new Pixel(10, 10, 10) } };
                var expectedResult = new double[,] { { GrayscaleTask.GetGrayBright(new Pixel(10, 10, 10)) } };
                yield return new TestCaseData(testImage1, expectedResult).SetName("Image 3x3");
                testImage1 = new Pixel[,]
                {
                    {new Pixel(164, 97, 211), new Pixel(174, 0, 49), new Pixel(234, 35, 28), new Pixel(134, 204, 87), new Pixel(234, 54, 178) },
                    {new Pixel(35, 218, 223), new Pixel(255, 97, 130), new Pixel(89, 165, 38), new Pixel(135, 187, 22), new Pixel(116, 86, 251) },
                    {new Pixel(99, 7, 160), new Pixel(240, 122, 136), new Pixel(245, 166, 222), new Pixel(38, 214, 132), new Pixel(184, 43, 85) }
                };
                expectedResult = new double[,] 
                {
                    { GrayscaleTask.GetGrayBright(new Pixel(164, 97, 211)), GrayscaleTask.GetGrayBright(new Pixel(174, 0, 49)),
                    GrayscaleTask.GetGrayBright(new Pixel(234, 35, 28)), GrayscaleTask.GetGrayBright(new Pixel(134, 204, 87)),
                    GrayscaleTask.GetGrayBright(new Pixel(234, 54, 178))},

                    {
                        GrayscaleTask.GetGrayBright(new Pixel(35, 218, 223)), GrayscaleTask.GetGrayBright(new Pixel(255, 97, 130)),
                    GrayscaleTask.GetGrayBright(new Pixel(89, 165, 38)), GrayscaleTask.GetGrayBright(new Pixel(135, 187, 22)),
                    GrayscaleTask.GetGrayBright(new Pixel(116, 86, 251))
                    },

                    {
                        GrayscaleTask.GetGrayBright(new Pixel(99, 7, 160)), GrayscaleTask.GetGrayBright(new Pixel(240, 122, 136)),
                    GrayscaleTask.GetGrayBright(new Pixel(245, 166, 222)), GrayscaleTask.GetGrayBright(new Pixel(38, 214, 132)),
                    GrayscaleTask.GetGrayBright(new Pixel(184, 43, 85))
                    }
                };
                yield return new TestCaseData(testImage1, expectedResult).SetName("Image 3x5");
            }
        }

        [TestCaseSource("Tests")]
        public void ToGrayscaleTests(Pixel[,]image, double [,] expeсtedGrayscale)
        {
            var result = GrayscaleTask.ToGrayscale(image);
            Assert.AreEqual(expeсtedGrayscale, result);
        }
    }

    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var widthOfArray = original.GetLength(0);
            var heightOfArray = original.GetLength(1);
            var grayscale = new double[widthOfArray, heightOfArray];
            for (int i = 0; i < widthOfArray; i++)
                for (int j = 0; j < heightOfArray; j++)
                    grayscale[i, j] = GetGrayBright(original[i, j]);
                
            return grayscale;
        }

        public static double GetGrayBright (Pixel color)
        {
            var brightPixel = (0.299 * color.R
                        + 0.587 * color.G
                        + 0.114 * color.B) / 255;
            if (brightPixel > 1)
                brightPixel = 1;
            return brightPixel;
        }
    }
}