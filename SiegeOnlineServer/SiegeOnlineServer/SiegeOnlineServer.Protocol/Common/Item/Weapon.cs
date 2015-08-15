//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Weapon.cs
//
// 文件功能描述：
//
// 武器属性
//
// 创建标识：taixihuase 20150723
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
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer.Protocol.Common.Item
{
    /// <summary>
    /// 类型：类
    /// 名称：Weapon
    /// 作者：taixihuase
    /// 作用：武器类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Weapon
    {
        public int FixedId { get; protected set; }              // 数据库固定编号

        public int AllocatedId { get; protected set; }          // 动态分配编号

        public string Name { get; protected set; }

        public int MaxLevel { get; protected set; }

        public int LevelLimit { get; protected set; }

        public int CurrentLevel { get; set; }

        #region 武器类型

        [Serializable]
        public enum WeaponType : byte
        {

        }

        public byte Type { get; protected set; }

        #endregion

        [Serializable]
        public enum AttackLimit : byte
        {
            Null,
            Attack_Physical_Min,
            Attack_Physical_Max,
            Attack_Magic_Min,
            Attack_Magic_Max
        }

        public Dictionary<AttackLimit, int> AttackPoints; // 攻击力上下限

        public Dictionary<int, Dictionary<AttributeCode, int>> ForgingAttributes; // 锻造附加属性

        public Weapon(int guid, int uniqueId, string name, int limit, int max, int cur, WeaponType type)
        {
            FixedId = guid;
            AllocatedId = uniqueId;
            Name = name;
            LevelLimit = limit;
            MaxLevel = max;
            CurrentLevel = cur;
            Type = (byte) type;
            AttackPoints = new Dictionary<AttackLimit, int>();
            ForgingAttributes = new Dictionary<int, Dictionary<AttributeCode, int>>();
        }

        public Weapon()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            LevelLimit = 0;
            MaxLevel = 0;
            CurrentLevel = 0;
            Type = (byte) AttackLimit.Null;
            AttackPoints = new Dictionary<AttackLimit, int> {{AttackLimit.Null, 0}};
            ForgingAttributes = new Dictionary<int, Dictionary<AttributeCode, int>>
            {
                {-1, new Dictionary<AttributeCode, int> {{AttributeCode.Null, 0}}}
            };
        }
    }
}
