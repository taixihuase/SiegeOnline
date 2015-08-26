//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：UserInfo.cs
//
// 文件功能描述：
//
// 用户账号信息
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
//----------------------------------------------------------------------------------------------------------

using System;

namespace SiegeOnlineServer.Protocol.Common.User
{
    /// <summary>
    /// 类型：类
    /// 名称：UserInfo
    /// 作者：taixihuase
    /// 作用：用户信息
    /// 编写日期：2015/7/12
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        // 索引
        public Guid Guid { get; set; }

        // 编号
        public int UniqueId { get; set; }

        // 账号名
        public string Account { get; set; }

        // 昵称
        public string Nickname { get; set; }

        // 状态
        public StatusType Status { get; set; }

        // 状态枚举值
        [Serializable]
        public enum StatusType : byte
        {
            Default             =   0,          // 默认值
            Loginning           =   1,          // 上线中
            Gaming              =   2           // 游戏中
        }

        // 登录时间
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：UserInfo
        /// 作者：taixihuase
        /// 作用：通过参数值构造 UserInfo 对象
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="uniqueId"></param>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="status"></param>
        public UserInfo(Guid guid, string account, int uniqueId = -1, string nickname = "", StatusType status = StatusType.Default)
        {
            Guid = guid;
            UniqueId = uniqueId;
            Account = account;
            Nickname = nickname;
            Status = status;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UserInfo
        /// 作者：taixihuase
        /// 作用：通过现有的 UserInfo 对象构造新的 UserInfo 对象
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="user"></param>
        public UserInfo(UserInfo user)
        {
            Guid = user.Guid;
            UniqueId = user.UniqueId;
            Account = user.Account;
            Nickname = user.Nickname;
        }
    }
}
