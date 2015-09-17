//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Position.cs
//
// 文件功能描述：
//
// 记录对象坐标方位及地图编号信息
//
// 创建标识：taixihuase 20150725
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System;

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：Position
    /// 作者：taixihuase
    /// 作用：位置属性类
    /// 编写日期：2015/7/25
    /// </summary>
    [Serializable]
    public class Position
    {
        public MapCode Map { get; set; } // 地图编号

        public int Zone { get; set; } // 区位编号

        public float X { get; set; } // X坐标

        public float Y { get; set; } // Y坐标

        public float Z { get; set; } // Z坐标

        public float Direction { get; set; } // 朝向

        /// <summary>
        /// 类型：方法
        /// 名称：Position
        /// 作者：taixihuase
        /// 作用：构造一个位置信息类实例
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="dir"></param>
        /// <param name="map"></param>
        public Position(float x = 0, float y = 0, float z = 0, float dir = 0, MapCode map = MapCode.World)
        {
            X = x;
            Y = y;
            Z = z;
            Direction = dir;
            Map = map;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetMap
        /// 作者：taixihuase
        /// 作用：设置地图编号
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="map"></param>
        public void SetMap(MapCode map)
        {
            Map = map;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetPosition
        /// 作者：taixihuase
        /// 作用：设置坐标
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetPosition
        /// 作者：taixihuase
        /// 作用：设置坐标及正面朝向
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="dir"></param>
        public void SetPosition(float x, float y, float z, float dir)
        {
            SetPosition(x, y, z);
            Direction = dir;
        }
    }
}
