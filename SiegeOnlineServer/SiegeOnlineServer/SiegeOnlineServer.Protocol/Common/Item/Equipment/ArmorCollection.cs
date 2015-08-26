using System;
using System.Collections.Generic;

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    [Serializable]
    public class ArmorCollection : IItem
    {
        private readonly Dictionary<int, Armor> _armors;

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

        public ArmorCollection()
        {
            _armors = new Dictionary<int, Armor> {{-1, new Armor()}};
        }

        public bool Add(int index, Armor armor)
        {
            if (!_armors.ContainsKey(index) && index >= 0)
            {
                _armors.Add(index, armor);
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
    }
}
