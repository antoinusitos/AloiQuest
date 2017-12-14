using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataItems : MonoBehaviour
{
    public List<PlayerInventory.Item> allItems;

    public PlayerInventory.Item GatherInfos(int theID)
    {
        for(int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].Id == theID)
            {
                return allItems[i];
            }
        }
        return new PlayerInventory.Item();
    }

    // -----------------------------------------------------------------------------------------

    //
    // Singleton Stuff
    // 

    private static DataItems _instance;

    public static DataItems GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
