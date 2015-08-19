//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Attribute.cs
//
// 文件功能描述：
//
// 角色基本属性
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
//-----------------------------------------------------------------------------------------------------------

using System;

namespace SiegeOnlineServer.Protocol.Common.Character
{
    /// <summary>
    /// 类型：类
    /// 名称：AttributeBase
    /// 作者：taixihuase
    /// 作用：基本属性类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class AttributeBase
    {
        public int Level { get; set; }                          // 等级

        public Position Position { get; set; }                  // 位置信息

        /// <summary>
        /// 类型：方法
        /// 名称：AttributeBase
        /// 作者：taixihuase
        /// 作用：通过一个基本属性对象构造一个新的实例对象，若参数为空，则设置为默认值
        /// 编写日期：2015/7/23
        /// </summary>
        /// <param name="attribute"></param>
        public AttributeBase(AttributeBase attribute = null)
        {
            if (attribute != null)
            {
                Level = attribute.Level;
                Position = attribute.Position;
            }
            else
            {
                Level = 1;
                Position = new Position();
                Position.SetBirthplace();
            }
        }

        /// <summary>
        ///  类型：方法
        /// 名称：SetBirthAttribute
        /// 作者：taixihuase
        /// 作用：通过职业类别，设置角色初始属性
        /// 编写日期：2015/7/23
        /// </summary>
        /// <param name="occupation"></param>
        public void SetBirthAttribute(Occupation occupation)
        {
            Level = 1;
        }
    }
}
