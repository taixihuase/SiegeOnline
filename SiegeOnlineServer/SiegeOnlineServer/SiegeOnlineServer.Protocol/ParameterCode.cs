//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ParameterCode.cs
//
// 文件功能描述：
//
// 数据包传输的参数类别
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
    /// 名称：ParameterCode
    /// 作者：taixihuase
    /// 作用：数据参数代码枚举值
    /// 编写日期：2015/7/14
    /// </summary>
    public enum ParameterCode : byte
    {
        Connect,
        Disconnect,
        Regist,
        Login,
        Logout,
        CreateCharacter,
        WorldEnter,
        WorldExit,
    }
}
