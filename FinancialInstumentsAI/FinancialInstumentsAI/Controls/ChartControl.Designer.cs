namespace FinancialInstumentsAI.Controls
{
    partial class ChartControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPredictedSeries = new System.Windows.Forms.CheckBox();
            this.cbFixedSeries = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(124, 0);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(540, 324);
            this.chart.TabIndex = 2;
            this.chart.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbPredictedSeries);
            this.groupBox1.Controls.Add(this.cbFixedSeries);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 324);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chart settings";
            // 
            // cbPredictedSeries
            // 
            this.cbPredictedSeries.AutoSize = true;
            this.cbPredictedSeries.Checked = true;
            this.cbPredictedSeries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPredictedSeries.Location = new System.Drawing.Point(6, 42);
            this.cbPredictedSeries.Name = "cbPredictedSeries";
            this.cbPredictedSeries.Size = new System.Drawing.Size(101, 17);
            this.cbPredictedSeries.TabIndex = 1;
            this.cbPredictedSeries.Text = "Predicted series";
            this.cbPredictedSeries.UseVisualStyleBackColor = true;
            this.cbPredictedSeries.CheckedChanged += new System.EventHandler(this.cbPredictedSeries_CheckedChanged);
            // 
            // cbFixedSeries
            // 
            this.cbFixedSeries.AutoSize = true;
            this.cbFixedSeries.Checked = true;
            this.cbFixedSeries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFixedSeries.Location = new System.Drawing.Point(6, 19);
            this.cbFixedSeries.Name = "cbFixedSeries";
            this.cbFixedSeries.Size = new System.Drawing.Size(81, 17);
            this.cbFixedSeries.TabIndex = 0;
            this.cbFixedSeries.Text = "Fixed series";
            this.cbFixedSeries.UseVisualStyleBackColor = true;
            this.cbFixedSeries.CheckedChanged += new System.EventHandler(this.cbFixedSeries_CheckedChanged);
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(664, 324);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPredictedSeries;
        private System.Windows.Forms.CheckBox cbFixedSeries;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}
