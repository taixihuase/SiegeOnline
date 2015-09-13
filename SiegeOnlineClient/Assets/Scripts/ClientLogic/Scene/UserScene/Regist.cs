//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Regist.cs
//
// 文件功能描述：
//
// 注册账号脚本，处理注册的逻辑及相关 UI
//
// 创建标识：taixihuase 20150827
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
using ExitGames.Client.Photon;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.Component;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using UnityEngine;
using UnityEngine.UI;
using SiegeOnlineServer.Protocol.Common.User;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

namespace SiegeOnline.ClientLogic.Scene.UserScene
{
    /// <summary>
    /// 类型：类
    /// 名称：Regist
    /// 作者：taixihuase
    /// 作用：客户端注册类
    /// 编写日期：2015/8/27
    /// </summary>
    public class Regist : MonoBehaviour, IResponseReceive
    {
        // 注册父对象
        public GameObject RegistObj;

        // 注册提示信息
        public Text RegistTip;

        // 登录邮箱输入框
        public InputField Email;

        // 昵称输入框
        public InputField Nickname;

        // 昵称全角字符长度
        private int _nicknameSbcLength;

        // 密码输入框
        public InputField Password;

        // 密码确认框
        public InputField PasswordAgain;

        // 确认按钮
        public Button SubmitButton;

        // 取消按钮
        public Button CancelButton;

        // 昵称长度限制
        public int MinNicknameLength;

        public int MaxNicknameLength;

        // 密码长度限制
        public int MinPasswordLength;

        public int MaxPasswordLength;

        // Use this for initialization
        private void Start()
        {
            RegistObj.SetActive(false);
            _nicknameSbcLength = 0;

            // 注册方法
            PhotonService.Events.MyRegist += Success;
            PhotonService.Events.MyRegist += EmailNotExist;
            PhotonService.Events.MyRegist += RepeatedRegist;
        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            RegistEventArgs e = new RegistEventArgs(operationResponse);
            PhotonService.Events.OnRegist(service, e);
        }

        #region UI 方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnSubmitButtonClick
        /// 作者：taixihuase
        /// 作用：当按下确认按钮时触发提交注册事件，将注册信息发送给服务端
        /// 编写日期：2015/9/3
        /// </summary>
        public void OnSubmitButtonClick()
        {
            if (PhotonSingleton.Service.ServerConnected)
            {
                if (CheckRegistInfo())
                {
                    var info = new RegistInfo(Email.text, Nickname.text, Password.text);
                    byte[] data = Serialization.Serialize(info);
                    var parameter = new Dictionary<byte, object>
                    {
                        {(byte) ParameterCode.Regist, data}
                    };
                    PhotonSingleton.Service.Peer.OpCustom((byte) OperationCode.Regist, parameter, true);
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnCancelButtonClick
        /// 作者：taixihuase
        /// 作用：当按下取消按钮时触发取消注册事件，关闭注册界面并返回登录界面
        /// 编写日期：2015/9/5
        /// </summary>
        public void OnCancelButtonClick()
        {
            FindObjectOfType<Login>().LoginObj.SetActive(true);
            RegistTip.text = "";
            Email.text = "";
            Nickname.text = "";
            Password.text = "";
            PasswordAgain.text = "";
            RegistObj.SetActive(false);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CheckRegistInfo
        /// 作者：taixihuase
        /// 作用：检测注册信息有效性
        /// 编写日期：2015/9/5
        /// </summary>
        private bool CheckRegistInfo()
        {
            if (Email.text.Length == 0)
            {
                RegistTip.text = "注册邮箱不能为空！";
                return false;
            }
            if (!StringTool.IsEmail(Email.text))
            {
                RegistTip.text = "注册邮箱格式不正确！";
                return false;
            }
            if (Nickname.text.Length == 0)
            {
                RegistTip.text = "游戏昵称不能为空！";
                return false;
            }
            _nicknameSbcLength = StringTool.CountSbcCase(Nickname.text);
            if (Nickname.text.Length + _nicknameSbcLength < MinNicknameLength)
            {
                RegistTip.text = "游戏昵称至少占" + MinNicknameLength + "个字符！";
                return false;
            }
            if (Nickname.text.Length + _nicknameSbcLength > MaxNicknameLength)
            {
                RegistTip.text = "游戏昵称最多占" + MaxNicknameLength + "个字符！";
                return false;
            }
            if (Password.text.Length == 0)
            {
                RegistTip.text = "登录密码不能为空！";
                return false;
            }
            if (!StringTool.IsLetterOrNumber(Password.text))
            {
                RegistTip.text = "登录密码只能包含字母或数字！";
                Password.text = "";
                PasswordAgain.text = "";
                return false;
            }
            if (Password.text.Length < MinPasswordLength)
            {
                RegistTip.text = "登录密码至少占" + MinPasswordLength + "个字符！";
                return false;
            }
            if (Password.text.Length > MaxPasswordLength)
            {
                RegistTip.text = "登录密码最多占" + MaxPasswordLength + "个字符！";
                return false;
            }
            if (PasswordAgain.text.Length == 0)
            {
                RegistTip.text = "请再次确认登录密码！";
                return false;
            }
            if (PasswordAgain.text != Password.text)
            {
                RegistTip.text = "两次输入的密码不一致！";
                Password.text = "";
                PasswordAgain.text = "";
                return false;
            }
            return true;
        }

        #endregion

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：Success
        /// 作者：taixihuase
        /// 作用：当成功进行注册时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Success(object sender, RegistEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.Ok)
            {
                Debug.Log(e.OperationResponse.DebugMessage);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：EmailNotExist
        /// 作者：taixihuase
        /// 作用：当注册邮箱无效时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailNotExist(object sender, RegistEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.EmailNotFound)
            {
                Debug.Log(e.OperationResponse.DebugMessage);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RepeatedRegist
        /// 作者：taixihuase
        /// 作用：当尝试用一个已注册过的邮箱或游戏昵称进行注册时触发
        /// 编写日期：2015/7/29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepeatedRegist(object sender, RegistEventArgs e)
        {
            if (e.OperationResponse.ReturnCode == (short) ErrorCode.RepeatedOperation)
            {
                Debug.Log(e.OperationResponse.DebugMessage);
            }
        }

        #endregion

        private void OnDestroy()
        {
            PhotonService.Events.MyRegist -= Success;
            PhotonService.Events.MyRegist -= EmailNotExist;
            PhotonService.Events.MyRegist -= RepeatedRegist;
        }
    }
}
