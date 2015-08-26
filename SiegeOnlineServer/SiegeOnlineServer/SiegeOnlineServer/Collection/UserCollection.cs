//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：UserCollection.cs
//
// 文件功能描述：
//
// 用户数据集合，存放连线中的用户信息及进行用户基本信息的操作
//
// 创建标识：taixihuase 20150712
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using SiegeOnlineServer.Protocol.Common.User;
using static SiegeOnlineServer.Collection.UserCollection.UserReturn.ReturnCodeType;
using static SiegeOnlineServer.Protocol.Common.User.UserInfo.StatusType;

namespace SiegeOnlineServer.Collection
{
    /// <summary>
    /// 类型：类
    /// 名称：UserCollection
    /// 作者：taixihuase
    /// 作用：保存当前连接的客户端，访问用户信息
    /// 编写日期：2015/7/12
    /// </summary>
    public class UserCollection
    {
        // 服务端
        public readonly ServerApplication Server;

        // 当前连接的客户端列表
        protected Dictionary<Guid, ServerPeer> ConnectedClients { get; set; }

        // 用于进行广播的已连线客户端列表
        public List<ServerPeer> ConnectedClientsToBroadcast { get; set; }

        // 从 Guid 取得编号
        protected Dictionary<Guid, int> GuidToUniqueId { get; set; }

        // 从账号名获得编号
        protected Dictionary<string, int> AccountToUniqueId { get; set; }

        // 从昵称取得编号
        protected Dictionary<string, int> NicknameToUniqueId { get; set; }

