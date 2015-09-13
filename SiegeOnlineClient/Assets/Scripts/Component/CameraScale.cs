//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CameraScale.cs
//
// 文件功能描述：
//
// 摄像机缩放类，用于 UI 自适应和景深设置
//
// 创建标识：taixihuase 20150905
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using UnityEngine;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local
// ReSharper disable LocalVariableHidesMember

namespace SiegeOnlineClient.Component
{
    /// <summary>
    /// 类型：类
    /// 名称：CameraScale
    /// 作者：taixihuase
    /// 作用：UI 摄像机缩放类
    /// 编写日期：2015/9/5
    /// </summary>
    public class CameraScale : MonoBehaviour
    {
        void Start()
        {
            float ManualWidth = 1024;
            float ManualHeight = 768;
            int manualHeight;
            if (System.Convert.ToSingle(Screen.height) / Screen.width > ManualHeight / ManualWidth)
                manualHeight = Mathf.RoundToInt(ManualWidth / Screen.width * Screen.height);
            else
                manualHeight = Mathf.RoundToInt(ManualHeight);
            Camera camera = GetComponent<Camera>();
            float scale = manualHeight / ManualHeight;
            camera.fieldOfView *= scale;
        }
    }
}
