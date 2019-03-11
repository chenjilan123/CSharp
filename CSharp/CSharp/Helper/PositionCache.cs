using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Helper
{
    public class PositionCache
    {
        private const string Precision = "0.0000";
        private ConcurrentDictionary<string, string> _cache = null;
        public PositionCache()
        {
            _cache = new ConcurrentDictionary<string, string>();
        }
        public bool TryGet(double lon, double lat, out string pos)
        {
            var key = this.GetKey(lon, lat);
            if (_cache.ContainsKey(key) && _cache.TryGetValue(key, out pos))
            {
                return true;
            }
            pos = string.Empty;
            return false;
        }

        public bool TryAdd(double lon, double lat, string pos)
        {
            var key = this.GetKey(lon, lat);
            return (!_cache.ContainsKey(key)) && _cache.TryAdd(key, pos);
        }

        private string GetKey(double lon, double lat)
        {
            return string.Format("{0}_{1}", lon.ToString(Precision), lat.ToString(Precision));
        }
    }
}
