using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsConverter.Data
{
    public class AndroidYandexMapsPoint
    {
        public long Id { set; get; }
        public string Name { set; get; }
        string _lowerName;
        public string LowerName
        {
            set { _lowerName = value; }
            get
            {
                return _lowerName == null
                    ? Name.ToLower()
                    : _lowerName;

            }
        }
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public string GeoCode { set; get; }
        public long? Date { set; get; }
        public string Oid { set; get; }
    }
}
