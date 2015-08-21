//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：PhotonSingleton.cs
//
// 文件功能描述：
//
// Photon 客户端单例，存放客户端进程实例及服务端信息
//
// 创建标识：taixihuase 20150717
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
using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.PhotonClient
{
    /// <summary>
    /// 类型：类
    /// 名称：PhotonSingleton
    /// 作者：taixihuase
    /// 作用：Photon 单例类，Unity 通过实例化该单例启动 PhotonService 客户端主处理进程
    /// 编写日期：2015/7/17
    /// </summary>
    public class PhotonSingleton : MonoBehaviour
    {
        // 全局静态单例
        private static PhotonSingleton _instance;

        // 单例属性
        public static PhotonSingleton Instance
        {
            get
            {
                // 若获取不到单例，则寻找该单例，并拒绝销毁单例所挂载的对象上
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PhotonSingleton>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        // 客户端主服务进程
        public static PhotonService Service;

        // 服务端 IP 地址
        private static string ServerIp = "203.195.224.120";
        //private static string ServerIp = "127.0.0.1";


        // 服务端端口号
        private static int ServerPort = 4530;

        // 服务端连接协议
        private static ConnectionProtocol ConnectionProtocol = ConnectionProtocol.Tcp;

        // 服务端进程名
        private static string ServerName = "SiegeOnlineServer";

        /// <summary>
        /// 类型：方法
        /// 名称：Awake
        /// 作者：taixihuase
        /// 作用：创建单例
        /// 编写日期：2015/7/17
        /// </summary>
        void Awake()
        {
            // 若当前不存在单例，则创建单例并实例化客户端服务进程
            if (_instance == null)
            {
                _instance = this;
                Service = new PhotonService();
                DontDestroyOnLoad(this);
            }
            else
            {
                // 若已存在一个单例，则销毁该单例所挂载的对象
                if (this != _instance)
                {
                    Destroy(gameObject);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            Service.Connect(ServerIp, ServerPort, ConnectionProtocol, ServerName);
        }

        // Update is called once per frame
        void Update()
        {
            Service.Service();
            Debug.Log(Service.ServerConnected);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnApplicationQuit
        /// 作者：taixihuase
        /// 作用：退出进程
        /// 编写日期：2015/7/17
        /// </summary>
        void OnApplicationQuit()
        {
            Service.Disconnect();
        }
    }
}
