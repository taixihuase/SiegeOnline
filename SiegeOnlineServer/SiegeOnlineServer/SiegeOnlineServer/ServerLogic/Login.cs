//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Login.cs
//
// 文件功能描述：
//
// 登录用户账号，响应客户端登录账号请求
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
using System.Collections.Generic;
using Photon.SocketServer;
using SiegeOnlineServer.Collection;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common.Character;
using SiegeOnlineServer.Protocol.Common.User;
using static SiegeOnlineServer.Collection.CharacterCollection.CharacterReturn.ReturnCodeType;
using static SiegeOnlineServer.Collection.UserCollection.UserReturn.ReturnCodeType;

namespace SiegeOnlineServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：Login
    /// 作者：taixihuase
    /// 作用：响应登录请求
    /// 编写日期：2015/7/14
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            TryLogin(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryLogin
        /// 作者：taixihuase
        /// 作用：通过登录数据尝试登录
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryLogin(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            ServerPeer.Log.Debug("Logining...");

            LoginInfo login = (LoginInfo)
                Serialization.Deserialize(operationRequest.Parameters[(byte) ParameterCode.Login]);

            #region 对账号密码进行判断

            ServerPeer.Log.Debug(DateTime.Now + " : Loginning...");
            ServerPeer.Log.Debug(login.Account);
            ServerPeer.Log.Debug(login.Password);

            // 获取用户资料
            UserInfo user = new UserInfo(peer.PeerGuid, login.Account);
            UserCollection.UserReturn userReturn = peer.Server.Users.UserOnline(user, login.Password);

            // 若成功取得用户资料
            if (userReturn.ReturnCode == UserCollection.UserReturn.ReturnCodeType.Success)
            {
                ServerPeer.Log.Debug(user.LoginTime + " :User " + user.Nickname + " loginning...");

                // 用于选择的数据返回参数
                var parameter = new Dictionary<byte, object>();

                // 用于选择的字符串信息
                string message = "";

                // 用于选择的返回值
                short returnCode = -1;

                #region 获取角色资料

                Character character = new Character(user);
                CharacterCollection.CharacterReturn characterReturn =
                    peer.Server.Characters.SearchCharacter(character);

                // 若取得角色资料
                if (characterReturn.ReturnCode == CharacterCollection.CharacterReturn.ReturnCodeType.Success)
                {
                    byte[] playerBytes = Serialization.Serialize(character);
                    parameter.Add((byte) ParameterCode.Login, playerBytes);
                    returnCode = (short) ErrorCode.Ok;
                    message = "";

                    peer.Server.Characters.CharacterLoad(character);
                }
                else if (characterReturn.ReturnCode == CharacterNotFound)
                {
                    byte[] userBytes = Serialization.Serialize(user);
                    parameter.Add((byte) ParameterCode.Login, userBytes);
                    returnCode = (short) ErrorCode.CharacterNotFound;
                    message = characterReturn.DebugMessage.ToString();
                }

                #endregion

                OperationResponse response = new OperationResponse((byte) OperationCode.Login)
                {
                    Parameters = parameter,
                    ReturnCode = returnCode,
                    DebugMessage = message
                };
                peer.SendOperationResponse(response, sendParameters);
                ServerPeer.Log.Debug(user.LoginTime + " : User " + user.Account + " logins successfully");
            }
            // 若重复登录
            else if (userReturn.ReturnCode == RepeatedLogin)
            {
                OperationResponse response = new OperationResponse((byte) OperationCode.Login)
                {
                    ReturnCode = (short) ErrorCode.RepeatedOperation,
                    DebugMessage = "账号已登录!"
                };
                peer.SendOperationResponse(response, sendParameters);
                ServerPeer.Log.Debug(DateTime.Now + " : Failed to login " + user.Account + " Because of " +
                                     Enum.GetName(typeof (UserCollection.UserReturn.ReturnCodeType),
                                         userReturn.ReturnCode));
            }
            else
            {
                // 返回非法登录错误
                OperationResponse response = new OperationResponse((byte) OperationCode.Login)
                {
                    ReturnCode = (short) ErrorCode.InvalidOperation,
                    DebugMessage = userReturn.DebugMessage.ToString()
                };
                peer.SendOperationResponse(response, sendParameters);
                ServerPeer.Log.Debug(DateTime.Now + " : Failed to login " + user.Account + " Because of " +
                                     Enum.GetName(typeof (UserCollection.UserReturn.ReturnCodeType),
                                         userReturn.ReturnCode));
            }

            #endregion
        }
    }
}
