
using System;

namespace SiegeOnlineServer.Protocol.Common
{
    [Serializable]
    public class MapZone
    {
        public MapCode CurrMap { get; set; }

        public int CurrZone { get; set; }

        public MapCode NextMap { get; set; }

        public int NextZone { get; set; }

        public int UniqueId { get; set; }
    }
}
