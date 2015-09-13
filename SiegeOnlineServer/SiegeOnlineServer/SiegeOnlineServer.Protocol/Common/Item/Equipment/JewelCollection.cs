//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：JewelCollection.cs
//
// 文件功能描述：
//
// 饰品集合
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

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    /// <summary>
    /// 类型：类
    /// 名称：JewelCollection
    /// 作者：taixihuase
    /// 作用：饰品集合类
    /// 编写日期：2015/8/27
    /// </summary>
    [Serializable]
    public class JewelCollection : ICollection, IItem
    {
        private readonly Dictionary<int, Jewel> _jewels;

        // 索引器
        public Jewel this[int index]
        {
            get
            {
                if (index >= 0 && _jewels.ContainsKey(index))
                {
                    return _jewels[index];
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
        /// 名称：JewelCollection
        /// 作者：taixihuase
        /// 作用：构造一个 JewelCollection 对象并初始化数据
        /// 编写日期：2015/8/27
        /// </summary>
        public JewelCollection()
        {
            _jewels = new Dictionary<int, Jewel> {{-1, new Jewel()}};
        }

        #region ICollection 接口实现

        public bool Add(int index, object obj)
        {
            if (!_jewels.ContainsKey(index) && index >= 0)
            {
                _jewels.Add(index, (Jewel) obj);
                return true;
            }
            return false;
        }

        public bool Remove(int index)
        {
            if (_jewels.ContainsKey(index) && index >= 0)
            {
                _jewels.Remove(index);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _jewels.Clear();
            _jewels.Add(-1, new Jewel());
        }

        #endregion

        #region IItem 接口实现

        public bool Apply(Character.Character character)
        {
            bool result = false;
            foreach (KeyValuePair<int, Jewel> pair in _jewels)
            {
                if (pair.Key > 0)
                {
                    result = pair.Value.Apply(character);
                    if (result == false)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public bool Cancel(Character.Character character)
        {
            bool result = false;
            foreach (KeyValuePair<int, Jewel> pair in _jewels)
            {
                if (pair.Key > 0)
                {
                    result = pair.Value.Cancel(character);
                    if (result == false)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
