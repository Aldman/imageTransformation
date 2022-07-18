using System;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */

        public static double[,] MedianFilter(double[,] original)
        {
            var originalHeight = original.GetLength(0);
            var originalWidth = original.GetLength(1);
            var medians = new double[originalHeight, originalWidth];

            for (int heightPosition = 0; heightPosition < originalHeight; heightPosition++)
                for (int widthPosition = 0; widthPosition < originalWidth; widthPosition++)
                {
                    List<double> sortedNearPixels
                        = GetSortedNearPixels(heightPosition, widthPosition,
                        originalWidth, originalHeight, original);
                    medians[heightPosition, widthPosition]
                        = GetMedian(sortedNearPixels);
                    sortedNearPixels.Clear();
                }
            return medians;
        }

        public static double GetMedian(List<double> sortedNearPixels)
        {
            var lengthList = sortedNearPixels.Count;
            if (lengthList % 2 == 0)
            {
                double arithmeticMeanOfMiddle
                    = (sortedNearPixels[lengthList / 2]
                    + sortedNearPixels[lengthList / 2 - 1]) / 2;
                return arithmeticMeanOfMiddle;
            }
            else
                return sortedNearPixels[lengthList / 2];
        }

        public static List<double> GetSortedNearPixels(
            int heightPosition, int widthPosition,
            int width, int height, double[,] original)
        {
            var nearPixels = new List<double>();
            bool canUp = false, canLeft = false,
                canRight = false;
            nearPixels.Add(original[heightPosition, widthPosition]);
            canUp = CanUpAndAddUpperPixel(nearPixels, heightPosition, widthPosition,
                width, height, original);
            canLeft = CanLeftAndAddLefterAndCornerPixels(nearPixels,
                heightPosition, widthPosition, canUp, original);
            canRight = CanRightAndAddRighterAndCornerPixels(
                nearPixels, heightPosition, widthPosition, canUp,
                width, original);
            AddDownLinePixels(nearPixels,
                heightPosition, widthPosition,
                canRight, canLeft,
                height, original);
            nearPixels.Sort();
            return nearPixels;
        }

        public static void AddDownLinePixels(List<double> nearPixels,
            int heightPosition, int widthPosition,
            bool canRight, bool canLeft,
            int height, double[,] original)
        {
            if (heightPosition + 1 < height)
            {
                nearPixels.Add(original[
                    heightPosition + 1,
                    widthPosition]);
                if (canLeft)
                    nearPixels.Add(original[
                        heightPosition + 1,
                        widthPosition - 1]);
                if (canRight)
                    nearPixels.Add(original[
                        heightPosition + 1,
                        widthPosition + 1]);
            }
        }

        public static bool CanRightAndAddRighterAndCornerPixels(List<double> nearPixels,
            int heightPosition, int widthPosition, bool canUp,
            int width, double[,] original)
        {
            if (widthPosition + 1 < width)
            {
                nearPixels.Add(original[heightPosition, widthPosition + 1]);
                if (canUp)
                    nearPixels.Add(original[heightPosition - 1,
                        widthPosition + 1]);
                return true;
            }
            return false;
        }

        public static bool CanLeftAndAddLefterAndCornerPixels(List<double> nearPixels,
            int heightPosition, int widthPosition, bool canUp, double[,] original)
        {
            if (widthPosition - 1 >= 0)
            {
                nearPixels.Add(original[heightPosition, widthPosition - 1]);
                if (canUp)
                    nearPixels.Add(original[heightPosition - 1, widthPosition - 1]);
                return true;
            }
            return false;
        }

        public static bool CanUpAndAddUpperPixel(List<double> nearPixels,
            int heightPosition, int widthPosition,
            int width, int height, double[,] original)
        {
            if (heightPosition - 1 >= 0)
            {
                nearPixels.Add(original[heightPosition - 1, widthPosition]);
                return true;
            }
            return false;
        }

    }
}