using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACraft : MonoBehaviour
{
    public int iDToGather;
    private PlayerInventory.Item _infos;
    public List<Needed> needed;
    private List<PlayerInventory.Item> _neededInfos;

    private PlayerInventory _playerInventory;

    public GameObject ObjectImagePrefab;

    private Button _button;

    [System.Serializable]
    public struct Needed
    {
        public int ID;
        public int Quantity;
    }

    private void Start()
    {
        _playerInventory = PlayerManager.GetInstance().inventory;

        _button = GetComponent<Button>();

        _neededInfos = new List<PlayerInventory.Item>();
        _infos = DataItems.GetInstance().GatherInfos(iDToGather);

        for(int i = 0; i < needed.Count; i++)
        {
            PlayerInventory.Item item = DataItems.GetInstance().GatherInfos(needed[i].ID);
            item.Quantity = needed[i].Quantity;
            _neededInfos.Add(item);
            GameObject go = Instantiate(ObjectImagePrefab);
            go.transform.parent = transform;
            go.GetComponent<Image>().sprite = item.Texture;
            go.GetComponentInChildren<Text>().text = item.Quantity.ToString();
            go.GetComponent<RectTransform>().localPosition = new Vector3(70 + i * 50, 0, 0);
        }
    }

    private void Update()
    {
        bool canCraft = true;
        for (int i = 0; i < _neededInfos.Count; i++)
        {
            if (_playerInventory.GetQuantityByID(_neededInfos[i].Id) < _neededInfos[i].Quantity)
            {
                canCraft = false;
                break;
            }
        }

        if (canCraft)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }

    public void TryToCraft()
    {
        bool canCraft = true;
        for (int i = 0; i < _neededInfos.Count; i++)
        {
            if(_playerInventory.GetQuantityByID(_neededInfos[i].Id) < _neededInfos[i].Quantity)
            {
                canCraft = false;
                break;
            }
        }

        if(canCraft)
        {
            _playerInventory.AddItemWithObject(_infos);
            for (int i = 0; i < _neededInfos.Count; i++)
            {
                _playerInventory.RemoveItemWithID(_neededInfos[i].Id, _neededInfos[i].Quantity);
            }
        }
    }
}
