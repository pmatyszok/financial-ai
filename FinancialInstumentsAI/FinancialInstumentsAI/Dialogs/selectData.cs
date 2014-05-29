using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class SelectData : Form
    {
        private readonly string fileName;
        private readonly List<double> data = new List<double>();
        public SelectData()
        {
            InitializeComponent();
        }

        public SelectData(string file)
        {
            InitializeComponent();
            fileName = file;
        }

        private void ReadFromFile(string from, string to)
        {
            using (var reader = new StreamReader(File.OpenRead(fileName + ".mst")))
            {
                bool add = false;
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] l = line.Split(',');
                    if (from.Equals(l[1]))
                    {
                        add = true;
                    }
                    if (to.Equals(l[1]))
                    {
                        return;
                    }
                    if (add)
                        data.Add(double.Parse(l[5], CultureInfo.InvariantCulture));
                }
                if (!add)
                {
                    MessageBox.Show("Nie natrafiono na początkową date");
                }
            }
        }

        public double[] ToDoubleTable()
        {
            return data.ToArray();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date < dateTimePicker2.Value.Date)
            {
                string month = dateTimePicker1.Value.Month.ToString();
                string month2 = dateTimePicker2.Value.Month.ToString();
                if (dateTimePicker1.Value.Month < 10)
                    month = "0" + dateTimePicker1.Value.Month;
                if (dateTimePicker2.Value.Month < 10)
                    month2 = "0" + dateTimePicker2.Value.Month;

                string day = dateTimePicker1.Value.Day.ToString();
                string day2 = dateTimePicker2.Value.Day.ToString();
                if (dateTimePicker1.Value.Day < 10)
                    day = "0" + dateTimePicker1.Value.Day;
                if (dateTimePicker2.Value.Day < 10)
                    day2 = "0" + dateTimePicker2.Value.Day;

                string fromdata = dateTimePicker1.Value.Year + month + day;
                string todata = dateTimePicker2.Value.Year + month2 + day2;
                ReadFromFile(fromdata, todata);

                DialogResult = DialogResult.OK;
            }
        }
    }
}
