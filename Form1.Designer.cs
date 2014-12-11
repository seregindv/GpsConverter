using System;

namespace GpsConverter
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.convertButton = new System.Windows.Forms.Button();
            this.montiorClipboardCheckBox = new System.Windows.Forms.CheckBox();
            this.fromBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.resultPanel = new System.Windows.Forms.TableLayoutPanel();
            this.info = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.fromBox.Location = new System.Drawing.Point(3, 3);
            this.fromBox.MaxLength = 2147483647;
            this.fromBox.Multiline = true;
            this.fromBox.Name = "fromBox";
            this.fromBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fromBox.Size = new System.Drawing.Size(680, 225);
            this.fromBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Prefix";
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.prefixTextBox.Location = new System.Drawing.Point(338, 236);
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(63, 20);
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
            // Form1
            // 
            this.AcceptButton = this.convertButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 527);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox fromBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.CheckBox montiorClipboardCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.TableLayoutPanel resultPanel;
        private System.Windows.Forms.Button info;
    }
}

