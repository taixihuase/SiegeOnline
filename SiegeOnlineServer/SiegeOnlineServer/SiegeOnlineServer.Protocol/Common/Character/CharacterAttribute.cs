//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CharacterAttribute.cs
//
// 文件功能描述：
//
// 玩家角色属性扩展
//
// 创建标识：taixihuase 20150722
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
    /// 名称：CharacterAttribute
    /// 作者：taixihuase
    /// 作用：角色属性类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class CharacterAttribute
    {
        #region 潜能点

        public int AttrStrength { get; set; }

        public int AttrDexterity { get; set; }

        public int AttrIntellect { get; set; }

        public int AttrVitality { get; set; }

        #endregion

        #region 攻击属性

        public int[] AttackMagic { get; set; }

        public int[] AttackPhysical { get; set; }

        public float AttackPercentMagic { get; set; }

        public float AttackPercentPhysical { get; set; }

        public float CritMagic { get; set; }

        public float CritPhysical { get; set; }

        public float DamageMagic { get; set; }

        public float DamagePhysical { get; set; }

        public float DamageCritMagic { get; set; }

        public float DamageCritPhysical { get; set; }

        public float HitMagic { get; set; }

        public float HitPhysical { get; set; }

        public int EnhanceFire { get; set; }

        public int EnhanceFrost { get; set; }

        public int EnhanceLightning { get; set; }

        public int EnhancePoison { get; set; }

        public int EnhanceRock { get; set; }

        public int EnhanceShadow { get; set; }

        public int EnhanceStorm { get; set; }

        #endregion

        #region 防御属性

        public int[] DefenseMagic { get; set; }

        public int[] DefensePhysical { get; set; }

        public float DefensePercentMagic { get; set; }

        public float DefensePercentPhysical { get; set; }

        public float ReduceMagic { get; set; }

        public float ReducePhysical { get; set; }

        public float ReduceCritMagic { get; set; }

        public float ReduceCritPhysical { get; set; }

        public float DodgeMagic { get; set; }

        public float DodgePhysical { get; set; }

        public float BlockMagic { get; set; }

        public float BlockPhysical { get; set; }

        public int ResistanceFire { get; set; }

        public int ResistanceFrost { get; set; }

        public int ResistanceLightning { get; set; }

        public int ResistancePoison { get; set; }

        public int ResistanceRock { get; set; }

        public int ResistanceShadow { get; set; }

        public int ResistanceStorm { get; set; }

        #endregion

        #region 速率属性

        public float SpeedAttack { get; set; }

        public float SpeedCooldown { get; set; }

        public float SpeedExperience { get; set; }

        public float SpeedMovement { get; set; }

        #endregion

        #region 红蓝属性

        public int[] HitPoint { get; set; }

        public float LifeIncreasePercent { get; set; }

        public int LifeRecovery { get; set; }

        public float LifeSteal { get; set; }

        public int[] Mana { get; set; }

        public float ManaIncreasePercent { get; set; }

        public int ManaRecovery { get; set; }

        public float ManaSteal { get; set; }

        #endregion

        #region 特殊属性

        public float Feedback { get; set; }

        public float Immunity { get; set; }

        public float Rebound { get; set; }

        #endregion

        #region 默认非零属性

        public float AttackSpeed { get; set; }

        public float AttackDistance { get; set; }

        public float MovementSpeed { get; set; }

        public float SkillCooldownSpeed { get; set; }

        public float ExperienceGainSpeed { get; set; }

        #endregion

        /// <summary>
        /// 类型：方法
        /// 名称：CharacterrAttribute
        /// 作者：taixihuase
        /// 作用：构造一个默认的角色属性实例
        /// 编写日期：2015/7/23
        /// </summary>
        public CharacterAttribute()
        {
            AttackMagic = new int[3];
            AttackPhysical = new int[3];
            DefenseMagic = new int[2];
            DefensePhysical = new int[2];
            HitPoint = new int[2];
            Mana = new int[2];

            AttackSpeed = DataConstraint.CharacterDefaultAttackSpeed;
            AttackDistance = DataConstraint.CharacterDefaultAttackDistance;
            MovementSpeed = DataConstraint.CharacterDefaultMovementSpeed;
            SkillCooldownSpeed = DataConstraint.CharacterDefaultSkillCooldownSpeed;
            ExperienceGainSpeed = DataConstraint.CharacterDefaultExperienceGainSpeed;
        }
    }
}
