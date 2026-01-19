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

    public PlayerData(int money = 100, int level = 1)
    {
        this.money = money;
        this.level = level;
        ownedItems = new List<ItemInstance>();
    }

    public void Add(ItemInstance item)
    {
        ownedItems.Add(item);   
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
