//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Skill.cs
//
// 文件功能描述：
//
// 技能属性
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

namespace SiegeOnlineServer.Protocol.Common.Skill
{
    /// <summary>
    /// 类型：类
    /// 名称：Skill
    /// 作者：taixihuase
    /// 作用：角色技能类
    /// 编写日期：2015/7/22
    /// </summary>
    [Serializable]
    public class Skill
    {
        public string Name { get; protected set; }

        public int FixedId { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：Skill
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造技能对象实例
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="fixedId"></param>
        /// <param name="name"></param>
        public Skill(int fixedId, string name)
        {
            FixedId = fixedId;
            Name = name;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Skill
        /// 作者：taixihuase
        /// 作用：构造空的技能实例
        /// 编写日期：2015/8/27
        /// </summary>
        public Skill()
        {
            FixedId = 0;
            Name = "";
        }
    }
}
