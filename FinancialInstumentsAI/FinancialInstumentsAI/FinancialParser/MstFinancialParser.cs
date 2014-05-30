using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FinancialInstumentsAI.FinancialParser
{
    internal static class MstFinancialParser
    {
        private const string DateFormat = "yyyyMMdd";

        public static KeyValuePair<DateTime, double>[] ParseFile(string source)
        {
            string extension = Path.GetExtension(source);
            if (extension == null || !File.Exists(source) || (extension.ToLower() != ".mst")) return null;

            var data = new Stack<KeyValuePair<DateTime, double>>();

            using (var reader = new StreamReader(File.OpenRead(source)))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;

                    string[] values = line.Split(',');
                    if (values.Count() != 7) break;

                    DateTime date;
                    if (!DateTime.TryParseExact(values[1], DateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out date)) return null;

                    double value;
                    if (!double.TryParse(values[5], NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                        return null;

                    data.Push(new KeyValuePair<DateTime, double>(date, value));
                }
            }

            return data.ToArray();
        }
    }
}