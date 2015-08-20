//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：IItem.cs
//
// 文件功能描述：
//
// 物品操作接口
//
// 创建标识：taixihuase 20150817
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace SiegeOnlineServer.Protocol.Common.Item
{
    /// <summary>
    /// 类型：接口
    /// 名称：IItem
    /// 作者：taixihuase
    /// 作用：物品操作接口
    /// 编写日期：2015/8/17
    /// </summary>
    interface IItem
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Apply
        /// 作者：taixihuase
        /// 作用：使用道具或穿戴道具时，应用道具效果到指定角色上
        /// 编写日期：2015/8/19
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        bool Apply(Character.Character character);

        /// <summary>
        /// 类型：方法
        /// 名称：Cancel
        /// 作者：taixihuase
        /// 作用：道具失效或卸下道具时，取消指定角色上的道具效果
        /// 编写日期：2015/8/19
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        bool Cancel(Character.Character character);
    }
}
