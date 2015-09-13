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

        // 用户数据库对象
        public UserDatabase UserData { get; protected set; }

        // 角色数据库对象
        public CharacterDatabase CharacterData { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：DatabaseCollection
        /// 作者：taixihuase
        /// 作用：构造 DatabaseCollection 对象
        /// 编写日期：2015/8/1
        /// </summary>
        /// <param name="server"></param>
        public DatabaseCollection(ServerApplication server)
        {
            Server = server;
            UserData = new UserDatabase();
            CharacterData = new CharacterDatabase();
        }
    }
}
