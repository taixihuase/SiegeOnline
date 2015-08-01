//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Position.cs
//
// 文件功能描述：
//
// 记录对象坐标方位信息
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
        public float X { get; set; }                // X坐标

        public float Y { get; set; }                // Y坐标

        public float Z { get; set; }                // Z坐标

        public float Direction { get; set; }        // 朝向

        public Position(float x = 0, float y = 0, float z = 0, float dir = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Direction = dir;
        }

        public void SetPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetPosition(float x, float y, float z, float dir)
        {
            SetPosition(x, y, z);
            Direction = dir;
        }

        public void SetBirthplace()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Direction = 0;
        }
    }
}
