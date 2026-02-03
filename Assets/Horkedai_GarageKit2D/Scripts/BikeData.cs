using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BikeStat // Unity JSON dosnt support Dicts
{
    public string slotId;
    public float power;
}


[Serializable]
public class BikeData
{
    public string bikeId;
    public string bikeName;
    public string bikePrefabPath;
    public List<BikeStat> bikeStats;

    public List<string> equippedItems = new List<string>();

    public BikeData(BikeSO bikeSO)
    {
        bikeId = System.Guid.NewGuid().ToString();
        bikeName = bikeSO.bikeName;
        bikePrefabPath = bikeSO.bikePrefabPath;
        bikeStats = bikeSO.bikeStats;
    }

    public void Equip(ItemInstance item)
    {
        equippedItems.Add(item.instanceId);
        item._IsEquipped = true;
    }

    public void Unequip(ItemInstance item)
    {
        item._IsEquipped = false;
        equippedItems.Remove(item.instanceId);
    }
}