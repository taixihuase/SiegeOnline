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
        /// 作用：尝试从数据库获取获取玩家游戏角色信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public PlayerCollection.CharacterReturn GetCharacterInfoFromDatabase(ref Character character)
        {
            PlayerCollection.CharacterReturn characterReturn = new PlayerCollection.CharacterReturn();

            if (character.Nickname == "abcd" || character.Nickname == "efgh")
            {
                character.Attribute.Level = 10;
                character.Occupation.Name = "战士";
                character.Attribute.Position.SetPosition(10, 20, 30);

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

        public void GetCharacterPositionFromDatabase(ref Character character)
        {
            if (character.Status == (byte) UserBase.StatusTypes.Gaming)
            {
                character.Attribute.Position.SetPosition(10, 20, 30);
            }
        }

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
                    character.Occupation.Name = "战士";
                    character.Occupation.BaseHitPoint = 100;
                    character.Occupation.BaseMana = 50;
                    character.Occupation.BaseAttack = 1;
                    character.Occupation.BaseDefense = 0;
                    character.Attribute.SetBirthAttribute(character.Occupation);
                    character.Attribute.Position.SetBirthplace();
                    break;
            }

            SaveCharacterToDatabase(character);
        }

        public void SaveCharacterToDatabase(Character character)
        {
            
        }
    }
}
