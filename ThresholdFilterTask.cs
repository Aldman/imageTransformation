using System.Collections.Generic;
using System;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var thresholdColor = GetThreshholdColor
                (original, whitePixelsFraction);
            return GetBWColors(original, thresholdColor);
        }

        public static double[,] GetBWColors
            (double[,] original, double thresholdColor)
        {
            var bwResult = new double[original.GetLength(0),
                original.GetLength(1)];
            var height = original.GetLength(0);
            var width = original.GetLength(1);
            Array.Copy(original, bwResult, original.Length);
            for (int heightPosition = 0; heightPosition < height; heightPosition++)
                for (int widthPosition = 0; widthPosition < width; widthPosition++)
                    bwResult[heightPosition, widthPosition]
                        = ChangeCodeColorToBW(
                            original[heightPosition, widthPosition]
                            , thresholdColor);
            return bwResult;
        }

        private static double ChangeCodeColorToBW(double originalColorCode, double thresholdColor)
        {
            if (originalColorCode <= thresholdColor)
                return 0;
            else return 1;
        }

        public static double GetThreshholdColor
            (double[,] original, double whitePixelsFraction)
        {
            var whitePixelsCountMin = (int)(original.Length
                * whitePixelsFraction);
            if (whitePixelsCountMin == 0)
                return double.MaxValue;
            else if (whitePixelsFraction == 1)
                return 0;
            var uniqueColors = GetUniqueColors(original);
            uniqueColors.Sort();
            uniqueColors.Reverse();
            var colorPosition = (int)(uniqueColors.Count * whitePixelsFraction);
            var colorForReturn = uniqueColors[colorPosition];
            if (whitePixelsCountMin > CalculateWhitePixelsCount(original, colorForReturn))
                colorForReturn = uniqueColors[colorPosition + 1];
            return colorForReturn;
        }

        private static int CalculateWhitePixelsCount
            (double[,] original, double thresholdColor)
        {
            var whitePixels = new List<double>();
            foreach (var pixel in original)
                if (pixel > thresholdColor)
                    whitePixels.Add(pixel);
            return whitePixels.Count;
        }

        public static List<double> GetUniqueColors(double[,] original)
        {
            var uniqueColors = new List<double>();
            foreach (var color in original)
                if (!uniqueColors.Contains(color))
                    uniqueColors.Add(color);
            return uniqueColors;
        }
    }
}