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
            set { textBox.Text = value; }
            get { return textBox.Text; }
        }

        public string Title
        {
            set { titleLabel.Text = value; }
            get { return titleLabel.Text; }
        }

        public Color TextColor
        {
            set { textBox.ForeColor = value; }
            get { return textBox.ForeColor; }
        }
    }
}
