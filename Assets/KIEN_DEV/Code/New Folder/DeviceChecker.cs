using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceChecker : Code_Singleton<DeviceChecker>
{   


    public RuntimePlatform Device;
    void Start()
    {
            
    }

    public bool IsMobile()
    {
        return Device == RuntimePlatform.Android || Device == RuntimePlatform.IPhonePlayer;
    }

    public bool IsPC()
    {
        return Device == RuntimePlatform.WindowsPlayer || Device == RuntimePlatform.OSXPlayer;
    }

    public bool IsEditor()
    {
        return Device == RuntimePlatform.WindowsEditor || Device == RuntimePlatform.OSXEditor;
    }
}
