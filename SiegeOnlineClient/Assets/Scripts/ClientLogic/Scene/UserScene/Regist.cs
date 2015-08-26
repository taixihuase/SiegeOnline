using System.Collections.Generic;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using UnityEngine;
using UnityEngine.UI;
using SiegeOnlineServer.Protocol.Common.User;
// ReSharper disable CheckNamespace

namespace SiegeOnline.ClientLogic.Scene.UserScene
{
    public class Regist : MonoBehaviour {

        // 账号输入框
        public InputField Account;

        // 密码输入框
        public InputField Password;

        // 注册按钮
        public Button RegistButton;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        #region UI 方法

        /// <summary>
        /// 类型：方法
        /// 名称：OnLoginButtonDown
        /// 作者：taixihuase
        /// 作用：当按下登录按钮时触发登录事件，将登录信息发送给服务端
        /// 编写日期：2015/7/17
        /// </summary>
        public void OnRegistButtonDown()
        {
            if (PhotonSingleton.Service.ServerConnected)
            {
                
                    var info = new RegistInfo(Account.text, "1234", Password.text);

                    byte[] data = Serialization.Serialize(info);
                    var parameter = new Dictionary<byte, object>
                    {
                        {(byte) ParameterCode.Regist, data}
                    };

                    PhotonSingleton.Service.Peer.OpCustom((byte)OperationCode.Regist, parameter, true);
            }
        }

        #endregion
    }
}
