using System;
using System.Collections.Generic;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
using static System.String;

namespace SiegeOnlineServer.Collection
{
    public class MapCollection
    {
        // 服务端
        public readonly ServerApplication Server;

        public Dictionary<MapCode, Dictionary<int, List<int>>> MapInfo;

        public MapCollection(ServerApplication server)
        {
            Server = server;

            MapInfo = new Dictionary<MapCode, Dictionary<int, List<int>>>(new EnumComparer<MapCode>());
            for (int i = 0; i < Enum.GetValues(typeof (MapCode)).Length; i++)
            {
                MapInfo.Add((MapCode) i, new Dictionary<int, List<int>>());
                string zone = EnumDescription.GetEnumDescription<MapCode>((MapCode) i);
                if (zone != Empty)
                {
                    for (int j = 0; j < Convert.ToInt32(zone); j++)
                    {
                        MapInfo[(MapCode) i].Add(j, new List<int>());
                    }
                }
            }
        }

        public bool Add(MapCode map, int zone, int id)
        {
            if (MapInfo[map].ContainsKey(zone))
            {
                if (MapInfo[map][zone].Contains(id))
                {
                    return false;
                }
                MapInfo[map][zone].Add(id);
                return true;
            }
            return false;
        }

        public bool Remove(MapCode map, int zone, int id)
        {
            if (MapInfo[map].ContainsKey(zone))
            {
                if (MapInfo[map][zone].Contains(id))
                {
                    MapInfo[map][zone].Remove(id);
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool Update(MapZone mapZone)
        {
            if (Add(mapZone.NextMap, mapZone.NextZone, mapZone.UniqueId))
            {
                if (Remove(mapZone.CurrMap, mapZone.CurrZone, mapZone.UniqueId))
                {
                    return true;
                }
                Remove(mapZone.NextMap, mapZone.NextZone, mapZone.UniqueId);
                return false;
            }
            return false;
        }

    }
}
