using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsConverter.Controls;
using GpsConverter.Converter;
using System.Text.RegularExpressions;
using GpsConverter.PointParsers;
using GpsConverter.Web.RequestProcessors;
using GpsConverter.Web.Servers;
using GpsConverter.Wikimapia;

namespace GpsConverter
{
    public partial class Form1 : ClipboardMonitorForm, IPoiProvider
    {
        private class ResultBox
        {
            public TitledTextbox TextBox { get; set; }
            public ToolStripButton SaveButton { get; set; }
        }

        readonly List<ResultBox> _resultBoxes = new List<ResultBox>();
        readonly ClipboardCoordinatesParser _htmlParser;
        private LocalWebServer _webServer;

        public Form1()
        {
            _htmlParser = new ClipboardCoordinatesParser();
            InitializeComponent();
            _resultBoxes.Add(CreateResultBox());
            resultPanel.Controls.Add(_resultBoxes[0].TextBox, 0, 0);
        }

        private void SetupResult(ConvertResult[] result)
        {
            for (int i = _resultBoxes.Count; i < result.Length; i++)
            {
                var box = CreateResultBox();
                _resultBoxes.Add(box);

            }
            for (int i = 0; i < result.Length; i++)
            {
                _resultBoxes[i].TextBox.Text = result[i].Text;
                _resultBoxes[i].TextBox.Title = result[i].Title;
                _resultBoxes[i].TextBox.TextColor = result[i].IsError ? Color.Red : Color.Black;
                if (result[i].IsOutdated)
                    _resultBoxes[i].TextBox.BackColor = SystemColors.Control;
                _resultBoxes[i].SaveButton.Text = "Save " + result[i].Title.ToLower();
                _resultBoxes[i].SaveButton.Visible = true;
            }
            if (result.Length != resultPanel.ColumnCount)
            {
                var colCount = resultPanel.ColumnCount;
                resultPanel.ColumnCount = result.Length;
                var colWidth = 100 / (float)result.Length;
                for (var i = 0; i < resultPanel.ColumnCount; i++)
                    if (resultPanel.ColumnStyles.Count > i)
                        SetupColumnStyle(resultPanel.ColumnStyles[i], colWidth);
                    else
                        resultPanel.ColumnStyles.Add(SetupColumnStyle(new ColumnStyle(), colWidth));

                if (colCount > result.Length)
                    for (var i = result.Length; i < colCount; i++)
                    {
                        resultPanel.Controls.Remove(_resultBoxes[i].TextBox);
                        toolStrip1.Items.Remove(_resultBoxes[i].SaveButton);
                    }
                else if (colCount < result.Length)
                    for (var i = colCount; i < result.Length; i++)
                    {
                        resultPanel.Controls.Add(_resultBoxes[i].TextBox, i, 0);
                        toolStrip1.Items.Add(_resultBoxes[i].SaveButton);
                    }
            }
        }

        private ColumnStyle SetupColumnStyle(ColumnStyle columnStyle, float width)
        {
            columnStyle.SizeType = SizeType.Percent;
            columnStyle.Width = width;
            return columnStyle;
        }

        private ResultBox CreateResultBox()
        {
            var textBox = new TitledTextbox();
            var button = new ToolStripButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Visible = false
            };
            button.Click += (s, e) => Save(textBox.Text, textBox.Title.ToLower());
            toolStrip1.Items.Add(button);

            return new ResultBox
            {
                TextBox = textBox,
                SaveButton = button
            };
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fromText = fromBox.Text;
                var converter = GetConverter(fromText);
                converter.Name = nameBox.Text;
                var converted = converter.Convert(fromText);
                SetupResult(converted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private IEarthConverter GetConverter(string text)
        {
            IEarthConverter converter;
            if (text.StartsWith("https://yandex.ru/maps/"))
                converter = new YaLinkToGpxConverter();
            else if (text.StartsWith("https://2gis.ru/"))
                converter = new DoubleGisToGpxConverter();
            else if (text.Contains("data-jmapping"))
                converter = new PoiConverter(new KarlovyVaryBusMapConverter());
            else if (text.Contains("GLatLng"))
                converter = new PoiConverter(new CzWiFiMapConverter());
            else if (text.IndexOf("<gpx", StringComparison.OrdinalIgnoreCase) != -1)
                converter = new PoiConverter(new GpxConverter());
            else if (text.IndexOf("<kml", StringComparison.OrdinalIgnoreCase) != -1)
                converter = new PoiConverter(new KmlConverter());
            else
                converter = new LinewizePoiConverter();
            return converter;
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
                var firstPoint = _htmlParser.Points.FirstOrDefault();
                if (firstPoint != null)
                    Text = firstPoint.Description;
                if (saveOnCopyCheckBox.Checked && !SaveText())
                    Text = "* " + Text;

            }
            base.OnClipboardChanged();
        }

        private void info_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                PointParser.GetFormatSamples()
                    .Aggregate(
                        new StringBuilder(),
                        (builder, sample) => builder.AppendLine(sample),
                        builder => builder.ToString()),
                "Supported formats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public IList<NamedEarthPoint> GetPoints()
        {
            var fromText = fromBox.Text;
            return GetConverter(fromText).GetPoints(fromText);
        }

        private void mapLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_webServer == null)
            {
                _webServer = new LocalWebServer(
                    new GetRequestProcessor(this),
                    new MapRequestProcessor(),
                    new CopyPointsRequestProcessor(),
                    new CopyPathRequestProcessor());
                _webServer.Start();
            }
            Process.Start(new Uri(_webServer.BaseUri, "map/").ToString());

        }

        private void folderLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["MapPath"]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_webServer != null)
                _webServer.Stop();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveText();
        }

        private bool Save(string text, string extension)
        {
            if (String.IsNullOrEmpty(nameBox.Text))
            {
                nameBox.Error = true;
                return false;
            }
            var encoding = Encoding.UTF8;
            var fileLength = encoding.GetByteCount(text);
            var filePath = Path.Combine(ConfigurationManager.AppSettings["MapPath"],
                nameBox.Text + "." + extension);
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists && fileInfo.Length > fileLength)
            {
                var messageBoxResult = MessageBox.Show(this, "Hey, you really wanna save smaller file?", "Wow", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (messageBoxResult == DialogResult.No)
                    return false;
            }

            File.WriteAllText(filePath, text, Encoding.UTF8);
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveText();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool SaveText() =>
            Save(fromBox.Text, "txt");
    }
}
