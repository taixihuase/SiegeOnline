//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ServerPeer.cs
//
// 文件功能描述：
//
// 服务端与客户端的连线实例
//
// 创建标识：taixihuase 20150712
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.ServerLogic;

namespace SiegeOnlineServer
{
    /// <summary>
    /// 类型：类
    /// 名称：ServerPeer
    /// 作者：taixihuase
    /// 作用：用于服务端与客户端之间的数据传输
    /// 编写日期：2015/7/12
    /// </summary>
    public class ServerPeer : PeerBase
    {
        // 日志
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        // 索引
        public Guid PeerGuid { get; protected set; }

        // 服务端
        public readonly ServerApplication Server;

        /// <summary>
        /// 类型：方法
        /// 名称：ServerPeer
        /// 作者：taixihuase
        /// 作用：构造 ServerPeer 对象
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="unmanagedPeer"></param>
        /// <param name="server"></param>
        public ServerPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer, ServerApplication server) : base(protocol, unmanagedPeer)
        {
            PeerGuid = Guid.NewGuid();
            Server = server;

            // 将当前 peer 加入连线列表
            Server.Users.AddConnectedPeer(PeerGuid, this);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationRequest
        /// 作者：taixihuase
        /// 作用：响应并处理客户端发来的请求
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode)
            {
                // 账号登陆
                case (byte) OperationCode.Login:
                    Login.OnRequest(operationRequest, sendParameters, this);
                    break;

                // 创建新角色
                case (byte) OperationCode.CreateCharacter:
                    CreateCharacter.OnRequest(operationRequest, sendParameters, this);
                    break;

                // 角色进入场景
                case (byte) OperationCode.WorldEnter:
                    WorldEnter.OnRequest(operationRequest, sendParameters, this);
                    break;


            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnect
        /// 作者：taixihuase
        /// 作用：当与客户端失去连接时进行处理
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Server.Players.RemoveCharacter(PeerGuid);
            Server.Users.UserOffline(PeerGuid);
            Server.Users.RemovePeer(PeerGuid);
        }
    }
}
