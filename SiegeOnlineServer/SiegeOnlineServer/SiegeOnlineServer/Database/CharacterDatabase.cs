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
        /// 名称：GetCharacterInfoFromDatabase
        /// 作者：taixihuase
        /// 作用：尝试从数据库获取玩家游戏角色信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public PlayerCollection.CharacterReturn GetCharacterInfoFromDatabase(ref Character character)
        {
            PlayerCollection.CharacterReturn characterReturn = new PlayerCollection.CharacterReturn();

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

                Weapon w = new Weapon(1, 2, "刀", OccupationCode.Warrior | OccupationCode.Paladin, 1, 1,
                    DataConstraint.EquipmentMaxDurability, Weapon.WeaponType.Null, Weapon.WeaponAttributeType.Physical);
                character.Weapons.Add(1, w);
                w.UpdateAttackLimit(100, 200, null, null);
                w.AddFixedAttribute(AttributeCode.Attack_Physical, 100);
                w.AddFixedAttribute(AttributeCode.Attack_Percent_Both, 10);
                w.Upgrade();
                w.Upgrade();
                w.Upgrade(AttributeCode.Attack_Percent_Both, 90);

                Armor a = new Armor(10, 20, "头盔", OccupationCode.Warrior, 1, 1, DataConstraint.EquipmentMaxDurability,
                    Armor.ArmorType.Helmet);
                character.Armors.Add(1, a);
                a.UpdateDefensePoints(1000, 2000);
                a.AddFixedAttribute(AttributeCode.Life_Increase, 1000);
                a.Upgrade();
                a.Upgrade(AttributeCode.Life_Increase_Percent, 50);

                Jewel j = new Jewel(100, 200, "戒指", OccupationCode.Common, 1, 1, DataConstraint.EquipmentMaxDurability,
                    Jewel.JewelType.Ring, Jewel.JewelAttributeType.Null);
                character.Jewels.Add(1, j);
                j.AddFixedAttribute(AttributeCode.Resistance_All, 22222);


                #endregion

                characterReturn.ReturnCode = (byte) PlayerCollection.CharacterReturn.ReturnCodeTypes.Success;
                characterReturn.DebugMessage.Append("成功获取角色数据");
            }
            else
            {
                characterReturn.ReturnCode = (byte) PlayerCollection.CharacterReturn.ReturnCodeTypes.CharacterNotFound;
                characterReturn.DebugMessage.Append("当前账号尚未创建角色");
            }

            return characterReturn;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetCharacterPositionFromDatabase
        /// 作者：taixihuase
        /// 作用：获取玩家游戏上次离线时的位置信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        public void GetCharacterPositionFromDatabase(ref Character character)
        {
            if (character.Status == (byte) UserBase.StatusTypes.Gaming)
            {
                character.Position.SetPosition(10, 20, 30);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateCharacterDependOnDatabase
        /// 作者：taixihuase
        /// 作用：根据数据库资料为玩家创建一个新的角色
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        /// <param name="info"></param>
        public void CreateCharacterDependOnDatabase(out Character character, CreateInfo info)
        {
            UserBase temp = new UserBase(info.Guid, info.Account, info.UniqueId, info.Nickname, info.Status)
            {
                LoginTime = info.LoginTime
            };
            character = new Character(temp);

            switch (info.Occupation)
            {
                case (byte) CreateInfo.OccupationTypes.Warrior:
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

            SaveCharacterToDatabase(character);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SaveCharacterToDatabase
        /// 作者：taixihuase
        /// 作用：向数据库提交一个玩家的角色数据
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        public void SaveCharacterToDatabase(Character character)
        {
            
        }
    }
}
