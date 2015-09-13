//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ArmorCollection.cs
//
// 文件功能描述：
//
// 防具集合
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
    /// 名称：ArmorCollection
    /// 作者：taixihuase
    /// 作用：防具集合类
    /// 编写日期：2015/8/27
    /// </summary>
    [Serializable]
    public class ArmorCollection : ICollection, IItem
    {
        private readonly Dictionary<int, Armor> _armors;

        // 索引器
        public Armor this[int index]
        {
            get
            {
                if (index >= 0 && _armors.ContainsKey(index))
                {
                    return _armors[index];
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
        /// 名称：ArmorCollection
        /// 作者：taixihuase
        /// 作用：构造一个 ArmorCollection 对象并初始化数据
        /// 编写日期：2015/8/27
        /// </summary>
        public ArmorCollection()
        {
            _armors = new Dictionary<int, Armor> {{-1, new Armor()}};
        }

        #region ICollection 接口实现

        public bool Add(int index, object obj)
        {
            if (!_armors.ContainsKey(index) && index >= 0)
            {
                _armors.Add(index, (Armor) obj);
                return true;
            }
            return false;
        }

        public bool Remove(int index)
        {
            if (_armors.ContainsKey(index) && index >= 0)
            {
                _armors.Remove(index);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _armors.Clear();
            _armors.Add(-1, new Armor());
        }

        #endregion

        #region IItem 接口实现

        public bool Apply(Character.Character character)
        {
            bool result = false;
            foreach (KeyValuePair<int, Armor> pair in _armors)
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
            foreach (KeyValuePair<int, Armor> pair in _armors)
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
