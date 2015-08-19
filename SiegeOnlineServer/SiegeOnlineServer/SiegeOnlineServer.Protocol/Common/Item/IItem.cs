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
        void Apply(Character.Character character);

        void Cancel(Character.Character character);
    }
}
