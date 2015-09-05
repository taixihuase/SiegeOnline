//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：UserDatabase.cs
//
// 文件功能描述：
//
// 用户数据库操作，与数据库进行用户信息的交互
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
using SiegeOnlineServer.Protocol.Common.User;
using static SiegeOnlineServer.Collection.UserCollection.UserReturn.ReturnCodeType;

namespace SiegeOnlineServer.Database
{
    /// <summary>
    /// 类型：类
    /// 名称：UserDatabase
    /// 作者：taixihuase
    /// 作用：用于服务端与数据库的传输
    /// 编写日期：2015/7/24
    /// </summary>
    public class UserDatabase
    {
        /// <summary>
        /// 类型：方法
        /// 名称：GetUserInfo
        /// 作者：taixihuase
        /// 作用：尝试从数据库获取获取用户信息
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserCollection.UserReturn GetUserInfo(UserInfo user, string password)
        {
            UserCollection.UserReturn userReturn = new UserCollection.UserReturn();

            if (user.Account == "test" || user.Account == "siege" || user.Account == "online")
            {
                if (password == "1234")
                {
                    if (user.Account == "siege")
                    {
                        user.UniqueId = 1;
                        user.Nickname = "abcd";
                    }
                    else if (user.Account == "online")
                    {
                        user.UniqueId = 2;
                        user.Nickname = "efgh";
                    }
                    else
                    {
                        user.UniqueId = 3;
                        user.Nickname = "test";
                    }

                    userReturn.ReturnCode = Default;
                    userReturn.DebugMessage.Append("用户信息正确!");
                }
                else
                {
                    userReturn.ReturnCode = WrongPassword;
                    userReturn.DebugMessage.Append("密码错误!");
                }
            }
            else
            {
                userReturn.ReturnCode = Unregister;
                userReturn.DebugMessage.Append("该账号未注册!");
            }

            return userReturn;
        }

        public UserCollection.UserReturn RegistNewUser(RegistInfo info)
        {
            UserCollection.UserReturn userReturn = new UserCollection.UserReturn();

            if (info.Account != "siege" && info.Account != "online" && info.Account != "test")
            {
                if (info.Nickname != "abcd" && info.Nickname != "efgh" && info.Nickname != "test")
                {
                    userReturn.ReturnCode = Success;
                    userReturn.DebugMessage.Append("注册账号成功");
                }
                else
                {
                    userReturn.ReturnCode = NicknameExist;
                    userReturn.DebugMessage.Append("昵称已被使用");
                }
            }
            else
            {
                userReturn.ReturnCode = AccountExist;
                userReturn.DebugMessage.Append("账号名已存在");
            }

            return userReturn;
        }

        public void SaveUser(UserInfo user)
        {
            
        }
    }
}
