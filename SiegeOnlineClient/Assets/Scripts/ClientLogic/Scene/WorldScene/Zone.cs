//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Zone.cs
//
// 文件功能描述：
//
// 地图区位管理脚本，处理游戏角色所处的地图区位逻辑
//
// 创建标识：taixihuase 20150918
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using ExitGames.Client.Photon;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
using UnityEngine;
// ReSharper disable UnusedMember.Local

namespace Assets.Scripts.ClientLogic.Scene.WorldScene
{
    /// <summary>
    /// 类型：类
    /// 名称：Zone
    /// 作者：taixihuase
    /// 作用：地图区位类
    /// 编写日期：2015/9/17
    /// </summary>
    public class Zone : MonoBehaviour, IEventReceive, IResponseReceive
    {
        public MapCode Map { get; set; }

        public int MapZone { get; set; }

        public List<int> UniqueIdInZone = new List<int>();

        private void Start()
        {
            // 注册方法
            PhotonService.Events.MyZoneUpdate += MyCharacterUpdateZone;
            PhotonService.Events.OtherZoneUpdate += OtherCharacterUpdateZone;
        }

        private void Update()
        {
            
        }

        public void OnEvent(EventData eventData, PhotonService service)
        {
            ZoneUpdateEventArgs e = new ZoneUpdateEventArgs(eventData);
            PhotonService.Events.OnZoneUpdate(service, e);
        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            if (operationResponse.ReturnCode == (short) ErrorCode.Ok)
            {
                ZoneUpdateEventArgs e = new ZoneUpdateEventArgs(operationResponse);
                PhotonService.Events.OnZoneUpdate(service, e);
            }
            else
            {
                Debug.Log(operationResponse.DebugMessage);
            }
        }

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：MyCharacterUpdateZone
        /// 作者：taixihuase
        /// 作用：更新当前角色所处区位中所存在的其他玩家的角色编号列表
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MyCharacterUpdateZone(object sender, ZoneUpdateEventArgs e)
        {
            UniqueIdInZone = e.MyUpdate;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OtherCharacterUpdateZone
        /// 作者：taixihuase
        /// 作用：当有其他玩家从另一个区位转移到当前角色所在区位时，将其角色编号添加进列表
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherCharacterUpdateZone(object sender, ZoneUpdateEventArgs e)
        {
            MapZone zone = e.OtherUpdate;
            if (Map == zone.NextMap && MapZone == zone.NextZone && !UniqueIdInZone.Contains(zone.UniqueId))
            {
                UniqueIdInZone.Add(zone.UniqueId);
            }
            else if (Map == zone.CurrMap && MapZone == zone.CurrZone && UniqueIdInZone.Contains(zone.UniqueId))
            {
                UniqueIdInZone.Remove(zone.UniqueId);
            }
        }

        #endregion

        private void Destroy()
        {
            PhotonService.Events.MyZoneUpdate -= MyCharacterUpdateZone;
            PhotonService.Events.OtherZoneUpdate -= OtherCharacterUpdateZone;
        }
    }
}
