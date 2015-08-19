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

        [Serializable]
        public enum JewelType : byte
        {
            Null,
            Bracelet,           // 手镯
            Earring,            // 耳环
            Necklace,           // 项链
            Ring                // 戒指
        }

        public byte Type { get; protected set; }

        #endregion

        #region 饰品属性类型

        [Serializable]
        public enum JewelAttributeType : byte
        {
            Null,
        }

        public JewelAttributeType JewelAttribute { get; protected set; }

        public KeyValuePair<JewelAttributeType, float> SpecialAttribute;

        #endregion

        public Jewel(Equipment equipment, JewelType type, JewelAttributeType jewelAttributeType)
            : base(equipment)
        {
            Type = (byte) type;
            JewelAttribute = jewelAttributeType;
            SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(jewelAttributeType, 0);
        }

        public Jewel()
        {
            Type = (byte) JewelType.Null;
            JewelAttribute = JewelAttributeType.Null;
            SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(JewelAttributeType.Null, 0);
        }

        public void UpdateSpecialAttribute(float value)
        {
            if (value > 0)
            {
                SpecialAttribute = new KeyValuePair<JewelAttributeType, float>(JewelAttribute, value);
            }
        }

        public void UpdateSpecialAttributeType(JewelAttributeType jewelAttribute)
        {
            JewelAttribute = jewelAttribute;
        }
    }
}
