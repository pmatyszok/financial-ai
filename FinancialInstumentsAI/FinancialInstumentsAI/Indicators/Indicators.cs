using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstumentsAI.Indicators
{
    internal static class Indicators
    {
        public static double[] SMA(double[] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            for (int i = 0; i < predValueIndex; i++)
            {
                if (i < period)
                {
                    toReturn[i] = data[i];
                }
                else
                {
                    double sum = 0;
                    for (int j = 0; j < period; j++)
                    {
                        sum += data[i - j];
                    }
                    toReturn[i] = sum / period;
                }
            }
            return toReturn;
        }

        public static double[] WMA(double[] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            for (int i = 0; i < predValueIndex; i++)
            {
                if (i < period)
                {
                    toReturn[i] = data[i];
                }
                else
                {
                    double sum = 0;
                    double wSum = 0;
                    for (int j = 0; j < period; j++)
                    {
                        sum += data[i - j]*(period-j);
                        wSum += (period - j);
                    }
                    toReturn[i] = sum / period;
                }
            }
            return toReturn;
        }

        public static double[] EMA(double[] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            toReturn[0] = data[0] * (2.0 / (period + 1.0));
            for (int i = 1; i < predValueIndex; i++)
            {
                toReturn[i] = (data[i] * (2.0 / (period + 1.0))) + (toReturn[i-1]*(1.0-((2.0 / (period + 1.0)))));
            }
            return toReturn;
        }

        public static double[] ROC(double[] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            for (int i = 0; i < predValueIndex; i++)
            {
                if (i < period)
                {
                    toReturn[i] = data[i] / data[i] - 1;
                }
                else
                {
                    toReturn[i] = data[i] / data[i-period] - 1;
                }
            }
            return toReturn;
        }

        public static double[] MACD(double[] data, int period, int predValueIndex)
        {
            double[] ema1 = EMA(data,period,predValueIndex);
            double[] ema2 = EMA(data, period/2, predValueIndex);
            double[] macd = new double[predValueIndex];
            double[] toReturn = new double[predValueIndex];
            for (int i = 0; i < predValueIndex; i++)
            {
                macd[1] = ema2[i] / ema1[i];
            }
            toReturn[0] = macd[0] * (2 / (period + 1));
            for (int i = 1; i < predValueIndex; i++)
            {
                toReturn[i] = (macd[i] * (2 / (period + 1))) + (toReturn[i - 1] * (1 - ((2 / (period + 1)))));
            }
            return toReturn;
        }

        public static double[] Oscillator(double[] data, int period, int predValueIndex)
        {
            double[] toReturn = new double[predValueIndex];
            toReturn[0] = 0;
            for (int i = 1; i < predValueIndex; i++)
            {
                if (i < period)
                {
                    double[] sub = SubArray<double>(data, 0, i);
                    if (sub.Max() != sub.Min())
                        toReturn[i] = (data[i] - sub.Min()) / (sub.Max() - sub.Min()) * 100;
                }
                else
                {
                    double[] sub = SubArray<double>(data, i - period, period);
                    if (sub.Max() != sub.Min())
                        toReturn[i]= (data[i]- sub.Min())/(sub.Max() - sub.Min()) * 100;
                }
            }
            return toReturn;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
