using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsConverter.Converter
{
    public interface IPoiProvider
    {
        IList<NamedEarthPoint> GetPoints();
    }
}
