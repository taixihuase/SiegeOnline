//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：OperationCode.cs
//
// 文件功能描述：
//
// 客户端操作类别
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
    /// 名称：OperationCode
    /// 作者：taixihuase
    /// 作用：客户端操作代码枚举值
    /// 编写日期：2015/7/14
    /// </summary>
    public enum OperationCode : byte
    {
        #region 基本操作码

        Connect, // 连接
        Disconnect, // 断线
        Regist, // 注册账号
        Login, // 登录账号
        Logout, // 登出账号
        CreateCharacter, // 创建角色
        WorldEnter, // 进入游戏场景
        WorldExit, // 退出游戏场景
        ZoneUpdate, // 地图区位转换

        #endregion

        #region 角色动作码

        Idle, // 空闲
        WalkForward, // 向前走
        WalkBackward, // 倒退走
        Jump, // 原地跳跃
        JumpForward, // 向前跳跃
        JumpBackward, // 向后跳跃
        Stun, // 晕眩
        Chaos, // 混乱
        Attack, // 普通攻击
        Cast, // 施法
        Die, // 死亡

        #endregion
    }
}
