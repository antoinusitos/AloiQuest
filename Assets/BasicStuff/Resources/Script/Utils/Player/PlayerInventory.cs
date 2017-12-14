using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _isInInventory = false;

    public GameObject inventoryUI;
    public GameObject mapUI;
    public GameObject craftUI;

    private InputManager _inputManager;
    private CameraManager _cameraManager;
    private UIManager _uiManager;

    public List<Item> inventory;
    private int _maximumQuantity = 5;

    [System.Serializable]
    public struct Item
    {
        public int Id;
        public string Name;
        public int Quantity;
        public Sprite Texture;
        public Item(Sprite theTexture, int theId = -1, string theName = "UNKNOWN", int theQuantity = 0)
        {
            Id = theId;
            Name = theName;
            Quantity = theQuantity;
            Texture = theTexture;
        }
    }

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _cameraManager = CameraManager.GetInstance();
        _uiManager = UIManager.GetInstance();
        OpenCloseinventoryUI();

        inventory = new List<Item>();
    }

    private void Update()
    {
        if(_inputManager.GetMenuPressed())
        {
            _isInInventory = !_isInInventory;
            OpenCloseinventoryUI();
        }
    }

    private void OpenCloseinventoryUI()
    {
        inventoryUI.SetActive(_isInInventory);
        mapUI.SetActive(_isInInventory);
        craftUI.SetActive(_isInInventory);
        if (_isInInventory)
        {
            _cameraManager.SetCursorLocked(CursorLockMode.None);
        }
        else
        {
            _cameraManager.SetCursorLocked(CursorLockMode.Locked);
        }
    }

    private int GetNumberFromID(int theID)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Id == theID)
            {
                return inventory[i].Quantity;
            }
        }
        return 0;
    }

    private int GetItemIndexFromID(int theID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Id == theID)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddItemWithObject(Item theObject)
    {
        int index = GetItemIndexFromID(theObject.Id);
        if (index == -1)
        {
            inventory.Add(theObject);
            _uiManager.AddImageToList(theObject.Texture);
            index = GetItemIndexFromID(theObject.Id);
            _uiManager.RefreshQuantities(index, theObject.Quantity);
        }
        else
        {
            AddItemWithID(theObject.Id, theObject.Quantity);
        }
    }

    public void AddItemWithID(int theID, int theQuantity)
    {
        if (GetNumberFromID(theID) < _maximumQuantity)
        {
            int index = GetItemIndexFromID(theID);
            if (index > -1)
            {
                Item i = new Item(inventory[index].Texture, inventory[index].Id, inventory[index].Name, Mathf.Clamp(inventory[index].Quantity + theQuantity, 0, _maximumQuantity));
                inventory[index] = i;
                _uiManager.RefreshQuantities(index, i.Quantity);
            }
            else
                print("cannot add item, no corresponding Item");
        }
    }

    public void RemoveItemWithID(int theID, int theQuantity)
    {
        if (GetNumberFromID(theID) >= theQuantity)
        {
            int index = GetItemIndexFromID(theID);
            Item i = new Item(inventory[index].Texture, inventory[index].Id, inventory[index].Name, Mathf.Clamp(inventory[index].Quantity - theQuantity, 0, _maximumQuantity));
            inventory[index] = i;
            _uiManager.RefreshQuantities(index, i.Quantity);
            if (i.Quantity == 0)
            {
                _uiManager.RemoveImageFromList(index);
                inventory.RemoveAt(index);
            }
        }
        else
        {
            print("cannot remove item, not enough quantity");
        }
    }

    public int GetQuantityByID(int theID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Id == theID)
            {
                return inventory[i].Quantity;
            }
        }
        return 0;
    }
}
