//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ServerApplication.cs
//
// 文件功能描述：
//
// 服务端主应用程序
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
using System.IO;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using SiegeOnlineServer.Collection;
using LogManager = ExitGames.Logging.LogManager;

namespace SiegeOnlineServer
{
    /// <summary>
    /// 类型：类
    /// 名称：ServerApplication
    /// 作者：taixihuase
    /// 作用：服务端应用程序
    /// 编写日期：2015/7/12
    /// </summary>
    public class ServerApplication : ApplicationBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public DatabaseCollection Data;

        public UserCollection Users;

        public PlayerCollection Players;

        /// <summary>
        /// 类型：方法
        /// 名称：CreatePeer
        /// 作者：taixihuase
        /// 作用：每当一个客户端连接时，生成一个新的 peer 并回传给 Photon Server
        /// 编写日期：2015/7/12
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ServerPeer(initRequest.Protocol, initRequest.PhotonPeer, this);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Setup
        /// 作者：taixihuase
        /// 作用：启动并初始化客户端
        /// 编写日期：2015/7/12
        /// </summary>
        protected override void Setup()
        {
            // 创建日志
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationRootPath, "log");

            string path = Path.Combine(BinaryPath, "log4net.config");
            var file = new FileInfo(path);
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }

            Log.Debug(DateTime.Now + " : Server is running");

            // 初始化数据库操作列表
            Data = new DatabaseCollection(this);

            // 初始化用户列表
            Users = new UserCollection(this);

            // 初始化玩家角色列表
            Players = new PlayerCollection(this);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TearDown
        /// 作者：taixihuase
        /// 作用：关闭客户端并释放资源
        /// 编写日期：2015/7/12
        /// </summary>
        protected override void TearDown()
        {
            Log.Debug(DateTime.Now + " : Server stops");
        }
    }
}
