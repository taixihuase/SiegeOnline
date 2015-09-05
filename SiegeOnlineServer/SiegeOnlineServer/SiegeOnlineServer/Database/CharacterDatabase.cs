//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CharacterDatabase.cs
//
// 文件功能描述：
//
// 角色数据库操作，与数据库进行角色信息的交互
//
// 创建标识：taixihuase 20150724
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using SiegeOnlineServer.Collection;
using SiegeOnlineServer.Protocol.Common;
using SiegeOnlineServer.Protocol.Common.Character;
using SiegeOnlineServer.Protocol.Common.Item.Equipment;
using SiegeOnlineServer.Protocol.Common.User;

namespace SiegeOnlineServer.Database
{
    /// <summary>
    /// 类型：类
    /// 名称：CharacterDatabase
    /// 作者：taixihuase
    /// 作用：用于服务端与数据库的传输
    /// 编写日期：2015/7/24
    /// </summary>
    public class CharacterDatabase
    {
        /// <summary>
        /// 类型：方法
        /// 名称：GetCharacter
        /// 作者：taixihuase
        /// 作用：尝试从数据库获取玩家游戏角色信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public CharacterCollection.CharacterReturn GetCharacter(Character character)
        {
            CharacterCollection.CharacterReturn characterReturn = new CharacterCollection.CharacterReturn();

            if (character.Nickname == "abcd" || character.Nickname == "efgh")
            {
                #region 测试用例

                character.Position.SetPosition(10, 20, 30);
                int[] exp = new int[DataConstraint.CharacterMaxLevel];
                exp[0] = 0;
                for (int i = 1; i < exp.Length; i++)
                {
                    exp[i] = exp[i - 1] + 10;
                }
                character.Experience.SetEachLevelDemand(exp);
                character.Experience.SetExperience(0, 0, 0, 0);
                character.Experience.GainExperience(0);

                character.Occupation.UpdateOccupation(OccupationCode.Warrior, "战士");
                character.Occupation.BaseHitPoint = 50;
                character.Occupation.BaseLifeRecovery = 5;
                character.Occupation.BaseMana = 10;
                character.Occupation.BaseManaRecovery = 1;
                character.Occupation.Apply(character.Attribute);

                Weapon w = new Weapon(1, 2, "刀", OccupationCode.Warrior | OccupationCode.Paladin, 1, true, 1,
                    DataConstraint.EquipmentMaxDurability, 0, 200, 200, Weapon.WeaponType.Null,
                    Weapon.WeaponAttackType.Physical,
                    Weapon.WeaponElementType.Null);

                character.Weapons.Add(1, w);
                w.UpdateAttackLimit(100, 200, null, null);
                w.UpdateFixedAttribute(AttributeCode.Attack_Physical, 100);
                w.UpdateFixedAttribute(AttributeCode.Attack_Percent_Both, 10);
                w.Upgrade();
                w.Upgrade();
                w.Upgrade(AttributeCode.Attack_Percent_Both, 90);
                w.UpdateElementAttribute(Weapon.WeaponElementType.Lightning);
                w.UpgradeElementAttribute(0);
                w.UpgradeElementEnhanceAttribute(300);
                w.UpgradeElementExtraAttribute(10, 2);

                Armor a = new Armor(10, 20, "头盔", OccupationCode.Warrior, 1, true, 1,
                    DataConstraint.EquipmentMaxDurability, Armor.ArmorType.Helmet);

                character.Armors.Add(1, a);
                a.UpdateDefensePoints(1000, 2000);
                a.UpdateFixedAttribute(AttributeCode.Life_Increase, 1000);
                a.Upgrade();
                a.Upgrade(AttributeCode.Life_Increase_Percent, 50);

                Jewel j = new Jewel(100, 200, "戒指", OccupationCode.Common, 1, false, 1,
                    DataConstraint.EquipmentMaxDurability, Jewel.JewelType.Ring, Jewel.JewelAttributeType.Null);

                character.Jewels.Add(1, j);
                j.UpdateFixedAttribute(AttributeCode.Resistance_All, 22222);
                j.UpdateRandomAttribute(AttributeCode.Attr_Strength, 1234);

                #endregion

                characterReturn.ReturnCode = CharacterCollection.CharacterReturn.ReturnCodeType.Success;
                characterReturn.DebugMessage.Append("成功获取角色数据");
            }
            else
            {
                characterReturn.ReturnCode = CharacterCollection.CharacterReturn.ReturnCodeType.CharacterNotFound;
                characterReturn.DebugMessage.Append("当前账号尚未创建角色");
            }

            return characterReturn;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetCharacterPosition
        /// 作者：taixihuase
        /// 作用：获取玩家游戏上次离线时的位置信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        public void GetCharacterPosition(Character character)
        {
            if (character.Status == UserInfo.StatusType.Gaming)
            {
                character.Position.SetPosition(10, 20, 30);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateNewCharacter
        /// 作者：taixihuase
        /// 作用：根据数据库资料为玩家创建一个新的角色
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        public void CreateNewCharacter(Character character)
        {
            switch (character.Occupation.Type)
            {
                case OccupationCode.Warrior:
                {
                    character.Position.SetPosition(10, 20, 30);
                    int[] exp = new int[DataConstraint.CharacterMaxLevel];
                    exp[0] = 0;
                    for (int i = 1; i < exp.Length; i++)
                    {
                        exp[i] = exp[i - 1] + 10;
                    }
                    character.Experience.SetEachLevelDemand(exp);
                    character.Experience.SetExperience(0, 0, 0, 0);
                    character.Experience.GainExperience(0);

                    character.Occupation.UpdateOccupation(OccupationCode.Warrior, "战士");
                    character.Occupation.BaseHitPoint = 50;
                    character.Occupation.BaseLifeRecovery = 5;
                    character.Occupation.BaseMana = 10;
                    character.Occupation.BaseManaRecovery = 1;
                    character.Occupation.Apply(character.Attribute);

                    break;
                }
            }

            SaveCharacter(character);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SaveCharacter
        /// 作者：taixihuase
        /// 作用：向数据库提交一个玩家的角色数据
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        public void SaveCharacter(Character character)
        {

        }
    }
}
