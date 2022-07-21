using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = GetTranspositionArray(sx);
            var sxSide = sx.GetLength(0);
            var deltaSx = (sxSide / 2);
            for (int x = deltaSx; x < width - deltaSx; x++)
                for (int y = deltaSx; y < height - deltaSx; y++)
                {
                    var subArray = GetSquareSubArray(g, sxSide, x, y);
                    var gx = CalculateElByElSquareArrayMulWithAdd
                        (sx, subArray);
                    var gy = CalculateElByElSquareArrayMulWithAdd
                        (sy, subArray);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        public static double CalculateElByElSquareArrayMulWithAdd
            (double[,] array1, double[,] array2)
        {
            if((array1.GetLength(0) != array2.GetLength(0))
                || (array1.GetLength(1) != array2.GetLength(1)))
                throw new Exception("Размеры массивов не одинаковы");

            var width = array1.GetLength(0);
            var height = array1.GetLength(1);
            var elByElMulledArray = new double[width, height];
            for (int heightPosition = 0; heightPosition < height; heightPosition++)
                for (int widthPosition = 0; widthPosition < width; widthPosition++)
                    elByElMulledArray[widthPosition, heightPosition]
                        = array1[widthPosition, heightPosition]
                        * array2[widthPosition, heightPosition];
            double numberForReturning = 0;
            foreach (var number in elByElMulledArray)
                numberForReturning += number;
            return numberForReturning;
        }

        public static double[,] GetSquareSubArray
            (double[,] originalArray, int subArraySide,
            int x, int y)
        {
            var width = originalArray.GetLength(0);
            var height = originalArray.GetLength(1);
            var deltaSubArraySide = (int)(subArraySide / 2);
            if ((x + deltaSubArraySide >= width)
                || (x - deltaSubArraySide < 0)
                || (y + deltaSubArraySide >= height)
                || (y - deltaSubArraySide < 0))
                throw new Exception("Оригинальный массив меньше подмассива");

            var subArray = new double[subArraySide, subArraySide];
            int xSub = 0;
            for (int xOriginal = x - deltaSubArraySide; xOriginal <= x + deltaSubArraySide; xOriginal++)
            {
                int ySub = 0;
                for (int yOriginal = y - deltaSubArraySide; yOriginal <= y + deltaSubArraySide; yOriginal++)
                {
                    subArray[xSub, ySub] = originalArray[xOriginal, yOriginal];
                    ySub++;
                }
                xSub++;
            }
            return subArray;
        }

        public static double[,] Mul2DimensionalArrayToNumber(
            double[,] arrayToMul, double numberToMul)
        {
            var width = arrayToMul.GetLength(0);
            var height = arrayToMul.GetLength(1);
            var arrayToReturn = new double[width, height];
            for (int heightPosition = 0; heightPosition < height; heightPosition++)
                for (int widthPosition = 0; widthPosition < width; widthPosition++)
                    arrayToReturn[widthPosition, heightPosition]
                        = arrayToMul[widthPosition, heightPosition] * numberToMul;
            return arrayToReturn;
        }

        public static double[,] GetTranspositionArray(double[,] arrayToTransposition)
        {
            var width = arrayToTransposition.GetLength(0);
            var height = arrayToTransposition.GetLength(1);
            var arrayToReturn = new double[height, width];

            for (int heightPosition = 0; heightPosition < height; heightPosition++)
                for (int widthPosition = 0; widthPosition < width; widthPosition++)
                {
                    arrayToReturn[heightPosition, widthPosition]
                        = arrayToTransposition[widthPosition, heightPosition];
                }
            return arrayToReturn;
        }
    }
}