using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int money;
    public int level;
    public float humanMass=60f;
    public List<ItemInstance> ownedItems;
    
    public bool defaultSetted = false;

    public int currentBike; // number in counts of items in bikes List [like 1 or 0 or mayeb 13]

    public List<string> bikes;
    public PlayerData()
    {
        this.money = 100;
        this.level = 1;
        ownedItems = new List<ItemInstance>();
        currentBike = 0;
        bikes = new List<String>();
    }
    public PlayerData(int money = 100, int level = 1)
    {
        this.money = money;
        this.level = level;
        ownedItems = new List<ItemInstance>();
        currentBike = 0;
        bikes = new List<String>();
    }

    public void AddItem(ItemInstance item)
    {
        //new OfflineSaveSystem("bikesdata.json").Load<BikesData>();
        ownedItems.Add(item);   
    
    }
    public void AddBike(BikeData bike)
    {
        var bikesData = new OfflineSaveSystem("bikesdata.json").Load<BikesData>();
        bikesData.bikes.Add(bike);
        new OfflineSaveSystem("bikesdata.json").Save(bikesData);
    }

    public BikeData GetCurrentBike(GameService gs)
    {
        Debug.Log("Getting bike at index: " + currentBike + " with name: " + gs.bikesData.bikes[currentBike].bikeName + " from bikesData with total bikes: " + gs.bikesData.bikes.Count);
        return gs.bikesData.bikes[currentBike];
    }

    public void EquipItem(ItemInstance item,  BikeVisual visual, GameService gs, BikeData bike = null)
    {
        if (bike == null)
        {
            bike = GetCurrentBike(gs);
        }
        ItemInstance findItem = ownedItems.Find(i => i.instanceId == item.instanceId);

        bike.Equip(findItem);
        visual.EquipItem(findItem);
    }
    public void UnequipItem(ItemInstance item, BikeVisual visual, GameService gs, BikeData bike = null)
    {
        if (bike == null)
        {
            bike = GetCurrentBike(gs);
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

    public void StartRepairItem(ItemInstance item, long ticks)
    {
        ItemInstance findItem = FindItem(item);
        if (findItem != null && findItem.durability < 100)
        {
            findItem.isRepairing = true;
            findItem.repairStarts = ticks;
        }
    }

    public ItemInstance FindItem(ItemInstance item)
    {
        return ownedItems.Find(i => i.instanceId == item.instanceId);
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
