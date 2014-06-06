using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FinancialInstumentsAI.FinancialParser
{
    internal static class MstFinancialParser
    {
        private const string DateFormat = "yyyyMMdd";

        public static KeyValuePair<DateTime, double[]>[] ParseFile(string source)
        {
            string extension = Path.GetExtension(source);
            if (extension == null || !File.Exists(source)) return null;
            int dateIndex = 0;
            int valueIndex = 0;
            if (extension.ToLower() == ".mst")
            {
                dateIndex = 1;
                valueIndex = 2;
            }
            else
            {
                dateIndex = 2;
                valueIndex = 4;
            }
            var data = new Stack<KeyValuePair<DateTime, double[]>>();            
            using (var reader = new StreamReader(File.OpenRead(source)))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;

                    string[] values = line.Split(',');
                    //if (values.Count() != 7) break;

                    DateTime date;
                    if (!DateTime.TryParseExact(values[dateIndex], DateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out date)) return null;

                    double[] value = new double[4];
                    if (!double.TryParse(values[valueIndex], NumberStyles.Number, CultureInfo.InvariantCulture, out value[0]))
                        return null;
                    if (!double.TryParse(values[valueIndex+1], NumberStyles.Number, CultureInfo.InvariantCulture, out value[1]))
                        return null;
                    if (!double.TryParse(values[valueIndex+2], NumberStyles.Number, CultureInfo.InvariantCulture, out value[2]))
                        return null;
                    if (!double.TryParse(values[valueIndex+3], NumberStyles.Number, CultureInfo.InvariantCulture, out value[3]))
                        return null;
                    data.Push(new KeyValuePair<DateTime, double[]>(date, value));
                }
            }

            return data.ToArray();
        }
    }
}