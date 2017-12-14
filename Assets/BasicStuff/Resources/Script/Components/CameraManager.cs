using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : BaseManager
{
    private CursorLockMode _currentCursorLockMode;

    public void SetCursorLocked(CursorLockMode newMode)
    {
        _currentCursorLockMode = newMode;
        Cursor.lockState = _currentCursorLockMode;
    }

    public CursorLockMode GetCurrentCursorMode()
    {
        return _currentCursorLockMode;
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static CameraManager _instance;

    public static CameraManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
