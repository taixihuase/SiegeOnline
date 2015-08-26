//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：EventCollection.cs
//
// 文件功能描述：
//
// 事件集合，存放委托、事件的声明及调用
//
// 创建标识：taixihuase 20150719
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using SiegeOnlineClient.ClientLogic.Event;
// ReSharper disable UseNullPropagation
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：EventCollection
    /// 作者：taixihuase
    /// 作用：事件集合，用于声明委托、事件及其相应判空函数
    /// 编写日期：2015/7/19
    /// </summary>
    public class EventCollection
    {
        #region 与登录相关的委托及事件

        // 登录委托
        public delegate void LoginEventHandler(object sender, LoginEventArgs e);

        // 登录事件
        public event LoginEventHandler MyLogin;

        public void OnLogin(object sender, LoginEventArgs e)
        {
            if (MyLogin != null)
            {
                MyLogin(sender, e);
            }
        }

        #endregion

        #region 与创建角色相关的委托及事件

        // 创建角色委托
        public delegate void CreateCharacterEventHandler(object sender, CreateCharacterEventArgs e);

        // 创建角色事件
        public event CreateCharacterEventHandler MyCreateCharacter;

        public void OnCreateCharacter(object sender, CreateCharacterEventArgs e)
        {
            if (MyCreateCharacter != null)
            {
                MyCreateCharacter(sender, e);
            }
        }

        #endregion

        #region 与加载角色相关的委托及事件

        // 加载角色委托
        public delegate void LoadCharacterEventHandler(object sender, LoadCharacterEventArgs e);

        // 加载角色事件
        public event LoadCharacterEventHandler MyLoadCharacter;

        public void OnLoadCharacter(object sender, LoadCharacterEventArgs e)
        {
            if (MyLoadCharacter != null)
            {
                MyLoadCharacter(sender, e);
            }
        }

        #endregion

        #region 与玩家角色进入场景相关的委托及事件

        // 进入场景委托
        public delegate void WorldEnterEventHandler(object sender, WorldEnterEventArgs e);

        // 自身进入场景
        public event WorldEnterEventHandler MyWorldEnter;

        // 任意进入场景
        public event WorldEnterEventHandler AnyWorldEnter;

        public void OnWorldEnter(object sender, WorldEnterEventArgs e)
        {
            if (e.MyCharacterPosition != null)
            {
                if (MyWorldEnter != null)
                {
                    MyWorldEnter(sender, e);
                }
            }
            else if (e.AnyCharacter != null)
            {
                if (AnyWorldEnter != null)
                {
                    AnyWorldEnter(sender, e);
                }
            }
        }

        #endregion

        #region 其他

        #endregion
    }
}
