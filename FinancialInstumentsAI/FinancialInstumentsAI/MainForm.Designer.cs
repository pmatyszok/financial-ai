﻿namespace FinancialInstumentsAI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obliczToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.run100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runButton = new System.Windows.Forms.ToolStripMenuItem();
            this.predic = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toPred = new System.Windows.Forms.ToolStripTextBox();
            this.oneValue = new System.Windows.Forms.ToolStripMenuItem();
            this.tcCharts = new System.Windows.Forms.TabControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.te = new System.Windows.Forms.ToolStripStatusLabel();
            this.predErrorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.predLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.valueCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSourceList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.splitter = new System.Windows.Forms.Splitter();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(672, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSourceToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.dataToolStripMenuItem.Text = "File";
            // 
            // setSourceToolStripMenuItem
            // 
            this.setSourceToolStripMenuItem.Name = "setSourceToolStripMenuItem";
            this.setSourceToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.setSourceToolStripMenuItem.Text = "Set source...";
            this.setSourceToolStripMenuItem.Click += new System.EventHandler(this.setSourceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.obliczToolStripMenuItem,
            this.predic});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "AI Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // obliczToolStripMenuItem
            // 
            this.obliczToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.run100ToolStripMenuItem,
            this.runButton});
            this.obliczToolStripMenuItem.Name = "obliczToolStripMenuItem";
            this.obliczToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.obliczToolStripMenuItem.Text = "Teach";
            // 
            // run100ToolStripMenuItem
            // 
            this.run100ToolStripMenuItem.Name = "run100ToolStripMenuItem";
            this.run100ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.run100ToolStripMenuItem.Text = "Run(100%)";
            this.run100ToolStripMenuItem.Click += new System.EventHandler(this.run100ToolStripMenuItem_Click);
            // 
            // runButton
            // 
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(152, 22);
            this.runButton.Text = "Run(70%)";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // predic
            // 
            this.predic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.toPred,
            this.oneValue});
            this.predic.Name = "predic";
            this.predic.Size = new System.Drawing.Size(152, 22);
            this.predic.Text = "Predict";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // toPred
            // 
            this.toPred.Name = "toPred";
            this.toPred.Size = new System.Drawing.Size(100, 23);
            // 
            // oneValue
            // 
            this.oneValue.Name = "oneValue";
            this.oneValue.Size = new System.Drawing.Size(160, 22);
            this.oneValue.Text = "One Value";
            // 
            // tcCharts
            // 
            this.tcCharts.ContextMenuStrip = this.contextMenuStrip;
            this.tcCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCharts.Location = new System.Drawing.Point(153, 24);
            this.tcCharts.Name = "tcCharts";
            this.tcCharts.SelectedIndex = 0;
            this.tcCharts.Size = new System.Drawing.Size(519, 293);
            this.tcCharts.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.toolStripStatusLabel1,
            this.te,
            this.predErrorLabel,
            this.predLabel,
            this.valueCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 317);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel1.Text = "info...";
            // 
            // te
            // 
            this.te.Name = "te";
            this.te.Size = new System.Drawing.Size(94, 17);
            this.te.Text = "Teach error: 0.00";
            // 
            // predErrorLabel
            // 
            this.predErrorLabel.Name = "predErrorLabel";
            this.predErrorLabel.Size = new System.Drawing.Size(157, 17);
            this.predErrorLabel.Text = "Predict value RMS error: 0.00";
            // 
            // predLabel
            // 
            this.predLabel.Name = "predLabel";
            this.predLabel.Size = new System.Drawing.Size(56, 17);
            this.predLabel.Text = "Predict: 0";
            // 
            // valueCount
            // 
            this.valueCount.Name = "valueCount";
            this.valueCount.Size = new System.Drawing.Size(76, 17);
            this.valueCount.Text = "Value count: ";
            // 
            // lbSourceList
            // 
            this.lbSourceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSourceList.FormattingEnabled = true;
            this.lbSourceList.Location = new System.Drawing.Point(3, 16);
            this.lbSourceList.Name = "lbSourceList";
            this.lbSourceList.Size = new System.Drawing.Size(144, 274);
            this.lbSourceList.TabIndex = 5;
            this.lbSourceList.DoubleClick += new System.EventHandler(this.lbSourceList_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbSourceList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 293);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data source";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(150, 24);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 293);
            this.splitter.TabIndex = 7;
            this.splitter.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 339);
            this.Controls.Add(this.tcCharts);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.TabControl tcCharts;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox lbSourceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predic;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toPred;
        private System.Windows.Forms.ToolStripMenuItem oneValue;
        private System.Windows.Forms.ToolStripStatusLabel te;
        private System.Windows.Forms.ToolStripStatusLabel predErrorLabel;
        private System.Windows.Forms.ToolStripStatusLabel predLabel;
        private System.Windows.Forms.ToolStripStatusLabel valueCount;
        private System.Windows.Forms.ToolStripMenuItem obliczToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem run100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runButton;
    }
}

