//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：WeaponCollection.cs
//
// 文件功能描述：
//
// 武器集合
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
    /// 名称：WeaponCollection
    /// 作者：taixihuase
    /// 作用：武器集合类
    /// 编写日期：2015/8/27
    /// </summary>
    [Serializable]
    public class WeaponCollection : ICollection, IItem
    {
        private readonly Dictionary<int, Weapon> _weapons;

        // 索引器
        public Weapon this[int index]
        {
            get
            {
                if (index >= 0 && _weapons.ContainsKey(index))
                {
                    return _weapons[index];
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
        /// 名称：WeaponCollection
        /// 作者：taixihuase
        /// 作用：构造一个 WeaponCollection 对象并初始化数据
        /// 编写日期：2015/8/27
        /// </summary>
        public WeaponCollection()
        {
            _weapons = new Dictionary<int, Weapon> {{-1, new Weapon()}};
        }

        #region ICollection 接口实现

        public bool Add(int index, object obj)
        {
            if (!_weapons.ContainsKey(index) && index >= 0)
            {
                _weapons.Add(index, (Weapon) obj);
                return true;
            }
            return false;
        }

        public bool Remove(int index)
        {
            if (_weapons.ContainsKey(index) && index >= 0)
            {
                _weapons.Remove(index);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _weapons.Clear();
            _weapons.Add(-1, new Weapon());
        }

        #endregion

        #region IItem 接口实现

        public bool Apply(Character.Character character)
        {
            bool result = false;
            foreach (KeyValuePair<int, Weapon> pair in _weapons)
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
            foreach (KeyValuePair<int, Weapon> pair in _weapons)
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
