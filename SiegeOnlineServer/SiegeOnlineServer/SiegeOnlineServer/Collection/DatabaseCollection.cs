//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：DatabaseCollection.cs
//
// 文件功能描述：
//
// 数据库集合，用于存放及调用各类数据库接口
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

using SiegeOnlineServer.Database;

namespace SiegeOnlineServer.Collection
{
    /// <summary>
    /// 类型：类
    /// 名称：DatabaseCollection
    /// 作者：taixihuase
    /// 作用：存放服务端所需的数据库传输实例
    /// 编写日期：2015/7/22
    /// </summary>
    public class DatabaseCollection
    {
        // 服务端
        public readonly ServerApplication Server;

        public UserDatabase UserData { get; protected set; }

        public CharacterDatabase CharacterData { get; protected set; }

        public DatabaseCollection(ServerApplication server)
        {
            Server = server;
            UserData = new UserDatabase();
            CharacterData = new CharacterDatabase();
        }
    }
}
