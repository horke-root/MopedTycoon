using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int money;
    public int level;
    public List<ItemInstance> ownedItems;
    
    public bool defaultSetted = false;

    public int currentBike; // number in counts of items in bikes List [like 1 or 0 or mayeb 13]

    public List<BikeData> bikes;

    public PlayerData(int money = 100, int level = 1)
    {
        this.money = money;
        this.level = level;
        ownedItems = new List<ItemInstance>();
        currentBike = 0;
        bikes = new List<BikeData>();
    }

    public void Add(ItemInstance item)
    {
        ownedItems.Add(item);   
    
    }

    public BikeData GetCurrentBike()
    {
        return bikes[currentBike];
    }


    /*public List<ItemInstance> GetItems(TuningCatalogSO catalog)
    {
        var list = new List<ItemInstance>();
        if (ownedItemIds != null)
        {
            foreach (var id in ownedItemIds)
            {
                list.Add(catalog.Get(id) != null ? new ItemInstance(catalog.Get(id)) : null);
            }
        }
        return list;
    }*/
}
