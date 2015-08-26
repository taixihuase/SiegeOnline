using System;
using System.Collections.Generic;

namespace SiegeOnlineServer.Protocol.Common.Skill
{
    [Serializable]
    public class SkillCollection
    {
        private readonly Dictionary<int, Skill> _skills;

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

        public SkillCollection()
        {
            _skills = new Dictionary<int, Skill> {{-1, new Skill()}};
        }

        public bool Add(int index, Skill weapon)
        {
            if (!_skills.ContainsKey(index) && index >= 0)
            {
                _skills.Add(index, weapon);
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
    }
}
