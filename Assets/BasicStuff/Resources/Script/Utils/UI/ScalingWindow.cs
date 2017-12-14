using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingWindow : MonoBehaviour
{
    private RectTransform _transform;
    private bool _selected = false;
    private Vector2 _offset = Vector2.zero;

    public RectTransform windowAttached;
    public bool isWidth = false;
    private float tempWidth;
    private float tempHeight;
    private Vector2 tempPos;

    private float _baseHeight;
    private float _baseWidth;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _baseHeight = _transform.rect.height;
        _baseWidth = _transform.rect.width;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (
            mousePos.x <= _transform.position.x + _transform.rect.width / 2 &&
            mousePos.x >= _transform.position.x - _transform.rect.width / 2 &&
            mousePos.y <= _transform.position.y + _transform.rect.height / 2 &&
            mousePos.y >= _transform.position.y - _transform.rect.height / 2 &&
            Input.GetMouseButtonDown(0)
        )
        {
            _selected = true;
            _offset = mousePos;
            tempWidth = windowAttached.rect.width;
            tempHeight = windowAttached.rect.height;
            tempPos = Vector2.zero;
        }

        if (_selected && Input.GetMouseButton(0))
        {
            if(!isWidth)
                ScaleHeight((_offset.y - mousePos.y) + tempHeight);
            else
            {
                ScaleWidth(( mousePos.x - _offset.x) + tempWidth);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _selected = false;
        }

        if(isWidth)
        {
            _transform.sizeDelta = new Vector2(_baseWidth, windowAttached.rect.height);
        }
        else
        {
            _transform.sizeDelta = new Vector2(windowAttached.rect.width, _baseHeight);
        }
    }


    public void ScaleHeight(float size)
    {
        if(size >= 50)
            windowAttached.sizeDelta = new Vector2(windowAttached.rect.width, size);
    }

    public void ScaleWidth(float size)
    {
        if (size >= 50)
            windowAttached.sizeDelta = new Vector2(size, windowAttached.rect.height);
    }
}
