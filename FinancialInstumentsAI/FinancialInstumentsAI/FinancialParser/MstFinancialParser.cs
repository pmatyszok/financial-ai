using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FinancialInstumentsAI.FinancialParser
{
    internal static class MstFinancialParser
    {
        private const string DateFormat = "yyyyMMdd";

        public static KeyValuePair<DateTime, double>[] ParseFile(string source)
        {
            string extension = Path.GetExtension(source);
            if (extension == null || !File.Exists(source)) return null;
            int dateIndex;
            int valueIndex;
            if (extension.ToLower() == ".mst")
            {
                dateIndex = 1;
                valueIndex = 5;
            }
            else
            {
                dateIndex = 2;
                valueIndex = 7;
            }
            var data = new Stack<KeyValuePair<DateTime, double>>();            
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

                    double value;
                    if (!double.TryParse(values[valueIndex], NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                        return null;                   
                    data.Push(new KeyValuePair<DateTime, double>(date, value));
                }
            }

            return data.ToArray();
        }
    }
}