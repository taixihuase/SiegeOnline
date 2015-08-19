//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Jewel.cs
//
// 文件功能描述：
//
// 饰品属性
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

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    /// <summary>
    /// 类型：类
    /// 名称：Jewel
    /// 作者：taixihuase
    /// 作用：饰品类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Jewel : Equipment
    {
        #region 饰品类型

        /// <summary>
        /// 类型：枚举
        /// 名称：JewelType
        /// 作者：taixihuase
        /// 作用：饰品类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum JewelType : byte
        {
            Null,
            Bracelet, // 手镯
            Earring, // 耳环
            Necklace, // 项链
            Ring // 戒指
        }

        public byte Type { get; protected set; } // 饰品类型

        #endregion

        #region 饰品属性类型

        /// <summary>
        /// 类型：枚举
        /// 名称：JewelAttributeType
        /// 作者：taixihuase
        /// 作用：饰品特殊属性类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum JewelAttributeType : byte
        {
            Null,
        }

        public JewelAttributeType JewelAttribute { get; protected set; } // 饰品特殊属性类型

        public KeyValuePair<JewelAttributeType, float> SpecialAttribute; // 饰品特殊属性（单条）

        #endregion

        /// <summary>
        /// 类型：方法
        /// 名称：Jewel
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造饰品装备实例
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="fixedId"></param>
        /// <param name="allocatedId"></param>
        /// <param name="name"></param>
        /// <param name="occupation"></param>
        /// <param name="limit"></param>
        /// <param name="cur"></param>
        /// <param name="dur"></param>
        /// <param name="type"></param>
        /// <param name="jewelAttributeType"></param>
        public Jewel(int fixedId, int allocatedId, string name, OccupationCode occupation, int limit, int cur, int dur,
            JewelType type, JewelAttributeType jewelAttributeType)
            : base(fixedId, allocatedId, name, occupation, limit, cur, dur, EquipmentType.Jewel)
        {
            Type = (byte) type;
            JewelAttribute = jewelAttributeType;
            SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(jewelAttributeType, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Jewel
        /// 作者：taixihuase
        /// 作用：构造空的饰品装备实例
        /// 编写日期：2015/8/16
        /// </summary>
        public Jewel()
        {
            Type = (byte) JewelType.Null;
            JewelAttribute = JewelAttributeType.Null;
            SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(JewelAttributeType.Null, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateSpecialAttribute
        /// 作者：taixihuase
        /// 作用：变更饰品特殊属性数值
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="value"></param>
        public void UpdateSpecialAttribute(float value)
        {
            if (value > 0)
            {
                SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(JewelAttribute, value);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateSpecialAttributeType
        /// 作者：taixihuase
        /// 作用：变更饰品特殊属性类型
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="jewelAttribute"></param>
        public void UpdateSpecialAttributeType(JewelAttributeType jewelAttribute)
        {
            JewelAttribute = jewelAttribute;
        }
    }
}
