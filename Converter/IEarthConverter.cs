using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsConverter.Converter
{
    interface IEarthConverter
    {
        string[] Convert(string something);
        string Name { set; get; }
    }
}
