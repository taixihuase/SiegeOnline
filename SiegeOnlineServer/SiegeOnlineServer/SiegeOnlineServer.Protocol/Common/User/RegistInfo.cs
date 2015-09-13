//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：RegistInfo.cs
//
// 文件功能描述：
//
// 注册新账号信息
//
// 创建标识：taixihuase 20150826
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
    /// 名称：RegistInfo
    /// 作者：taixihuase
    /// 作用：注册账号信息
    /// 编写日期：2015/8/26
    /// </summary>
    [Serializable]
    public class RegistInfo
    {
        // 账号名
        public string Account { get; set; }

        // 昵称
        public string Nickname { get; set; }

        // 密码
        public string Password { get; set; }

        // 注册时间
        public DateTime RegistTime { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistInfo
        /// 作者：taixihuase
        /// 作用：通过账号名、昵称和密码构造注册数据
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        public RegistInfo(string account, string nickname, string password)
        {
            Account = account;
            Nickname = nickname;
            Password = password;
            RegistTime = DateTime.Now;
        }
    }
}
