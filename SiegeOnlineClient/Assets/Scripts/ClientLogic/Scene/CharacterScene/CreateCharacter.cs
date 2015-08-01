//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CreateCharacter.cs
//
// 文件功能描述：
//
// 创建角色脚本，处理角色数据的创建逻辑及相关 UI
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

using ExitGames.Client.Photon;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common.Character;
using SiegeOnlineServer.Protocol.Common.User;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

namespace SiegeOnline.ClientLogic.Scene.CharacterScene
{
    /// <summary>
    /// 类型：类
    /// 名称：CreateCharacter
    /// 作者：taixihuase
    /// 作用：创建角色类
    /// 编写日期：2015/7/29
    /// </summary>
    public class CreateCharacter : MonoBehaviour, IResponseReceive
    {
        public Button CreateCharacterButton;

        // Use this for initialization
        private void Start()
        {
            PhotonService.Events.MyCreateCharacter += CreateCharacterData;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            Character character = (Character)
                Serialization.Deserialize(operationResponse.Parameters[(byte) ParameterCode.CreateCharacter]);
            LoadCharacterEventArgs e = new LoadCharacterEventArgs(character);
            FindObjectOfType<LoadCharacter>().OnLoad(service, e);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnCreate
        /// 作者：taixihuase
        /// 作用：触发创建角色事件
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCreate(object sender, CreateCharacterEventArgs e)
        {
            CreateCharacterButton.gameObject.SetActive(true);
            FindObjectOfType<LoadCharacter>().EnterGameButton.gameObject.SetActive(false);
            PhotonService.Events.OnCreateCharacter(sender, e);
        }

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnCreate
        /// 作者：taixihuase
        /// 作用：当创建角色事件触发时，创建角色数据并发送给服务端
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateCharacterData(object sender, CreateCharacterEventArgs e)
        {
            PhotonService.Player.User = new UserBase(e.User.Guid, e.User.Account, e.User.UniqueId, e.User.Nickname,
                e.User.Status);
            Debug.Log(e.User.Nickname);
        }

        #endregion

        #region UI方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnCreateCharacterButtonDown
        /// 作者：taixihuase
        /// 作用：将按下创建角色按钮时，判断数据完整性并发送给服务端
        /// 编写日期：2015/7/29
        /// </summary>
        public void OnCreateCharacterButtonDown()
        {
            
        }

        #endregion
        private void OnDestroy()
        {
            PhotonService.Events.MyCreateCharacter -= CreateCharacterData;
        }
    }
}
