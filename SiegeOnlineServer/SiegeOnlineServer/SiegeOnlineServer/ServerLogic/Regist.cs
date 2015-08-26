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

using System;
using Photon.SocketServer;
using SiegeOnlineServer.Collection;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common.User;

namespace SiegeOnlineServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：CreateCharacter
    /// 作者：taixihuase
    /// 作用：响应创建角色请求
    /// 编写日期：2015/7/14
    /// </summary>
    public class Regist
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
            TryRegist(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryRegist
        /// 作者：taixihuase
        /// 作用：通过请求的角色数据，尝试创建、记录一个新的角色数据并再次返回给客户端
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryRegist(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            ServerPeer.Log.Debug("Regist a new account...");

            RegistInfo info = (RegistInfo)
                Serialization.Deserialize(operationRequest.Parameters[(byte) ParameterCode.Regist]);

            UserCollection.UserReturn userReturn = peer.Server.Users.RegistUser(info);

            if (userReturn.ReturnCode == UserCollection.UserReturn.ReturnCodeType.Success)
            {
                OperationResponse response = new OperationResponse((byte)OperationCode.Regist)
                {
                    ReturnCode = (short)ErrorCode.Ok,
                    DebugMessage = "账号创建成功！"
                };

                peer.SendOperationResponse(response, sendParameters);
            }
            else
            {
                OperationResponse response = new OperationResponse((byte)OperationCode.Regist)
                {
                    ReturnCode = (short)ErrorCode.InvalidOperation,
                    DebugMessage = userReturn.DebugMessage.ToString()
                };
                peer.SendOperationResponse(response, sendParameters);
                ServerPeer.Log.Debug(DateTime.Now + " : Failed to regist " + info.Account + " Because of " +
                                     Enum.GetName(typeof(UserCollection.UserReturn.ReturnCodeType),
                                         userReturn.ReturnCode));
            }
        }
    }
}
