using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _deadZoneStick = 0;
    private GamepadManager _gamepadManager = null;

    private float _speed = 20.0f;
    private float _acceleration = 0;
    private float _accelerationSpeed = 20;
    private float _decelerationSpeed = 2;

    private float _jumpHigh = 7.0f;

    private Transform _transform = null;

    private Rigidbody2D _rigidBody = null;

    private Vector2 _velocity = Vector2.zero;

    private void Start()
    {
        _gamepadManager = GamepadManager.GetInstance();
        _deadZoneStick = _gamepadManager.GetStickDeadZone();
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        _velocity *= 0;

        if (_gamepadManager.GetStickPosX(0) > _deadZoneStick)
        {
            _acceleration += Time.deltaTime * _accelerationSpeed;
            if (_acceleration > _speed) _acceleration = _speed;

            _velocity += Vector2.right * _acceleration * Time.deltaTime;
            _rigidBody.velocity += _velocity;
        }
        else if (_gamepadManager.GetStickPosX(0) < -_deadZoneStick)
        {
            _acceleration += Time.deltaTime * _accelerationSpeed;
            if (_acceleration > _speed) _acceleration = _speed;
            _velocity -= Vector2.right * _acceleration * Time.deltaTime;
            _rigidBody.velocity += _velocity;
        }
        else
        {
            _acceleration = 0;
            _velocity = _rigidBody.velocity;

            _velocity.x = Mathf.Lerp(_velocity.x, 0, _decelerationSpeed * Time.deltaTime);

            _rigidBody.velocity = _velocity;
        }

        if (_gamepadManager.AButtonPressed(0))
        {
            _rigidBody.velocity += Vector2.up * _jumpHigh;
        }
    }
}
