using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float limitAngleVertical = 89;

    private InputManager _inputManager;
    private GamepadManager _gamepadManager;
    private CameraManager _cameraManager;

    private Transform _transform;
    private Transform _transformParent;

    private float _totalVertical = 0;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _gamepadManager = GamepadManager.GetInstance();
        _cameraManager = CameraManager.GetInstance();

        _transform = GetComponent<Transform>();
        _transformParent = _transform.parent.GetComponent<Transform>();

        _cameraManager.SetCursorLocked(CursorLockMode.Locked);
    }

    private void Update()
    {
        float horizontalMovement = _inputManager.GetHorizontalMouseMovement() * horizontalSpeed * Time.deltaTime;
        float verticalMovement = _inputManager.GetVerticalMouseMovement() * verticalSpeed * Time.deltaTime;

        if (_cameraManager.GetCurrentCursorMode() == CursorLockMode.Locked)
        {
            if (_totalVertical + verticalMovement < limitAngleVertical && _totalVertical + verticalMovement > -limitAngleVertical)
            {
                _totalVertical += verticalMovement;
                _transform.Rotate(Vector3.right, -verticalMovement);
            }
            _transformParent.Rotate(Vector3.up, horizontalMovement);
        }
    }

}
