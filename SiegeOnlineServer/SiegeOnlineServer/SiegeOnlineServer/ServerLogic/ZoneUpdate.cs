//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ZoneUpdate.cs
//
// 文件功能描述：
//
// 角色区位信息发生变动时，响应处理并广播给其他在线玩家该角色的新区位信息
//
// 创建标识：taixihuase 20150917
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
    /// 名称：ZoneUpdate
    /// 作者：taixihuase
    /// 作用：角色区位信息更新处理逻辑
    /// 编写日期：2015/9/17
    /// </summary>
    public static class ZoneUpdate
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            TryUpdate(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryUpdate
        /// 作者：taixihuase
        /// 作用：通过地图区位转换数据进行更新
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryUpdate(OperationRequest operationRequest, SendParameters sendParameters, ServerPeer peer)
        {
            MapZone zone = (MapZone)
                Serialization.Deserialize(operationRequest.Parameters[(byte) ParameterCode.ZoneUpdate]);

            bool ret = peer.Server.Maps.Update(zone);

            if (ret)
            {
                // 将目标区位中存在的角色的编号回应给客户端
                List<int> update = peer.Server.Maps.MapInfo[zone.NextMap][zone.NextZone];
                byte[] characters = Serialization.Serialize(update);
                OperationResponse response = new OperationResponse(
                    (byte) OperationCode.ZoneUpdate)
                {
                    Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.ZoneUpdate, characters}},
                    ReturnCode = (short)ErrorCode.Ok,
                    DebugMessage = "加载角色区位信息成功"
                };
                peer.SendOperationResponse(response, sendParameters);

                // 组播该玩家区位信息给其他客户端
                byte[] data = Serialization.Serialize(zone);
                EventData eventData = new EventData((byte) EventCode.ZoneUpdate)
                {
                    Parameters = new Dictionary<byte, object> {{(byte) ParameterCode.ZoneUpdate, data}}
                };

                // 组播去除该角色的区位信息
                eventData.SendTo(
                    peer.Server.Maps.PeerList[zone.CurrMap][zone.CurrZone].Where(p => p.PeerGuid != peer.PeerGuid),
                    sendParameters);

                // 组播添加该角色的区位信息
                eventData.SendTo(
                    peer.Server.Maps.PeerList[zone.NextMap][zone.NextZone].Where(p => p.PeerGuid != peer.PeerGuid),
                    sendParameters);
            }
            else
            {
                OperationResponse response = new OperationResponse((byte) OperationCode.ZoneUpdate)
                {
                    ReturnCode = (short) ErrorCode.InvalidOperation,
                    DebugMessage = "更新角色区位信息失败"
                };
                peer.SendOperationResponse(response, sendParameters);
            }
        }
    }
}

