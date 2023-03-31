using System;
using GpsConverter.Converter;

namespace GpsConverter.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.folderLink = new System.Windows.Forms.LinkLabel();
            this.saveOnCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.mapLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.nameBox = new GpsConverter.Controls.TextBoxEx();
            this.info = new System.Windows.Forms.Button();
            this.convertButton = new System.Windows.Forms.Button();
            this.montiorClipboardCheckBox = new System.Windows.Forms.CheckBox();
            this.fromBox = new GpsConverter.Controls.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.resultPanel = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.folderLink);
            this.splitContainer1.Panel1.Controls.Add(this.saveOnCopyCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.mapLink);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.nameBox);
            this.splitContainer1.Panel1.Controls.Add(this.info);
            this.splitContainer1.Panel1.Controls.Add(this.convertButton);
            this.splitContainer1.Panel1.Controls.Add(this.montiorClipboardCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.fromBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.prefixTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultPanel);
            this.splitContainer1.Size = new System.Drawing.Size(686, 527);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // folderLink
            // 
            this.folderLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.folderLink.AutoSize = true;
            this.folderLink.Location = new System.Drawing.Point(248, 239);
            this.folderLink.Name = "folderLink";
            this.folderLink.Size = new System.Drawing.Size(36, 13);
            this.folderLink.TabIndex = 11;
            this.folderLink.TabStop = true;
            this.folderLink.Text = "Folder";
            this.folderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.folderLink_LinkClicked);
            // 
            // saveOnCopyCheckBox
            // 
            this.saveOnCopyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveOnCopyCheckBox.AutoSize = true;
            this.saveOnCopyCheckBox.Checked = true;
            this.saveOnCopyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveOnCopyCheckBox.Location = new System.Drawing.Point(125, 238);
            this.saveOnCopyCheckBox.Name = "saveOnCopyCheckBox";
            this.saveOnCopyCheckBox.Size = new System.Drawing.Size(92, 17);
            this.saveOnCopyCheckBox.TabIndex = 10;
            this.saveOnCopyCheckBox.Text = "Save on copy";
            this.saveOnCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(686, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(35, 22);
            this.saveButton.Text = "Save";
            this.saveButton.ToolTipText = "Save txt";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // mapLink
            // 
            this.mapLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mapLink.AutoSize = true;
            this.mapLink.Location = new System.Drawing.Point(218, 239);
            this.mapLink.Name = "mapLink";
            this.mapLink.Size = new System.Drawing.Size(28, 13);
            this.mapLink.TabIndex = 8;
            this.mapLink.TabStop = true;
            this.mapLink.Text = "Map";
            this.mapLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mapLink_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Error = false;
            this.nameBox.Location = new System.Drawing.Point(417, 236);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(140, 20);
            this.nameBox.TabIndex = 6;
            // 
            // info
            // 
            this.info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.info.Location = new System.Drawing.Point(563, 234);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(23, 23);
            this.info.TabIndex = 5;
            this.info.Text = "?";
            this.info.UseVisualStyleBackColor = true;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // convertButton
            // 
            this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.convertButton.Location = new System.Drawing.Point(592, 234);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(82, 23);
            this.convertButton.TabIndex = 1;
            this.convertButton.Text = "Convert it";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // montiorClipboardCheckBox
            // 
            this.montiorClipboardCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.montiorClipboardCheckBox.AutoSize = true;
            this.montiorClipboardCheckBox.Checked = true;
            this.montiorClipboardCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.montiorClipboardCheckBox.Location = new System.Drawing.Point(12, 238);
            this.montiorClipboardCheckBox.Name = "montiorClipboardCheckBox";
            this.montiorClipboardCheckBox.Size = new System.Drawing.Size(107, 17);
            this.montiorClipboardCheckBox.TabIndex = 2;
            this.montiorClipboardCheckBox.Text = "Monitor clipboard";
            this.montiorClipboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // fromBox
            // 
            this.fromBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fromBox.Error = false;
            this.fromBox.Location = new System.Drawing.Point(3, 28);
            this.fromBox.MaxLength = 2147483647;
            this.fromBox.Multiline = true;
            this.fromBox.Name = "fromBox";
            this.fromBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fromBox.Size = new System.Drawing.Size(680, 200);
            this.fromBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Prefix";
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prefixTextBox.Location = new System.Drawing.Point(338, 236);
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(32, 20);
            this.prefixTextBox.TabIndex = 3;
            // 
            // resultPanel
            // 
            this.resultPanel.ColumnCount = 1;
            this.resultPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPanel.Location = new System.Drawing.Point(0, 0);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.RowCount = 1;
            this.resultPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resultPanel.Size = new System.Drawing.Size(686, 263);
            this.resultPanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AcceptButton = this.convertButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 527);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "GPS Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.TextBoxEx fromBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.CheckBox montiorClipboardCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.TableLayoutPanel resultPanel;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Label label2;
        private Controls.TextBoxEx nameBox;
        private System.Windows.Forms.LinkLabel mapLink;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.CheckBox saveOnCopyCheckBox;
        private System.Windows.Forms.LinkLabel folderLink;
    }
}

