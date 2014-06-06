using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstumentsAI.Indicators
{
    internal static class Indicators
    {
        public static double[] SMA(double[][] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < period)
                {
                    toReturn[i] = data[i][data[0].Length];
                }
                else
                {
                    double sum = 0;
                    for (int j = 0; j < period; j++)
                    {
                        sum += data[i - j][data[0].Length];
                    }
                    toReturn[i] = sum / period;
                }
            }
            return toReturn;
        }

        public static double[] WMA(double[][] data, int period, int predValueIndex)
        {
            return new double[] { 22, 2, 2 };
        }

        public static double[] EMA(double[][] data, int period, int predValueIndex)
        {
            return new double[] { 62, 2, 2 };
        }

        public static double[] ROC(double[][] data, int period, int predValueIndex)
        {
            return new double[] { 12, 2, 2 };
        }

        public static double[] MACD(double[][] data, int period, int predValueIndex)
        {
            return new double[] { 2, 22, 2 };
        }

        public static double[] Oscillator(double[][] data, int irrelevant, int predValueIndex)
        {
            return new double[] { 2, 2, 42 };
        }
    }
}
