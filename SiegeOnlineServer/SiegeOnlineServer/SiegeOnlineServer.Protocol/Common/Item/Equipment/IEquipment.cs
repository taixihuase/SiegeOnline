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
// 创建标识：taixihuase 20150723
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
        void UpdateForgingAttribute(int level, AttributeCode attribute, int value);

        void UpgradeForgingAttribute(AttributeCode attribute, int value);

        void DegradeForgingAttribute();

        void UpgradeCurrentLevel();

        void DegradeCurrentLevel();
    }
}
