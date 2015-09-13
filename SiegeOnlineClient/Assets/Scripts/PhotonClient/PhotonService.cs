//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：PhotonService.cs
//
// 文件功能描述：
//
// Photon 客户端主进程，进行连线、消息收发及监听等操作
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

using ExitGames.Client.Photon;
using SiegeOnline.ClientLogic.Scene.CharacterScene;
using SiegeOnline.ClientLogic.Scene.UserScene;
using SiegeOnline.ClientLogic.Scene.WorldScene;
using SiegeOnlineServer.Protocol;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.Data.Player;
using UnityEngine;
// ReSharper disable UseNullPropagation
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.PhotonClient
{
    /// <summary>
    /// 类型：类
    /// 名称：PhotonService
    /// 作者：taixihuase
    /// 作用：Photon 客户端主进程
    /// 编写日期：2015/7/17
    /// </summary>
    public class PhotonService : IPhotonPeerListener
    {
        // 连线用的 peer
        public PhotonPeer Peer { protected set; get; }

        // 连线状态
        public bool ServerConnected { protected set; get; }

        // 存放 Debug 信息
        public string DebugMessage { protected set; get; }

        // 事件集合
        public static EventCollection Events = new EventCollection();


        // 玩家数据缓存
        public static PlayerData Player = new PlayerData();

        /// <summary>
        /// 类型：方法
        /// 名称：PhotonService
        /// 作者：taixihuase
        /// 作用：程序运行后，构造客户端主进程实例
        /// 编写日期：2015/7/17
        /// </summary>
        public PhotonService()
        {
            Peer = null;
            ServerConnected = false;
            DebugMessage = "";
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Connect
        /// 作者：taixihuase
        /// 作用：尝试通过 ip 地址、端口、传输协议及服务端进程名，与服务端连线
        /// 编写日期：2015/7/17
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="connectionProtocol"></param>
        /// <param name="serverName"></param>
        public void Connect(string ip, int port, ExitGames.Client.Photon.ConnectionProtocol connectionProtocol,
            string serverName)
        {
            string serverAddress = ip + ":" + port.ToString();
            Peer = new PhotonPeer(this, connectionProtocol);
            Peer.Connect(serverAddress, serverName);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：DebugReturn
        /// 作者：taixihuase
        /// 作用：获取返回的 Debug 消息
        /// 编写日期：2015/7/17
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void DebugReturn(DebugLevel level, string message)
        {
            DebugMessage = message;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationResponse
        /// 作者：taixihuase
        /// 作用：客户端发送请求后，接收并处理相应的服务端响应内容
        /// 编写日期：2015/7/17
        /// </summary>
        /// <param name="operationResponse"></param>
        public void OnOperationResponse(OperationResponse operationResponse)
        {
            switch (operationResponse.OperationCode)
            {
                // 账号注册
                case (byte)OperationCode.Regist:
                    Object.FindObjectOfType<Regist>().OnResponse(operationResponse, this);
                    break;

                // 账号登陆
                case (byte) OperationCode.Login:
                    Object.FindObjectOfType<Login>().OnResponse(operationResponse, this);
                    break;

                // 角色创建
                case (byte)OperationCode.CreateCharacter:
                    Object.FindObjectOfType<CreateCharacter>().OnResponse(operationResponse, this);
                    break;

                // 玩家进入场景
                case (byte) OperationCode.WorldEnter:
                    Object.FindObjectOfType<World>().OnResponse(operationResponse, this);
                    break;

                
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnStatusChanged
        /// 作者：taixihuase
        /// 作用：当连接状态发生改变时，回调触发
        /// 编写日期：2015/7/17
        /// </summary>
        /// <param name="statusCode"></param>
        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:            // 连接
                    ServerConnected = true;
                    break;

                case StatusCode.Disconnect:         // 断线
                    ServerConnected = false;
                    Peer = null;
                    break;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnEvent
        /// 作者：taixihuase
        /// 作用：监听服务端发来的广播并回调触发事件
        /// 编写日期：2015/7/17
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEvent(EventData eventData)
        {
            switch (eventData.Code)
            { 
                // 有玩家进入场景
                case (byte) EventCode.WorldEnter:
                    Object.FindObjectOfType<World>().OnEvent(eventData, this);
                    break;


            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Service
        /// 作者：taixihuase
        /// 作用：呼叫服务
        /// 编写日期：2015/7/17
        /// </summary>
        public void Service()
        {
            if (Peer != null)
            {
                Peer.Service();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Disconnect
        /// 作者：taixihuase
        /// 作用：断开与服务端之间的连接
        /// 编写日期：2015/7/17
        /// </summary>
        public void Disconnect()
        {
            if (Peer != null)
            {
                Peer.Disconnect();
            }
        }
    }
}
