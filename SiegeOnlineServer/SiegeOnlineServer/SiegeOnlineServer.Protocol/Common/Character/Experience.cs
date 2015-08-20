//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Experience.cs
//
// 文件功能描述：
//
// 角色经验
//
// 创建标识：taixihuase 20150820
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
    /// 名称：Experience
    /// 作者：taixihuase
    /// 作用：玩家经验值数据类
    /// 编写日期：2015/8/20
    /// </summary>
    [Serializable]
    public class Experience
    {
        public int Level { get; set; } // 等级

        public int CurrenLevelExperience { get; set; } // 当前等级已获经验

        public int NextLevelDemand { get; set; } // 升级所需经验

        public int GainedExperience { get; set; } // 角色总获得经验 

        private int[] EachLevelDemand { get; set; } // 每级所需经验

        /// <summary>
        /// 类型：方法
        /// 名称：SetExperience
        /// 作者：taixihuase
        /// 作用：设置经验数据
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="cur"></param>
        /// <param name="next"></param>
        /// <param name="gained"></param>
        public void SetExperience(int lv, int cur, int next, int gained)
        {
            Level = lv;
            CurrenLevelExperience = cur;
            NextLevelDemand = next;
            GainedExperience = gained;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GainExperience
        /// 作者：taixihuase
        /// 作用：使经验值上涨
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="gain"></param>
        public void GainExperience(int gain)
        {
            GainedExperience += gain;
            CurrenLevelExperience += gain;
            if (CurrenLevelExperience >= NextLevelDemand)
            {
                if (Level < DataConstraint.CharacterMaxLevel)
                {
                    CurrenLevelExperience -= NextLevelDemand;
                    NextLevelDemand = EachLevelDemand[Level++];
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Experience
        /// 作者：taixihuase
        /// 作用：构造空的经验类实例
        /// 编写日期：2015/8/20
        /// </summary>
        public Experience()
        {
            EachLevelDemand = new int[DataConstraint.CharacterMaxLevel];
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetEachLevelDemand
        /// 作者：taixihuase
        /// 作用：设置各级经验需求数值
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="exp"></param>
        public void SetEachLevelDemand(int[] exp)
        {
            for (int i = 0; i < DataConstraint.CharacterMaxLevel; i++)
            {
                EachLevelDemand[i] = exp[i];
            }
        }
    }
}
