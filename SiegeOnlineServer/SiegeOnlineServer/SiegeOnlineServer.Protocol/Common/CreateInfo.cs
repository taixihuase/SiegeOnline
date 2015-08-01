//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CreateInfo.cs
//
// 文件功能描述：
//
// 创建角色的请求参数，由客户端选择职业对象后发送
//
// 创建标识：taixihuase 20150725
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
using SiegeOnlineServer.Protocol.Common.User;

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：CreateInfo
    /// 作者：taixihuase
    /// 作用：创建新角色请求参数
    /// 编写日期：2015/7/25
    /// </summary>
    [Serializable]
    public class CreateInfo : UserBase
    {
        public byte Occupation { get; set; }

        [Serializable]
        public enum OccupationTypes : byte
        {
            Warrior,
        }

        public CreateInfo(UserBase user, OccupationTypes occupation)
            : base(user)
        {
            Occupation = (byte) occupation;
        }
    }
}
