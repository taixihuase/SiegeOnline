using System;
using System.Collections.Generic;

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    [Serializable]
    public class WeaponCollection : IItem
    {
        private readonly Dictionary<int, Weapon> _weapons;

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

        public WeaponCollection()
        {
            _weapons = new Dictionary<int, Weapon> {{-1, new Weapon()}};
        }

        public bool Add(int index, Weapon weapon)
        {
            if (!_weapons.ContainsKey(index) && index >= 0)
            {
                _weapons.Add(index, weapon);
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
    }
}
