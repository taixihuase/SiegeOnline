//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：SkillCollection.cs
//
// 文件功能描述：
//
// 技能集合
//
// 创建标识：taixihuase 20150827
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

namespace SiegeOnlineServer.Protocol.Common.Skill
{
    /// <summary>
    /// 类型：类
    /// 名称：SkillCollection
    /// 作者：taixihuase
    /// 作用：技能集合类
    /// 编写日期：2015/8/27
    /// </summary>
    [Serializable]
    public class SkillCollection : ICollection
    {
        private readonly Dictionary<int, Skill> _skills;

        // 索引器
        public Skill this[int index]
        {
            get
            {
                if (index >= 0 && _skills.ContainsKey(index))
                {
                    return _skills[index];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    Add(index, value);
                }
                else
                {
                    Remove(index);
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SkillCollection
        /// 作者：taixihuase
        /// 作用：构造一个 SkillCollection 对象并初始化数据
        /// 编写日期：2015/8/27
        /// </summary>
        public SkillCollection()
        {
            _skills = new Dictionary<int, Skill> {{-1, new Skill()}};
        }

        #region ICollection 接口实现

        public bool Add(int index, object obj)
        {
            if (!_skills.ContainsKey(index) && index >= 0)
            {
                _skills.Add(index, (Skill) obj);
                return true;
            }
            return false;
        }

        public bool Remove(int index)
        {
            if (_skills.ContainsKey(index) && index >= 0)
            {
                _skills.Remove(index);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _skills.Clear();
            _skills.Add(-1, new Skill());
        }

        #endregion
    }
}
