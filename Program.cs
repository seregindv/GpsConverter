using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GpsConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var dp = new GpsConverter.DataProviders.AndroidYandexMapsDP();
            //var pt = new GpsConverter.Data.AndroidYandexMapsPoint
            //{
            //    Name = "Питер",
            //    Latitude = 59.95,
            //    Longitude = 30.3
            //};
            ////dp.InsertPoint(pt);
            //dp.GetPoints();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void DumpBytes(byte[] bytes, string path)
        {
            using (var fileStream = System.IO.File.Create(path))
                fileStream.Write(bytes, 0, bytes.Length);
        }
    }
}
