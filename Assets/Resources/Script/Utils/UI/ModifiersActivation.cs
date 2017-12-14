using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiersActivation : MonoBehaviour 
{
    public List<GameObject> toActivate;

    private bool _activated = false;
    private RectTransform _transform;
    private float _offsetFriendly = 0.75f;


    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        for (int i = 0; i < toActivate.Count; i++)
        {
            toActivate[i].SetActive(false);
        }
    }

    public void Update()
    {

        Vector2 mousePos = Input.mousePosition;
        if (
            mousePos.x <= _transform.position.x + _transform.rect.width * _offsetFriendly + _transform.rect.width / 2 &&
            mousePos.x >= _transform.position.x - _transform.rect.width * _offsetFriendly + _transform.rect.width / 2 &&
            mousePos.y <= _transform.position.y + _transform.rect.height * _offsetFriendly - _transform.rect.height / 2 &&
            mousePos.y >= _transform.position.y - _transform.rect.height * _offsetFriendly - _transform.rect.height / 2
        )
        {
            if(!_activated)
            {
                _activated = true;
                for(int i = 0; i < toActivate.Count; i++)
                {
                    toActivate[i].SetActive(true);
                }
            }
        }
        else if (_activated)
        {
            _activated = false;
            for (int i = 0; i < toActivate.Count; i++)
            {
                toActivate[i].SetActive(false);
            }
        }
    }

}
