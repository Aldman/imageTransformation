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
            
            
            return new double[original.GetLength(0), original.GetLength(1)];
		}

        public static double[,] GetBWColors
            (double[,] original, double thresholdColor)
        {
            //TODO: Копировать оригинальный массив в новый,
            //  заменить все цвета >= пороговому на белый, вернуть

            var bwResult = new double[original.GetLength(0),
                original.GetLength(1)];
            Array.Copy(original, bwResult, original.Length);
            return bwResult;
        }


        public static double GetThreshholdColor
            (double[,] original, double whitePixelsFraction)
        {
            var whitePixelsCountMin = (int)(original.Length
                * whitePixelsFraction);
            var originalList = new List<double>();
            originalList.AddRange(originalList);
            originalList.Sort();
            return originalList[whitePixelsCountMin];            
        }
	}
}