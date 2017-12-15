using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private float _stickDeadZone = 0;

    private float _currentMoveTime = 0;
    private float _maxMoveTime = 0.3f;

    private float _lerpTime = 0;
    private float _lerpSpeed = 2.0f;

    private float _fadeTime = 1.0f;
    private float _timer = 0f;

    private int _currentIndex = 0;
    private int _maxIndex = 3;

    private int _baseSize = 0;
    private int _selectedSize = 0;

    private Color _baseColor = Color.white;
    private Color _selectedColor = Color.red;

    private bool _hasMoved = false;

    private bool _fadeOut = false;

    private GamepadManager _gamepadManager = null;

    public Text[] alltext;

    private void Start()
    {
        _gamepadManager = GamepadManager.GetInstance();
        _stickDeadZone = _gamepadManager.GetStickDeadZone();

        _baseColor = alltext[0].color;
        _baseSize = alltext[0].fontSize;
        _selectedSize = _baseSize + 20;

        SelectText(0);
    }

    private void SelectText(int index)
    {
        alltext[index].fontSize = _selectedSize;
    }

    private void UnSelectText(int index)
    {
        alltext[index].color = _baseColor;
        alltext[index].fontSize = _baseSize;
    }

    private void CheckGamepadMove()
    {
        if (_gamepadManager.GetStickPosY(0) < -_stickDeadZone)
        {
            _hasMoved = true;
            UnSelectText(_currentIndex);
            _currentIndex++;
            if (_currentIndex > _maxIndex)
                _currentIndex = 0;

            SelectText(_currentIndex);
        }
        else if (_gamepadManager.GetStickPosY(0) > _stickDeadZone)
        {
            _hasMoved = true;
            UnSelectText(_currentIndex);
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _maxIndex;

            SelectText(_currentIndex);
        }
    }

    private void Update()
    {
        _lerpTime = (Mathf.Sin(Time.time * _lerpSpeed) + 1) / 2;
        alltext[_currentIndex].color = Color.Lerp(_baseColor, _selectedColor, _lerpTime);

        if(_fadeOut) return;

        if (_gamepadManager.AButtonPressed(0))
        {
            ExecuteButton();
            return;
        }

        if (!_hasMoved)
        {
            CheckGamepadMove();
        }
        else
        {
            if (_gamepadManager.GetStickPosY(0) > -_stickDeadZone && _gamepadManager.GetStickPosY(0) < _stickDeadZone)
            {
                _currentMoveTime = 0;
                _hasMoved = false;
                return;
            }

            _currentMoveTime += Time.deltaTime;
            if(_currentMoveTime >= _maxMoveTime)
            {
                _currentMoveTime = 0;
                _hasMoved = false;
            }
        }
    }

    private void ExecuteButton()
    {
        if (_currentIndex == 2)
        {
            //Options
        }
        else
        {
            _fadeOut = true;
            CameraManager.GetInstance().FadeOut(_fadeTime, PostFade);
        }
    }

    public void PostFade()
    {
        if (_currentIndex == 0)
        {
            //New Game
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (_currentIndex == 1)
        {
            //Load Game
        }
        else if (_currentIndex == 3)
        {
            //Exit
            Application.Quit();
        }
    }
}
