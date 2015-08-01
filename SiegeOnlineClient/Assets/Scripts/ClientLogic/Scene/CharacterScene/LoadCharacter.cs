//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：LoadCharacter.cs
//
// 文件功能描述：
//
// 加载角色脚本，处理角色数据的加载逻辑及相关 UI
//
// 创建标识：taixihuase 20150729
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common.User;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

namespace SiegeOnline.ClientLogic.Scene.CharacterScene
{
    /// <summary>
    /// 类型：类
    /// 名称：LoadCharacter
    /// 作者：taixihuase
    /// 作用：加载角色类
    /// 编写日期：2015/7/29
    /// </summary>
    public class LoadCharacter : MonoBehaviour
    {
        public Button EnterGameButton;

        // Use this for initialization
        private void Start()
        {
            PhotonService.Events.MyLoadCharacter += LoadCharacterData;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnLoad
        /// 作者：taixihuase
        /// 作用：触发加载角色事件
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnLoad(object sender, LoadCharacterEventArgs e)
        {
            EnterGameButton.gameObject.SetActive(true);
            FindObjectOfType<CreateCharacter>().CreateCharacterButton.gameObject.SetActive(false);
            PhotonService.Events.OnLoadCharacter(sender, e);
        }

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：LoadCharacterData
        /// 作者：taixihuase
        /// 作用：将玩家角色数据加载进缓存中
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadCharacterData(object sender, LoadCharacterEventArgs e)
        {
            PhotonService.Player.User = new UserBase(e.Character.Guid, e.Character.Account, e.Character.UniqueId,
                e.Character.Nickname, e.Character.Status);
            PhotonService.Player.Character = e.Character;

            Debug.Log(e.Character.Occupation.Name);
            Debug.Log(e.Character.Skills[-1].SkillId);
        }

        #endregion

        #region UI方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnEnterGameDownButton
        /// 作者：taixihuase
        /// 作用：将按下进入游戏按钮时，将进入请求发送给服务端
        /// 编写日期：2015/7/29
        /// </summary>
        public void OnEnterGameButtonDown()
        {
            byte[] data = Serialization.Serialize(PhotonService.Player.Character);
            var parameter = new Dictionary<byte, object>
            {
                {(byte) ParameterCode.WorldEnter, data}
            };
            PhotonSingleton.Service.Peer.OpCustom((byte) OperationCode.WorldEnter, parameter, true);
            Application.LoadLevel("World");
        }

        #endregion

        private void OnDestroy()
        {
            PhotonService.Events.MyLoadCharacter -= LoadCharacterData;
        }
    }
}
