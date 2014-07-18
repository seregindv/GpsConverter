using GpsConverter.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace GpsConverter.DataProviders
{
    class AndroidYandexMapsDP
    {
        string Connection = @"Data Source=H:\yandexmaps\data\bookmarks\labels.db;Version=3;";

        public AndroidYandexMapsDP()
        {
        }

        public List<AndroidYandexMapsPoint> GetPoints()
        {
            var result = new List<AndroidYandexMapsPoint>();
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select * from mylabels";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new AndroidYandexMapsPoint
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                LowerName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Latitude = reader.IsDBNull(3) ? null : (double?)reader.GetDouble(3),
                                Longitude = reader.IsDBNull(4) ? null : (double?)reader.GetDouble(4),
                                GeoCode = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Date = reader.IsDBNull(6) ? null : (long?)reader.GetInt64(6),
                                Oid = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }
            return result;
        }

        public long GetMaxId()
        {
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select max(_id) from mylabels";
                    var result = cmd.ExecuteScalar();
                    return result is DBNull ? 0L : (long)result;
                }
            }
        }

        public void InsertPoint(AndroidYandexMapsPoint point)
        {
            if (point.Id == 0)
                point.Id = GetMaxId() + 1;
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"insert into mylabels(_id,label_name,label_name_tolower
,lat,lon,geocode,date,oid)VALUES(@id,@label_name,@label_name_tolower
,@lat,@lon,@geocode,@date,@oid)";
                    cmd.Parameters.AddWithValue("@id", point.Id);
                    cmd.Parameters.AddWithValue("@label_name", (object)point.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@label_name_tolower", (object)point.LowerName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@lat", (object)point.Latitude ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@lon", (object)point.Longitude ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@geocode", (object)point.GeoCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", (object)point.Date ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@oid", (object)point.Oid ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
