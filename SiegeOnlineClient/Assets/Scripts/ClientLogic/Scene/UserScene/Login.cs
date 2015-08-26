//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Login.cs
//
// 文件功能描述：
//
// 登录场景脚本，处理登录的逻辑及相关 UI
//
// 创建标识：taixihuase 20150717
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using SiegeOnline.ClientLogic.Scene.CharacterScene;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using UnityEngine;
using UnityEngine.UI;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineServer.Protocol.Common.Character;
using SiegeOnlineServer.Protocol.Common.User;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

namespace SiegeOnline.ClientLogic.Scene.UserScene
{
    /// <summary>
    /// 类型：类
    /// 名称：Login
    /// 作者：taixihuase
    /// 作用：客户端登录类
    /// 编写日期：2015/7/17
    /// </summary>
    public class Login : MonoBehaviour, IResponseReceive
    {
        // 登录参数
        private LoginInfo _loginInfo;

        // 账号输入框
        public InputField Account;

        // 密码输入框
        public InputField Password;

        // 登录按钮
        public Button LoginButton;

        // 退出按钮
        public Button ExitButton;

        // Use this for initialization
        private void Start()
        {
            // 注册方法
            PhotonService.Events.MyLogin += CharacterNotExist;
            PhotonService.Events.MyLogin += ErrorInput;
            PhotonService.Events.MyLogin += RepeatedLogin;
            PhotonService.Events.MyLogin += CharacterExist;
        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            LoginEventArgs e = new LoginEventArgs(operationResponse);
            PhotonService.Events.OnLogin(service, e);
        }

        #region UI 方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnLoginButtonDown
        /// 作者：taixihuase
        /// 作用：当按下登录按钮时触发登录事件，将登录信息发送给服务端
        /// 编写日期：2015/7/17
        /// </summary>
        public void OnLoginButtonDown()
        {
            if (PhotonSingleton.Service.ServerConnected)
            {
                if (Account.text.Length > 0 && Password.text.Length > 0)
                {
                    _loginInfo = new LoginInfo(Account.text, Password.text);

                    byte[] data = Serialization.Serialize(_loginInfo);
                    var parameter = new Dictionary<byte, object>
                    {
                        {(byte) ParameterCode.Login, data}
                    };

                    PhotonSingleton.Service.Peer.OpCustom((byte)OperationCode.Login, parameter, true);
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnExitButtonDown
        /// 作者：taixihuase
        /// 作用：当按下退出按钮时触发退出事件，退出进程，Debug模式无效
        /// 编写日期：2015/7/17
        /// </summary>
        public void OnExitButtonDown()
        {
            Application.Quit();
        }

        #endregion

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：ErrorInput
        /// 作者：taixihuase
        /// 作用：当账号或密码有误时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErrorInput(object sender, LoginEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.InvalidOperation)
            {
                Debug.Log(e.OperationResponse.DebugMessage);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RepeatedLogin
        /// 作者：taixihuase
        /// 作用：当尝试登录一个已在线账号时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepeatedLogin(object sender, LoginEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.RepeatedOperation)
            {
                Debug.Log(e.OperationResponse.DebugMessage);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CharacterExist
        /// 作者：taixihuase
        /// 作用：当登录账号成功并且成功获取到当前账号的角色数据时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterExist(object sender, LoginEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.Ok)
            {
                DontDestroyOnLoad(transform.parent);
                Application.LoadLevel("Character");

                Character character = (Character)
                    Serialization.Deserialize(e.OperationResponse.Parameters[(byte) ParameterCode.Login]);

                StartCoroutine(LoadCharacter(sender, character));
            }
        }

        /// <summary>
        ///  类型：方法
        /// 名称：CharacterNotExist
        /// 作者：taixihuase
        /// 作用：当登录账号成功并且该账号未创建角色时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterNotExist(object sender, LoginEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short)ErrorCode.CharacterNotFound)
            {
                DontDestroyOnLoad(transform.parent);
                Application.LoadLevel("Character");

                UserInfo user = (UserInfo)
                    Serialization.Deserialize(e.OperationResponse.Parameters[(byte)ParameterCode.Login]);
                Debug.Log(user.Nickname + " have no character...");

                StartCoroutine(CreateCharacter(sender, user));
            }
        }

        #endregion

        #region 协程方法

        /// <summary>
        /// 类型：方法
        /// 名称：LoadCharacter
        /// 作者：taixihuase
        /// 作用：当成功获取到角色数据时触发加载角色事件
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        private IEnumerator LoadCharacter(object sender, Character character)
        {
            LoadCharacter load;
            while ((load = FindObjectOfType<LoadCharacter>()) == null)
            {
                yield return null;
            }
            LoadCharacterEventArgs lc = new LoadCharacterEventArgs(character);
            load.OnLoad(sender, lc);

            Destroy(transform.parent.gameObject);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateCharacter
        /// 作者：taixihuase
        /// 作用：当成功获取到角色数据时触发创建角色事件
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private IEnumerator CreateCharacter(object sender, UserInfo user)
        {
            CreateCharacter create;
            while ((create = FindObjectOfType<CreateCharacter>()) == null)
            {
                yield return null;
            }
            CreateCharacterEventArgs cc = new CreateCharacterEventArgs(user);
            create.OnCreate(sender, cc);

            Destroy(transform.parent.gameObject);
        }

        #endregion

        private void OnDestroy()
        {
            PhotonService.Events.MyLogin -= CharacterNotExist;
            PhotonService.Events.MyLogin -= ErrorInput;
            PhotonService.Events.MyLogin -= RepeatedLogin;
            PhotonService.Events.MyLogin -= CharacterExist;
        }
    }
}

