//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Move.cs
//
// 文件功能描述：
//
// 角色发生位置变动时，响应并组播给其他与之相同区位的玩家以同步更新其位置
//
// 创建标识：taixihuase 20150920
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Photon.SocketServer;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;

namespace SiegeOnlineServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：Move
    /// 作者：taixihuase
    /// 作用：角色位置变动处理逻辑
    /// 编写日期：2015/9/20
    /// </summary>
    public static class Move
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            TryMove(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryMove
        /// 作者：taixihuase
        /// 作用：通过请求的角色位置数据，尝试检测其有效性并组播给其余玩家进行更新
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryMove(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            switch (operationRequest.OperationCode)
            {
                case (byte) OperationCode.Idle:
                    Idle(operationRequest, sendParameters, peer);
                    break;
                case (byte) OperationCode.WalkForward:
                    WalkForward(operationRequest, sendParameters, peer);
                    break;
                case (byte) OperationCode.WalkBackward:
                    WalkBackward(operationRequest, sendParameters, peer);
                    break;
                case (byte) OperationCode.Jump:
                    Jump(operationRequest, sendParameters, peer);
                    break;
                case (byte) OperationCode.JumpForward:
                    JumpForward(operationRequest, sendParameters, peer);
                    break;
                case (byte) OperationCode.JumpBackward:
                    JumpBackward(operationRequest, sendParameters, peer);
                    break;
            }
        }

        #region 响应不同的位置操作

        /// <summary>
        /// 类型：方法
        /// 名称：Idle
        /// 作者：taixihuase
        /// 作用：检测空闲操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void Idle(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.Idle)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：WalkForward
        /// 作者：taixihuase
        /// 作用：检测向前走操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void WalkForward(OperationRequest operationRequest, SendParameters sendParameters,
            ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.WalkForward)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：WalkBackward
        /// 作者：taixihuase
        /// 作用：检测向后走操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void WalkBackward(OperationRequest operationRequest, SendParameters sendParameters,
            ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.WalkBackward)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Jump
        /// 作者：taixihuase
        /// 作用：检测原地跳操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void Jump(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.Jump)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：JumpForward
        /// 作者：taixihuase
        /// 作用：检测向前跳操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void JumpForward(OperationRequest operationRequest, SendParameters sendParameters,
            ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.JumpForward)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：JumpBackward
        /// 作者：taixihuase
        /// 作用：检测向后跳操作有效性并组播
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void JumpBackward(OperationRequest operationRequest, SendParameters sendParameters,
            ServerPeer peer)
        {
            var data = operationRequest[(byte) ParameterCode.Move];
            Position pos = (Position) Serialization.Deserialize(data);
            EventData eventData = new EventData((byte) EventCode.JumpBackward)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.Move, data}},
            };
            eventData.SendTo(
                peer.Server.Maps.PeerList[pos.Map][pos.Zone].Where(p => p.PeerGuid != peer.PeerGuid), sendParameters);
        }
    }

    #endregion
}
