//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：DatabaseCollection.cs
//
// 文件功能描述：
//
// 地图管理集合，用于记录及更新在线玩家所处的地图区位信息
//
// 创建标识：taixihuase 20150915
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
using static System.String;

namespace SiegeOnlineServer.Collection
{
    /// <summary>
    /// 类型：类
    /// 名称：MapCollection
    /// 作者：taixihuase
    /// 作用：保存和管理角色的地图区位信息
    /// 编写日期：2015/9/15
    /// </summary>
    public class MapCollection
    {
        // 服务端
        public readonly ServerApplication Server;

        // 记录每个区位中存在的角色编号
        public Dictionary<MapCode, Dictionary<int, List<int>>> MapInfo;

        // 记录每个区位中存在的客户端连接
        public Dictionary<MapCode, Dictionary<int, List<ServerPeer>>> PeerList; 

        /// <summary>
        /// 类型：方法
        /// 名称：MapCollection
        /// 作者：taixihuase
        /// 作用：通过预设的地图分区数据实例化一个地图集合对象
        /// 编写日期：2015/9/15
        /// </summary>
        /// <param name="server"></param>
        public MapCollection(ServerApplication server)
        {
            Server = server;

            MapInfo = new Dictionary<MapCode, Dictionary<int, List<int>>>(new EnumComparer<MapCode>());

            PeerList = new Dictionary<MapCode, Dictionary<int, List<ServerPeer>>>(new EnumComparer<MapCode>());

            // 地图编号从 1 开始
            for (int i = 1; i <= Enum.GetValues(typeof (MapCode)).Length; i++)
            {
                MapInfo.Add((MapCode) i, new Dictionary<int, List<int>>());
                PeerList.Add((MapCode) i, new Dictionary<int, List<ServerPeer>>());
                string zone = EnumDescription.GetEnumDescription<MapCode>((MapCode) i);
                if (zone != Empty)
                {
                    // 区位编号从 1 开始
                    for (int j = 1; j <= Convert.ToInt32(zone); j++)
                    {
                        MapInfo[(MapCode) i].Add(j, new List<int>());
                        PeerList[(MapCode) i].Add(j, new List<ServerPeer>());
                    }
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Add
        /// 作者：taixihuase
        /// 作用：向指定地图的某个分区中添加一个角色编号
        /// 编写日期：2015/9/15
        /// </summary>
        /// <param name="map"></param>
        /// <param name="zone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Add(MapCode map, int zone, int id)
        {
            if (MapInfo[map].ContainsKey(zone))
            {
                if (MapInfo[map][zone].Contains(id))
                {
                    return false;
                }
                MapInfo[map][zone].Add(id);
                PeerList[map][zone].Add(Server.Users.TryGetPeer(Server.Users.GetGuidFromUniqueId(id)));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Remove
        /// 作者：taixihuase
        /// 作用：移除指定地图分区中的一个角色编号
        /// </summary>
        /// <param name="map"></param>
        /// <param name="zone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(MapCode map, int zone, int id)
        {
            if (MapInfo[map].ContainsKey(zone))
            {
                if (MapInfo[map][zone].Contains(id))
                {
                    MapInfo[map][zone].Remove(id);
                    PeerList[map][zone].Remove(Server.Users.TryGetPeer(Server.Users.GetGuidFromUniqueId(id)));
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Update
        /// 作者：taixihuase
        /// 作用：读取角色地图区位信息并进行统计更新
        /// </summary>
        /// <param name="mapZone"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 类型：方法
        /// 名称：Search
        /// 作者：taixihuase
        /// 作用：查找某个角色是否存在并返回其当前的地图区位信息，若不存在于场景中则将返回的地图区位信息置零
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mapCode"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public bool Search(int id, out MapCode mapCode, out int zone)
        {
            foreach (KeyValuePair<MapCode, Dictionary<int, List<int>>> map in MapInfo)
            {
                foreach (KeyValuePair<int, List<int>> pair in map.Value.Where(pair => pair.Value.Contains(id)))
                {
                    mapCode = map.Key;
                    zone = pair.Key;
                    return true;
                }
            }
            mapCode = 0;
            zone = 0;
            return false;
        }
    }
}

