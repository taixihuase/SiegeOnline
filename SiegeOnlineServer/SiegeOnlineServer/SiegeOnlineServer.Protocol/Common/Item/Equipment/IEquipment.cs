//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：IEquipment.cs
//
// 文件功能描述：
//
// 提供给可锻造装备的接口，用于升降级别
//
// 创建标识：taixihuase 20150816
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    /// <summary>
    /// 类型：接口
    /// 名称：IEquipment
    /// 作者：taixihuase
    /// 作用：可锻造装备接口
    /// 编写日期：2015/8/16
    /// </summary>
    interface IEquipment
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Upgrade
        /// 作者：taixihuase
        /// 作用：装备等级提升
        /// 编写日期：2015/8/16
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// </summary>
        void Upgrade(AttributeCode attribute = AttributeCode.Null, float value = 0);

        /// <summary>
        /// 类型：方法
        /// 名称：Degrade
        /// 作者：taixihuase
        /// 作用：装备等级下降
        /// 编写日期：2015/8/16
        /// </summary>
        void Degrade();
    }
}
