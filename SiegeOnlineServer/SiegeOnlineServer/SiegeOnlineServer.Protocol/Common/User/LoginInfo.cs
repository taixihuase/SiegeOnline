//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：LoginInfo.cs
//
// 文件功能描述：
//
// 账号登录参数，存放登录操作的账号及密码
//
// 创建标识：taixihuase 20150714
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
    /// 名称：LoginInfo
    /// 作者：taixihuase
    /// 作用：记录登录数据并用于传输
    /// 编写日期：2015/7/14
    /// </summary>
    [Serializable]
    public class LoginInfo
    {
        // 账号名
        public string Account { get; set; }

        // 密码
        public string Password { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：LoginInfo
        /// 作者：taixihuase
        /// 作用：通过账号名和密码构造登陆数据
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public LoginInfo(string account, string password)
        {
            Account = account;
            Password = password;
        }
    }
}
