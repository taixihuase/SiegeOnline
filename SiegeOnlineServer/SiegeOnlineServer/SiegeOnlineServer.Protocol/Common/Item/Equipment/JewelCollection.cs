using System;
using System.Collections.Generic;

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    [Serializable]
    public class JewelCollection : IItem
    {
        private readonly Dictionary<int, Jewel> _jewels;

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

        public JewelCollection()
        {
            _jewels = new Dictionary<int, Jewel> {{-1, new Jewel()}};
        }

        public bool Add(int index, Jewel armor)
        {
            if (!_jewels.ContainsKey(index) && index >= 0)
            {
                _jewels.Add(index, armor);
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
    }
}
