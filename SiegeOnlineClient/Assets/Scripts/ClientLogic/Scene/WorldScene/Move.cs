//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Move.cs
//
// 文件功能描述：
//
// 位置移动脚本，处理游戏角色位置变动的逻辑
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
//----------------------------------------------------------------------------------------------------------

using ExitGames.Client.Photon;
using SiegeOnlineClient.ClientLogic;
using SiegeOnlineClient.ClientLogic.Event;
using SiegeOnlineClient.PhotonClient;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
using UnityEngine;

namespace Assets.Scripts.ClientLogic.Scene.WorldScene
{
    /// <summary>
    /// 类型：类
    /// 名称：Move
    /// 作者：taixihuase
    /// 作用：位置移动类
    /// 编写日期：2015/9/20
    /// </summary>
    public class Move : MonoBehaviour, IEventReceive, IResponseReceive
    {
        private void Start()
        {
            PhotonService.Events.MyMove += WrongMove;
            PhotonService.Events.OtherMove += OtherMove;
        }

        private void Update()
        {

        }

        public void OnResponse(OperationResponse operationResponse, PhotonService service)
        {
            MoveEventArgs e = new MoveEventArgs(operationResponse);
            PhotonService.Events.OnMove(service, e);
        }

        public void OnEvent(EventData eventData, PhotonService service)
        {
            Position pos = (Position)
                Serialization.Deserialize(eventData[(byte)ParameterCode.Move]);
            MoveEventArgs e = new MoveEventArgs(pos);
            PhotonService.Events.OnMove(service, e);

        }

        #region 用于注册事件的方法

        /// <summary>
        /// 类型：方法
        /// 名称：WrongMove
        /// 作者：taixihuase
        /// 作用：当自身角色的移动被判定为无效时回滚操作
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WrongMove(object sender, MoveEventArgs e)
        {
            if (e.MyMove.ReturnCode == (short) ErrorCode.InvalidOperation)
            {
                
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OtherMove
        /// 作者：taixihuase
        /// 作用：当其他角色位置发生有效的变动时，更新其位置信息
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherMove(object sender, MoveEventArgs e)
        {

        }

        #endregion

        private void Destroy()
        {
            PhotonService.Events.MyMove -= WrongMove;
            PhotonService.Events.OtherMove -= OtherMove;
        }
    }
}