        // 从编号获得用户信息
        protected Dictionary<int, UserInfo> UniqueIdToUser { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：UserCollection
        /// 作者：taixihuase
        /// 作用：构造 UserCollection 对象
        /// 编写日期：2015/7/12
        /// </summary>
        public UserCollection(ServerApplication server)
        {
            Server = server;
            ConnectedClients = new Dictionary<Guid, ServerPeer>();
            ConnectedClientsToBroadcast = new List<ServerPeer>();
            GuidToUniqueId = new Dictionary<Guid, int>();
            AccountToUniqueId = new Dictionary<string, int>();
            UniqueIdToUser = new Dictionary<int, UserInfo>();
            NicknameToUniqueId = new Dictionary<string, int>();
        }

        /// <summary>
        /// 类型：类
        /// 名称：UserReturn
        /// 作者：taixihuase
        /// 作用：返回用户登录信息
        /// 编写日期：2015/7/12
        /// </summary>
        [Serializable]
        public class UserReturn
        {
            // 回传码
            public ReturnCodeType ReturnCode { get; set; }

            // 回传字串
            public StringBuilder DebugMessage { get; set; }

            /// <summary>
            /// 类型：枚举
            /// 名称：ReturnCodeTypes
            /// 作者：taixihuase
            /// 作用：登录操作回传码枚举值
            /// 编写日期：2015/7/12
            /// </summary>
            [Serializable]
            public enum ReturnCodeType : byte
            {
                Default, // 初始默认值
                Success, // 操作成功
                RepeatedLogin, // 重复登录
                WrongPassword, // 错误密码
                Unregister, // 账号未注册
                AccountExist,   // 账号已存在
                NicknameExist   // 昵称已存在
            }

            /// <summary>
            /// 类型：方法
            /// 名称：UserReturn
            /// 作者：taixihuase
            /// 作用：构造 UserReturn 对象
            /// 编写日期：2015/7/12
            /// </summary>
            public UserReturn()
            {
                ReturnCode = ReturnCodeType.Default;
                DebugMessage = new StringBuilder();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistUser
        /// 作者：taixihuase
        /// 作用：注册一个新的用户
        /// 编写日期：2015/8/26
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public UserReturn RegistUser(RegistInfo info)
        {
            return Server.Data.UserData.RegistNewUser(info);
        }
        
         /// <summary>
         /// 类型：方法
         /// 名称：AddConnectedPeer
         /// 作者：taixihuase
         /// 作用：添加一个新的客户端连接
         /// 编写日期：2015/7/12
         /// </summary>
         /// <param name="guid"></param>
         /// <param name="peer"></param>
        public void AddConnectedPeer(Guid guid, ServerPeer peer)
        {
            ConnectedClients.Add(guid, peer);
            ConnectedClientsToBroadcast.Add(peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetPeer
        /// 作者：taixihuase
        /// 作用：尝试通过索引键获取客户端连接
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServerPeer TryGetPeer(Guid guid)
        {
            if (ConnectedClients.ContainsKey(guid))
            {
                ServerPeer peer;
                ConnectedClients.TryGetValue(guid, out peer);
                return peer;
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemovePeer
        /// 作者：taixihuase
        /// 作用：删除一个客户端连接
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="guid"></param>
        public void RemovePeer(Guid guid)
        {
            if (ConnectedClients.ContainsKey(guid))
            {
                ConnectedClientsToBroadcast.Remove(ConnectedClients[guid]);
                ConnectedClients.Remove(guid);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UserOnline
        /// 作者：taixihuase
        /// 作用：通过接收并添加一个用户信息进行登录
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserReturn UserOnline(UserInfo user, string password)
        {
            // 从数据库获取用户信息
            UserReturn userReturn = Server.Data.UserData.GetUserInfo(user, password);

            #region 判断用户登录信息是否正确，正确则尝试登录，错误则直接返回

            if (userReturn.ReturnCode == UserReturn.ReturnCodeType.Default)
            {
                lock (this)
                {
                    // 如果已查得登录信息
                    if (GuidToUniqueId.ContainsKey(user.Guid) || AccountToUniqueId.ContainsKey(user.Account) ||
                        UniqueIdToUser.ContainsKey(user.UniqueId))
                    {
                        userReturn.ReturnCode = RepeatedLogin;
                        userReturn.DebugMessage.Append("重复登录!");
                    }
                    else
                    {
                        // 添加用户信息

                        GuidToUniqueId.Add(user.Guid, user.UniqueId);

                        user.LoginTime = DateTime.Now;
                        user.Status = Loginning;

                        UniqueIdToUser.Add(user.UniqueId, user);
                        if (!AccountToUniqueId.ContainsKey(user.Account))
                        {
                            AccountToUniqueId.Add(user.Account, user.UniqueId);
                        }
                        if (!NicknameToUniqueId.ContainsKey(user.Nickname))
                        {
                            NicknameToUniqueId.Add(user.Nickname, user.UniqueId);
                        }

                        userReturn.ReturnCode = Success;
                        userReturn.DebugMessage.Append("登录成功!");
                    }
                }
            }

            #endregion

            return userReturn;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UserOffline
        /// 作者：taixihuase
        /// 作用：登出一个用户信息并移除客户端连接
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="guid"></param>
        public void UserOffline(Guid guid)
        {
            lock (this)
            {
                if (GuidToUniqueId.ContainsKey(guid))
                {
                    int uniqueId = GuidToUniqueId[guid];
                    GuidToUniqueId.Remove(guid);

                    if (UniqueIdToUser.ContainsKey(uniqueId))
                    {
                        UserInfo user = GetUser(uniqueId);

                        if (AccountToUniqueId.ContainsKey(user.Account))
                        {
                            AccountToUniqueId.Remove(user.Account);
                        }

                        if (NicknameToUniqueId.ContainsKey(user.Nickname))
                        {
                            NicknameToUniqueId.Remove(user.Nickname);
                        }

                        UniqueIdToUser.Remove(uniqueId);
                    }
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetUser
        /// 作者：taixihuase
        /// 作用：通过编号获取用户信息
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public UserInfo GetUser(int uniqueId)
        {
            UserInfo user;
            UniqueIdToUser.TryGetValue(uniqueId, out user);
            return user;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetUserFromAccount
        /// 作者：taixihuase
        /// 作用：通过账号名获取用户信息
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserInfo GetUserFromAccount(string account)
        {
            if (AccountToUniqueId.ContainsKey(account))
            {
                return GetUser(AccountToUniqueId[account]);
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetUserFromNickName
        /// 作者：taixihuase
        /// 作用：通过昵称获取用户信息
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public UserInfo GetUserFromNickname(string nickname)
        {
            int id;
            if ((id = GetUniqueIdFromNickname(nickname)) >= 0)
            {
                return GetUser(id);
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetUniqueIdFromNickName
        /// 作者：taixihuase
        /// 作用：通过昵称获取用户编号，不存在则返回 -1
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public int GetUniqueIdFromNickname(string nickname)
        {
            if (NicknameToUniqueId.ContainsKey(nickname))
            {
                return NicknameToUniqueId[nickname];
            }
            return -1;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetUniqueIdFromGuid
        /// 作者：taixihuase
        /// 作用：通过客户端连接索引获取用户编号，不存在则返回 -1
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int GetUniqueIdFromGuid(Guid guid)
        {
            if (GuidToUniqueId.ContainsKey(guid))
            {
                return GuidToUniqueId[guid];
            }
            return -1;
        }
    }
}
