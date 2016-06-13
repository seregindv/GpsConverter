using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GpsConverter.Converter
{
    interface IEarthConverter
    {
        IList<NamedEarthPoint> GetPoints(string points);
        ConvertResult[] Convert(string something);
        string Name { set; get; }
    }
}
