//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CreateCharacter.cs
//
// 文件功能描述：
//
// 创建新角色，响应客户端创建角色请求
//
// 创建标识：taixihuase 20150714
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
    /// 名称：CreateCharacter
    /// 作者：taixihuase
    /// 作用：响应创建角色请求
    /// 编写日期：2015/7/14
    /// </summary>
    public static class CreateCharacter
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2015/7/24
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            TryCreate(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryCreate
        /// 作者：taixihuase
        /// 作用：通过请求的角色数据，尝试创建、记录一个新的角色数据并再次返回给客户端
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryCreate(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            ServerPeer.Log.Debug("Create a new character...");

            Character character = (Character)
                Serialization.Deserialize(operationRequest.Parameters[(byte) ParameterCode.CreateCharacter]);

            peer.Server.Data.CharacterData.CreateNewCharacter(character);
            peer.Server.Characters.CharacterLoad(character);

            byte[] data = Serialization.Serialize(character);
            OperationResponse response = new OperationResponse((byte) OperationCode.CreateCharacter)
            {
                Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.CreateCharacter, data}},
                ReturnCode = (short)ErrorCode.Ok,
                DebugMessage = "创建角色成功！"
            };

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
