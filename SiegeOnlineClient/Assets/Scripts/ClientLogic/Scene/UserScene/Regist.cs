using System.Collections.Generic;
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
    public class Regist : MonoBehaviour
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
    }
}
