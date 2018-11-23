using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GpsConverter.Controls
{
    public partial class TitledTextbox : UserControl
    {
        public TitledTextbox()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        public Color TextColor
        {
            get { return textBox.ForeColor; }
            set { textBox.ForeColor = value; }
        }

        public Color BackColor
        {
            get { return textBox.BackColor;}
            set { textBox.BackColor = value; }
        }
    }
}
