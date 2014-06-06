using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FinancialInstumentsAI.FinancialParser
{
    internal static class PrnFinancialParser
    {
        private const double TimeOffsetInMinutes = 5.0;
        private const string DateFormat = "yyyyMMddHHmmss";

        public static KeyValuePair<DateTime, double>[] ParseFile(string source)
        {
            string extension = Path.GetExtension(source);
            if (extension == null || !File.Exists(source) || (extension.ToLower() != ".prn"))
                return null;

            var data = new Stack<KeyValuePair<DateTime, double>>();

            using (var reader = new StreamReader(File.OpenRead(source)))
            {
                reader.ReadLine();
                DateTime? lastDate = null;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;

                    string[] values = line.Split(',');
                    if (values.Count() != 10) break;

                    DateTime date;
                    if (!DateTime.TryParseExact(values[2] + values[3], DateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out date))
                        return null;

                    /*//filling empty spaces between data
                    if (lastDate != null)
                    {
                        TimeSpan offset = date - (DateTime)lastDate;
                        double minutes = offset.TotalMinutes;
                        while (minutes > TimeOffsetInMinutes)
                        {
                            KeyValuePair<DateTime, double> last = data.Peek();
                            data.Push(new KeyValuePair<DateTime, double>(last.Key.AddMinutes(5.0), last.Value));
                            minutes -= TimeOffsetInMinutes;
                        }
                    }
                    lastDate = date;
                    */

                    double value;
                    if (!double.TryParse(values[7], NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                        return null;

                    data.Push(new KeyValuePair<DateTime, double>(date, value));
                }
            }

            return data.ToArray();
        }
    }
}