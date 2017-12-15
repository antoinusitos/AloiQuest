using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager
{
    public Text objectName;
    public Text actionName;
    public GameObject[] textsToShow;

    public GameObject cursor;

    public Transform allItems;
    public GameObject objectImagePrefab;
    private int _indexInventory = 0;
    private int _maximumImage = 5;
    private float _margin = 10.0f;
    private RectTransform backGroundSize;

    public List<GameObject> _allImages;

    public enum ActionType
    {
        None,
        Pickup,
        Open,
        Close,
        //...
    }
    private ActionType _currentActionType = ActionType.None;

    private void Start()
    {
        _allImages = new List<GameObject>();
        if(allItems != null)
            backGroundSize = allItems.parent.GetComponent<RectTransform>();
        HideObjectName();
    }

    private void Update()
    {
        if (_allImages.Count == 0)
            _maximumImage = 5;
        else
        {
            _maximumImage = (int)Mathf.Floor(backGroundSize.rect.width / (_allImages[0].GetComponent<RectTransform>().rect.width + _margin));
        }

        for (int i = 0; i < _allImages.Count; i++)
        {
            Image img = _allImages[i].GetComponent<Image>();
            RectTransform rec = img.rectTransform;
            rec.localPosition = new Vector3(
                (i % _maximumImage) * rec.rect.width + rec.rect.width / 2 + _margin,
                -((i / _maximumImage) * rec.rect.height + rec.rect.height / 2 + _margin),
                0);
        }
    }

    public void SetObjectName(string name)
    {
        objectName.text = name;
        for(int i = 0; i < textsToShow.Length; i++)
        {
            textsToShow[i].SetActive(true);
        }
    }

    public void ShowCursor(bool newState)
    {
        cursor.SetActive(newState);
    }

    public void SetActionType(ActionType newActionType)
    {
        _currentActionType = newActionType;
    }

    public void SetActionName(string name)
    {
        actionName.text = name + " to " + _currentActionType.ToString();
    }

    public void HideObjectName()
    {
        for (int i = 0; i < textsToShow.Length; i++)
        {
            textsToShow[i].SetActive(false);
        }
    }

    public void AddImageToList(Sprite theImage)
    {
        GameObject go = Instantiate(objectImagePrefab);
        go.transform.SetParent(allItems);
        Image img = go.GetComponent<Image>();
        img.sprite = theImage;
        _allImages.Add(go);
        _indexInventory++;
    }

    public void RemoveImageFromList(int index)
    {
        Destroy(_allImages[index]);
        _allImages.RemoveAt(index);
    }

    public void RefreshQuantities(int index, int theQuantity)
    {
        _allImages[index].GetComponentInChildren<Text>().text = theQuantity.ToString();
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static UIManager _instance;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
