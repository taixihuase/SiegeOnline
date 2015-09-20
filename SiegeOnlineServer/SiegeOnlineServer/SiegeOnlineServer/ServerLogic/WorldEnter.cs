//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：WorldEnter.cs
//
// 文件功能描述：
//
// 进入游戏场景，响应客户端进入场景请求
//
// 创建标识：taixihuase 20150722
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
using Photon.SocketServer;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common.Character;

namespace SiegeOnlineServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：WorldEnter
    /// 作者：taixihuase
    /// 作用：响应进入场景请求
    /// 编写日期：2015/7/22
    /// </summary>
    public static class WorldEnter
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            TryEnter(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryEnter
        /// 作者：taixihuase
        /// 作用：通过角色数据尝试进入场景
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryEnter(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            ServerPeer.Log.Debug("Entering");

            int uniqueId = (int)
                Serialization.Deserialize(operationRequest.Parameters[(byte) ParameterCode.WorldEnter]);

            Character character;
            if (peer.Server.Characters.CharacterEnter(uniqueId, out character))
            {
                peer.Server.Data.CharacterData.GetCharacterPosition(character);
            }

            // 返回数据给客户端

            byte[] pos = Serialization.Serialize(character.Position);
            OperationResponse responseData = new OperationResponse((byte) OperationCode.WorldEnter)

            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.WorldEnter, pos}},
                ReturnCode = (short) ErrorCode.Ok,
                DebugMessage = "进入场景成功"
            };
            peer.SendOperationResponse(responseData, sendParameters);

            byte[] data = Serialization.Serialize(character);
            EventData eventData = new EventData((byte) EventCode.WorldEnter)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.WorldEnter, data}}
            };
            eventData.SendTo(peer.Server.Characters.GamingClientsToBroadcast, sendParameters);
        }
    }
}
