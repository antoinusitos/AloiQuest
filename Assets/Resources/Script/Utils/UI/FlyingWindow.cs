using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingWindow : MonoBehaviour
{
    private RectTransform _transform;
    private bool _selected = false;
    private Vector2 _offset = Vector2.zero;
    private float _baseHeight;

    public RectTransform _childTransform;
    private Vector2 _offsetToMove = Vector2.zero;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _baseHeight = _transform.rect.height;
        _offsetToMove = _childTransform.position - _transform.position;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (
            mousePos.x <= _transform.position.x + _transform.rect.width &&
            mousePos.x >= _transform.position.x &&
            mousePos.y <= _transform.position.y + _transform.rect.height / 2 &&
            mousePos.y >= _transform.position.y - _transform.rect.height / 2 &&
            Input.GetMouseButtonDown(0)
        )
        {
            _selected = true;
            _offset = new Vector2(_transform.position.x, _transform.position.y) - mousePos;
        }

        if(_selected && Input.GetMouseButton(0))
        {
            _transform.position = mousePos + _offset;
            _childTransform.position= (Vector2)_transform.position + _offsetToMove;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _selected = false;
        }

        _transform.sizeDelta = new Vector2(_childTransform.rect.width, _baseHeight);
    }

}
