using System;

namespace SiegeOnlineServer.Protocol.Common
{
    [Serializable]
    public static class DataConstraint
    {
        public static int EquipmentMaxLevel => 10;

        public static int EquipmentMaxDurability => 1000;

        public static float CharacterDefaultAttackSpeed => 100;

        public static float CharacterDefaultAttackDistance => 1;

        public static float CharacterDefaultMovementSpeed => 100;

        public static float CharacterDefaultSkillCooldownSpeed => 100;

        public static float CharacterDefaultExperienceGainSpeed => 100;
    }
}
