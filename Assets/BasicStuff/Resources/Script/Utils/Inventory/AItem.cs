using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItem : MonoBehaviour
{
    public int IDToFind = -1;
    public PlayerInventory.Item _infos;

    private void Start()
    {
        _infos = DataItems.GetInstance().GatherInfos(IDToFind);
    }

    public PlayerInventory.Item GetInfos()
    {
        return _infos;
    }
}
