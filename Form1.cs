using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsConverter.Converter;
using System.Text.RegularExpressions;
using GpsConverter.Wikimapia;

namespace GpsConverter
{
    public partial class Form1 : ClipboardMonitorForm
    {
        List<TextBox> _resultBoxes = new List<TextBox>();

        readonly ClipboardCoordinatesParser _htmlParser;

        public Form1()
        {
            _htmlParser = new ClipboardCoordinatesParser();
            InitializeComponent();
            _resultBoxes.Add(CreateResultTextBox());
            resultPanel.Controls.Add(_resultBoxes[0], 0, 0);
            fromBox.KeyDown += TextBox_KeyDown;
        }

        private void SetupResult(string[] result)
        {
            for (int i = _resultBoxes.Count; i < result.Length; i++)
                _resultBoxes.Add(CreateResultTextBox());
            for (int i = 0; i < result.Length; i++)
                _resultBoxes[i].Text = result[i];
            if (result.Length != resultPanel.ColumnCount)
            {
                var colCount = resultPanel.ColumnCount;
                resultPanel.ColumnCount = result.Length;
                var colWidth = 100 / (float)result.Length;
                for (int i = 0; i < resultPanel.ColumnCount; i++)
                {
                    if (resultPanel.ColumnStyles.Count > i)
                        SetupColumnStyle(resultPanel.ColumnStyles[i], colWidth);
                    else
                        resultPanel.ColumnStyles.Add(SetupColumnStyle(new ColumnStyle(), colWidth));
                }
                if (colCount > result.Length)
                {
                    for (int i = result.Length; i < colCount; i++)
                        resultPanel.Controls.Remove(_resultBoxes[i]);
                }
                else if (colCount < result.Length)
                    for (int i = colCount; i < result.Length; i++)
                    {
                        resultPanel.Controls.Add(_resultBoxes[i], i, 0);
                    }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        private ColumnStyle SetupColumnStyle(ColumnStyle columnStyle, float width)
        {
            columnStyle.SizeType = SizeType.Percent;
            columnStyle.Width = width;
            return columnStyle;
        }

        private TextBox CreateResultTextBox()
        {
            var result = new TextBox
            {
                Multiline = true,
                WordWrap = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Both
            };
            result.KeyDown += TextBox_KeyDown;
            return result;
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            try
            {
                IEarthConverter converter = null;
                var fromText = fromBox.Text;
                if (fromText.StartsWith("http") && fromText.Contains("maps.yandex.ru"))
                    converter = new YaLinkToGpxConverter();
                else if (fromText.Contains("data-jmapping"))
                    converter = new PoiConverter(new KarlovyVaryBusMapConverter());
                else if (fromText.Contains("GLatLng"))
                    converter=new PoiConverter(new CzWiFiMapConverter());
                else
                    converter = new LinewizePoiConverter();
                var converted = converter.Convert(fromText);
                SetupResult(converted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        protected override void OnClipboardChanged()
        {
            if (montiorClipboardCheckBox.Checked && _htmlParser.Parse())
            {
                var points = _htmlParser.Points.Aggregate(new StringBuilder(),
                    (builder, point) => builder.AppendLine(String.Concat(point.Coordinates, " ", prefixTextBox.Text.Length > 0 ? (prefixTextBox.Text + " ") : String.Empty, point.Description)),
                    builder => builder.ToString());
                fromBox.AppendText(points);
                fromBox.Select(fromBox.Text.Length, 0);
                fromBox.ScrollToCaret();
            }
            base.OnClipboardChanged();
        }
    }
}
