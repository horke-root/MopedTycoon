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
        if (item.isEquippedOnBike(this) == false) 
        {
            equippedItems.Add(item.instanceId);
            item._IsEquipped = true;
        }
        
        
    }

    public void Unequip(ItemInstance item)
    {
        item._IsEquipped = false;
        equippedItems.Remove(item.instanceId);
    }

    public float GetCurrentMass(PlayerData playerData)
    {
        float mass = 0f;
        foreach (string itemId in equippedItems)
        {
            ItemInstance item = playerData.ownedItems.Find(i => i.instanceId == itemId);
            if (item == null) continue;
            if (item.slotId == "frame")
            {
                mass += playerData.humanMass - item.mass;
            } else
            {
                mass += item.mass;
            }
            
        }
        return mass;
    }

    public int GetEstCost(PlayerData playerData)
    {
        int cost = 0;
        foreach (var itemId in equippedItems)
        {
            ItemInstance item = playerData.ownedItems.Find(i => i.instanceId == itemId);
            if (item == null) continue;
            cost += item.GetEstCost();
        }
        return cost;
    }   
}