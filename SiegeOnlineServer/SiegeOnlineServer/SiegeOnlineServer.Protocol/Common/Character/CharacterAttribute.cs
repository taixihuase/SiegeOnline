//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CharacterAttribute.cs
//
// 文件功能描述：
//
// 玩家角色属性扩展
//
// 创建标识：taixihuase 20150722
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

namespace SiegeOnlineServer.Protocol.Common.Character
{
    /// <summary>
    /// 类型：类
    /// 名称：CharacterAttribute
    /// 作者：taixihuase
    /// 作用：角色属性拓展类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class CharacterAttribute : AttributeBase
    {
        public DateTime WorldEnterTime { get; set; }

        public CharacterAttribute(AttributeBase attribute = null) : base(attribute)
        {
            WorldEnterTime = new DateTime();
        }
    }
}
