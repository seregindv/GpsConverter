using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        readonly List<TitledTextbox> _resultBoxes = new List<TitledTextbox>();
        readonly ClipboardCoordinatesParser _htmlParser;
        private LocalWebServer _webServer;

        public Form1()
        {
            _htmlParser = new ClipboardCoordinatesParser();
            InitializeComponent();
            _resultBoxes.Add(CreateResultTextBox());
            resultPanel.Controls.Add(_resultBoxes[0], 0, 0);
        }

        private void SetupResult(ConvertResult[] result)
        {
            for (int i = _resultBoxes.Count; i < result.Length; i++)
                _resultBoxes.Add(CreateResultTextBox());
            for (int i = 0; i < result.Length; i++)
            {
                _resultBoxes[i].Text = result[i].Text;
                _resultBoxes[i].Title = result[i].Title;
                _resultBoxes[i].TextColor = result[i].IsError ? Color.Red : Color.Black;
            }
            if (result.Length != resultPanel.ColumnCount)
            {
                var colCount = resultPanel.ColumnCount;
                resultPanel.ColumnCount = result.Length;
                var colWidth = 100 / (float)result.Length;
                for (int i = 0; i < resultPanel.ColumnCount; i++)
                    if (resultPanel.ColumnStyles.Count > i)
                        SetupColumnStyle(resultPanel.ColumnStyles[i], colWidth);
                    else
                        resultPanel.ColumnStyles.Add(SetupColumnStyle(new ColumnStyle(), colWidth));

                if (colCount > result.Length)
                    for (int i = result.Length; i < colCount; i++)
                        resultPanel.Controls.Remove(_resultBoxes[i]);
                else if (colCount < result.Length)
                    for (int i = colCount; i < result.Length; i++)
                        resultPanel.Controls.Add(_resultBoxes[i], i, 0);
            }
        }

        private ColumnStyle SetupColumnStyle(ColumnStyle columnStyle, float width)
        {
            columnStyle.SizeType = SizeType.Percent;
            columnStyle.Width = width;
            return columnStyle;
        }

        private TitledTextbox CreateResultTextBox()
        {
            return new TitledTextbox();
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
                    new CopyRequestProcessor());
                _webServer.Start();
            }
            Process.Start(new Uri(_webServer.BaseUri, "map/").ToString());

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_webServer!=null)
                _webServer.Stop();
        }
    }
}
