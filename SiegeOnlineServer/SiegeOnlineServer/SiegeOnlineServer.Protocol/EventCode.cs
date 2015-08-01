//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：EventCode.cs
//
// 文件功能描述：
//
// 服务端向客户端发送广播时的事件类型
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

namespace SiegeOnlineServer.Protocol
{
    /// <summary>
    /// 类型：枚举
    /// 名称：EventCode
    /// 作者：taixihuase
    /// 作用：事件代码枚举值
    /// 编写日期：2015/7/14
    /// </summary>
    public enum EventCode : byte
    {
        Connect,
        Disconnect,
        Login,
        Logout,
        CreateCharacter,
        WorldEnter,
        WorldExit,
    }
}
