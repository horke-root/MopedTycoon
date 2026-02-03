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

    public void AddItem(ItemInstance item)
    {
        ownedItems.Add(item);   
    
    }
    public void AddBike(BikeData bike)
    {
        bikes.Add(bike);
    }

    public BikeData GetCurrentBike()
    {
        return bikes[currentBike];
    }

    public void EquipItem(ItemInstance item,  BikeVisual visual, BikeData bike = null)
    {
        if (bike == null)
        {
            bike = GetCurrentBike();
        }
        ItemInstance findItem = ownedItems.Find(i => i.instanceId == item.instanceId);

        bike.Equip(findItem);
        visual.EquipItem(findItem);
    }
    public void UnequipItem(ItemInstance item, BikeVisual visual, BikeData bike = null)
    {
        if (bike == null)
        {
            bike = GetCurrentBike();
        }
        ItemInstance findItem = ownedItems.Find(i => i.instanceId == item.instanceId);
        bike.Unequip(findItem);
        visual.UnequipItem(findItem);
    }

    public List<ItemInstance> GetEquippedItems(BikeData bike)
    {
        List<ItemInstance> equippedItems = new List<ItemInstance>();
        if (bike == null || bike.equippedItems == null)
        {
            return equippedItems;
        }

        foreach (var item in ownedItems)
        {
            foreach (var eid in bike.equippedItems)
            {
                if (item.instanceId == eid)
                {
                    equippedItems.Add(item);
                    break;
                }
            }
        }
        return equippedItems;
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
