using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager
{
    private Vector2 _lastMousePos = Vector2.zero;

    public KeyCode forward = KeyCode.Z;
    public KeyCode backward = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.Q;
    public KeyCode pause = KeyCode.P;
    public KeyCode menu = KeyCode.I;
    public KeyCode action = KeyCode.E;

    private void Update()
    {
        _lastMousePos = Input.mousePosition;
    }

    // Enter
    public bool GetEnterPressed()
    {
        return Input.GetKeyDown(KeyCode.KeypadEnter);
    }

    public bool GetEnterReleased()
    {
        return Input.GetKeyUp(KeyCode.KeypadEnter);
    }

    public bool GetEnterState()
    {
        return Input.GetKey(KeyCode.KeypadEnter);
    }

    // Space Bar
    public bool GetSpaceBarPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetSpaceBarReleased()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public bool GetSpaceBarState()
    {
        return Input.GetKey(KeyCode.Space);
    }

    // Escape
    public bool GetEscapePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public Vector2 GetLastMousePosition()
    {
        return _lastMousePos;
    }

    public float GetHorizontalMouseMovement()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetVerticalMouseMovement()
    {
        return Input.GetAxis("Mouse Y");
    }

    public bool GetPausePressed()
    {
        return Input.GetKeyDown(pause);
    }

    public bool GetMenuPressed()
    {
        return Input.GetKeyDown(menu);
    }

    public bool GetActionPressed()
    {
        return Input.GetKeyDown(action);
    }

    public bool GetForwardState()
    {
        return Input.GetKey(forward);
    }

    public bool GetBackwardState()
    {
        return Input.GetKey(backward);
    }

    public bool GetRightState()
    {
        return Input.GetKey(right);
    }

    public bool GetLeftState()
    {
        return Input.GetKey(left);
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static InputManager _instance;

    public static InputManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
