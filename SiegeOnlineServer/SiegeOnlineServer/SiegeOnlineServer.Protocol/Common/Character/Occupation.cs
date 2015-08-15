//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Occupation.cs
//
// 文件功能描述：
//
// 职业属性
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

namespace SiegeOnlineServer.Protocol.Common.Character
{
    /// <summary>
    /// 类型：类
    /// 名称：Occupation
    /// 作者：taixihuase
    /// 作用：角色职业类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Occupation
    {
        public string Name { get; set; } // 职业名

        public int BaseHitPoint { get; set; } // 基础生命值

        public int BaseMana { get; set; } // 基础法力值

        public int BaseAttack { get; set; } // 基础攻击力

        public int BaseDefense { get; set; } // 基础防御力

        public Occupation(string name = "")
        {
            Name = name;
        }
    }
}
