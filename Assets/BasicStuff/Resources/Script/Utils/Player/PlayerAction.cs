using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private InputManager _inputManager;
    private UIManager _uiManager;
    private PlayerInventory _playerInventory;
    private Transform _mainCam;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _uiManager = UIManager.GetInstance();
        _playerInventory = GetComponent<PlayerInventory>();
        _mainCam = Camera.main.transform;
        _uiManager.SetActionType(UIManager.ActionType.Pickup);
        _uiManager.SetActionName(_inputManager.action.ToString());
    }

    private void Update()
    {
        RaycastHit objectFront;
        if (Physics.Raycast(_mainCam.position, _mainCam.forward, out objectFront, 100))
        {
            AItem item = objectFront.collider.GetComponent<AItem>();
            if (item)
                _uiManager.SetObjectName(item.GetInfos().Name);
            else
                _uiManager.HideObjectName();
        }
        else
        {
            _uiManager.HideObjectName();
        }

        if (_inputManager.GetActionPressed())
        {
            if (objectFront.collider)
            {
                AItem item = objectFront.collider.GetComponent<AItem>();
                if(item)
                {
                    _playerInventory.AddItemWithObject(item.GetInfos());
                    Destroy(objectFront.collider.gameObject);
                }
            }
        }
    }

}