//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：World.cs
//
// 文件功能描述：
//
// 世界场景脚本，处理游戏主场景的逻辑及相关 UI
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

using ExitGames.Client.Photon;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable UnusedMember.Local
// ReSharper disable CheckNamespace

namespace SiegeOnline.ClientLogic.Scene.WorldScene
{
    public class World : MonoBehaviour, IEventReceive, IResponseReceive
    {
        // 玩家上线提示文本
        public Text LoginTip;

        // Use this for initialization
        void Start()
        {
            PhotonService.Events.MyWorldEnter += MyWorldPlayerEnter;
            PhotonService.Events.AnyWorldEnter += AnyPlayerEnter;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            // 判断事件类型并调用对应的方法
            switch (operationResponse.OperationCode)
            {
                // 玩家角色进入场景
                case (byte) OperationCode.WorldEnter:
                    OnEnter(operationResponse, service);
                    break;
            }
        }

        public void OnEvent(EventData eventData, PhotonService service)
        {
            // 判断事件类型并调用对应的方法
            switch (eventData.Code)
            {
                // 玩家角色进入场景
                case (byte) EventCode.WorldEnter:
                    OnEnter(eventData, service);
                    break;
            }
        }

        #region 用于触发事件时选择的事件类型

        #region 玩家角色进入场景

        /// <summary>
        /// 类型：方法
        /// 名称：OnEnter
        /// 作者：taixihuase
        /// 作用：当自身角色进入场景时，触发事件
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="operationResponse"></param>
        /// <param name="service"></param>
        private void OnEnter(OperationResponse operationResponse, PhotonService service)
        {
            WorldEnterEventArgs e = new WorldEnterEventArgs(operationResponse);
            PhotonService.Events.OnWorldEnter(service, e);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnEnter
        /// 作者：taixihuase
        /// 作用：当有玩家进入场景时，触发事件
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="service"></param>
        private void OnEnter(EventData eventData, PhotonService service)
        {
            WorldEnterEventArgs e = new WorldEnterEventArgs(eventData);
            PhotonService.Events.OnWorldEnter(service, e);
        }

        #endregion

        #endregion

        #region 用于注册事件的方法

        #region 玩家角色进入场景

        /// <summary>
        /// 类型：方法
        /// 名称：MyWorldPlayerEnter
        /// 作者：taixihuase
        /// 作用：当自己角色进入游戏场景时
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyWorldPlayerEnter(object sender, WorldEnterEventArgs e)
        {
            PhotonService.Player.CharacterCopy.Position = e.MyCharacterPosition;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AnyWorldPlayerEnter
        /// 作者：taixihuase
        /// 作用：当任意角色进入游戏场景时
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnyPlayerEnter(object sender, WorldEnterEventArgs e)
        {
            #region 测试用例

            LoginTip.text = "玩家 " + e.AnyCharacter.Nickname + " 上线了！";
            Debug.Log(e.AnyCharacter.Armors[1].DefensePoints[1]);
            foreach (var fixedAttribute in e.AnyCharacter.Weapons[1].FixedAttributes)
            {
                Debug.Log(fixedAttribute.Value);
            }
            Debug.Log(e.AnyCharacter.Weapons[1].ForgingAttributes[4].Value);
            Debug.Log(e.AnyCharacter.Position.Z);
            Debug.Log(e.AnyCharacter.Attribute.EnhanceLightning);
            Debug.Log(e.AnyCharacter.Attribute.ResistanceLightning);
            Debug.Log(e.AnyCharacter.Experience.Level);
            Debug.Log(e.AnyCharacter.Experience.GainedExperience);

            #endregion
        }

        #endregion

        #endregion

        #region UI方法

        #endregion

        void OnDestroy()
        {
            PhotonService.Events.MyWorldEnter -= MyWorldPlayerEnter;
            PhotonService.Events.AnyWorldEnter -= AnyPlayerEnter;
        }
    }
}
