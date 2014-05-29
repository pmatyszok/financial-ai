using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class selectData : Form
    {
        private string filaName;
        private List<double> data = new List<double>();
        public selectData()
        {
            InitializeComponent();
        }

        public selectData(string file)
        {
            InitializeComponent();
            filaName = file;            
        }

        private void readFromFile(string from, string to)
        {
            using (var reader = new StreamReader(File.OpenRead(filaName+".mst")))
            {
                string line;
                bool add= false;
                reader.ReadLine();               
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var l = line.Split(',');
                    if (from.Equals(l[1]))
                    {
                        add = true;
                    }
                    if (to.Equals(l[1]))
                    {                        
                        return;
                    }
                    if(add)
                        data.Add(double.Parse(l[5], System.Globalization.CultureInfo.InvariantCulture));
                }
                if (!add)
                {
                    MessageBox.Show("nienatrafiono na początkową date");
                }
            }
        }

        public double[] toDoubleTable()
        {
            return data.ToArray();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.Date < dateTimePicker2.Value.Date)
            {
                string month = dateTimePicker1.Value.Month.ToString();
                string month2 = dateTimePicker2.Value.Month.ToString();
                if(dateTimePicker1.Value.Month < 10)
                    month = "0"+dateTimePicker1.Value.Month.ToString();
                if (dateTimePicker2.Value.Month < 10)
                    month2 = "0" + dateTimePicker2.Value.Month.ToString();

                string day = dateTimePicker1.Value.Day.ToString();
                string day2 = dateTimePicker2.Value.Day.ToString();
                if (dateTimePicker1.Value.Day < 10)
                    day = "0" + dateTimePicker1.Value.Day.ToString();
                if (dateTimePicker2.Value.Day < 10)
                    day2 = "0" + dateTimePicker2.Value.Day.ToString();

                var fromdata = dateTimePicker1.Value.Year.ToString() + month + day;
                var todata = dateTimePicker2.Value.Year.ToString() + month2 + day2;
                readFromFile(fromdata, todata);

                DialogResult = DialogResult.OK;
            }
        }
    }
}
