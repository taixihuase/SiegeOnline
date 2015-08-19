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
        /// 名称：UpdateForgingAttribute
        /// 作者：taixihuase
        /// 作用：变更一条锻造属性
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="level"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        void UpdateForgingAttribute(int level, AttributeCode attribute, float value);

        /// <summary>
        /// 类型：方法
        /// 名称：UpgradeForgingAttribute
        /// 作者：taixihuase
        /// 作用：添加一条更高一级锻造等级的锻造属性
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        void UpgradeForgingAttribute(AttributeCode attribute, float value);

        /// <summary>
        /// 类型：方法
        /// 名称：DegradeForgingAttribute
        /// 作者：taixihuase
        /// 作用：去除一条当前锻造等级的锻造属性
        /// 编写日期：2015/8/16
        /// </summary>
        void DegradeForgingAttribute();

        /// <summary>
        /// 类型：方法
        /// 名称：UpgradeCurrentLevel
        /// 作者：taixihuase
        /// 作用：装备等级提升
        /// 编写日期：2015/8/16
        /// </summary>
        void UpgradeCurrentLevel();

        /// <summary>
        /// 类型：方法
        /// 名称：DegradeCurrentLevel
        /// 作者：taixihuase
        /// 作用：装备等级下降
        /// 编写日期：2015/8/16
        /// </summary>
        void DegradeCurrentLevel();
    }
}
