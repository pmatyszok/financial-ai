namespace FinancialInstumentsAI.Dialogs
{
    partial class AISettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.windowSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.layersNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.momentumNumeric = new System.Windows.Forms.NumericUpDown();
            this.rateNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.alphaNumeric = new System.Windows.Forms.NumericUpDown();
            this.activFuncComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.constValueTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.initFuncComboBox = new System.Windows.Forms.ComboBox();
            this.layerCountCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layersNumeric)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.momentumNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateNumeric)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alphaNumeric)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.windowSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.layersNumeric);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network";
            // 
            // windowSize
            // 
            this.windowSize.Location = new System.Drawing.Point(105, 41);
            this.windowSize.Name = "windowSize";
            this.windowSize.Size = new System.Drawing.Size(97, 20);
            this.windowSize.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Window size";
            // 
            // layersNumeric
            // 
            this.layersNumeric.Location = new System.Drawing.Point(105, 14);
            this.layersNumeric.Name = "layersNumeric";
            this.layersNumeric.Size = new System.Drawing.Size(97, 20);
            this.layersNumeric.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layers count:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.momentumNumeric);
            this.groupBox2.Controls.Add(this.rateNumeric);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Learner";
            // 
            // momentumNumeric
            // 
            this.momentumNumeric.DecimalPlaces = 2;
            this.momentumNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.momentumNumeric.Location = new System.Drawing.Point(74, 35);
            this.momentumNumeric.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.momentumNumeric.Name = "momentumNumeric";
            this.momentumNumeric.Size = new System.Drawing.Size(128, 20);
            this.momentumNumeric.TabIndex = 5;
            // 
            // rateNumeric
            // 
            this.rateNumeric.DecimalPlaces = 2;
            this.rateNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rateNumeric.Location = new System.Drawing.Point(74, 14);
            this.rateNumeric.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.rateNumeric.Name = "rateNumeric";
            this.rateNumeric.Size = new System.Drawing.Size(128, 20);
            this.rateNumeric.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Momentum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rate:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.alphaNumeric);
            this.groupBox3.Controls.Add(this.activFuncComboBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 163);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 68);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Activation function";
            // 
            // alphaNumeric
            // 
            this.alphaNumeric.DecimalPlaces = 2;
            this.alphaNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.alphaNumeric.Location = new System.Drawing.Point(74, 42);
            this.alphaNumeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.alphaNumeric.Name = "alphaNumeric";
            this.alphaNumeric.Size = new System.Drawing.Size(128, 20);
            this.alphaNumeric.TabIndex = 6;
            // 
            // activFuncComboBox
            // 
            this.activFuncComboBox.FormattingEnabled = true;
            this.activFuncComboBox.Location = new System.Drawing.Point(74, 19);
            this.activFuncComboBox.Name = "activFuncComboBox";
            this.activFuncComboBox.Size = new System.Drawing.Size(128, 21);
            this.activFuncComboBox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Alpha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Type:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 22);
            this.panel1.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(7, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(82, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(157, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.constValueTextBox);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.initFuncComboBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 78);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initialization function";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Const";
            // 
            // constValueTextBox
            // 
            this.constValueTextBox.Location = new System.Drawing.Point(74, 47);
            this.constValueTextBox.Name = "constValueTextBox";
            this.constValueTextBox.Size = new System.Drawing.Size(128, 20);
            this.constValueTextBox.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Type:";
            // 
            // initFuncComboBox
            // 
            this.initFuncComboBox.FormattingEnabled = true;
            this.initFuncComboBox.Location = new System.Drawing.Point(74, 19);
            this.initFuncComboBox.Name = "initFuncComboBox";
            this.initFuncComboBox.Size = new System.Drawing.Size(128, 21);
            this.initFuncComboBox.TabIndex = 0;
            this.initFuncComboBox.SelectedIndexChanged += new System.EventHandler(this.initFuncComboBox_SelectedIndexChanged);
            // 
            // layerCountCheckBox
            // 
            this.layerCountCheckBox.AutoSize = true;
            this.layerCountCheckBox.Location = new System.Drawing.Point(7, 352);
            this.layerCountCheckBox.Name = "layerCountCheckBox";
            this.layerCountCheckBox.Size = new System.Drawing.Size(220, 17);
            this.layerCountCheckBox.TabIndex = 5;
            this.layerCountCheckBox.Text = "Choose hidden layer neuron count for me";
            this.layerCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Iterations";
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(86, 320);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(128, 20);
            this.iterationsTextBox.TabIndex = 7;
            // 
            // AISettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(232, 397);
            this.Controls.Add(this.iterationsTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.layerCountCheckBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AISettings";
            this.Text = "AISettings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layersNumeric)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.momentumNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateNumeric)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alphaNumeric)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown layersNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox activFuncComboBox;
        private System.Windows.Forms.NumericUpDown momentumNumeric;
        private System.Windows.Forms.NumericUpDown rateNumeric;
        private System.Windows.Forms.NumericUpDown alphaNumeric;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox initFuncComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox constValueTextBox;
        private System.Windows.Forms.NumericUpDown windowSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox layerCountCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox iterationsTextBox;
    }
}