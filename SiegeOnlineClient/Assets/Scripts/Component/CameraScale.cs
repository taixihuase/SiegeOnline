using UnityEngine;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local
// ReSharper disable LocalVariableHidesMember

namespace SiegeOnlineClient.Component
{
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
