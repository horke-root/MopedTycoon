using System;
using UnityEngine;



[Serializable]
public class ItemInstance
{
    public string instanceId; // unique id for this instance
    public string itemId;     // refers to the item definition
    public string slotId;
    public float mass;

    public int durability; // current durability
    public bool _IsEquipped = false;

    //public string equippedTo; // the bike this item is equipped to
    public int cost; // purchase cost
    public int estCost; // estimated resale cost

    public int GetEstCost()
    {
        estCost = (int)(cost * ((float)durability / 100));
        return estCost;
    }

    public ItemInstance(TuningItemSO item, int newDurability = 100)
    {
        instanceId = System.Guid.NewGuid().ToString();
        itemId = item.itemId;
        slotId = item.slotId;
        durability = newDurability;
        cost = item.cost;
        mass = item.mass;
    }

    public void Reload() //function for reload all dynamic data
    {
        GetEstCost();
    }

    public bool IsEquipped(PlayerData data)
    {
        var items = data.GetEquippedItems(data.GetCurrentBike());
        foreach (var item in items)
        {
            if (item.instanceId == this.instanceId)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool isEquippedOnBike(BikeData bike)
    {
        return bike.equippedItems.Contains(this.instanceId);
    }

    /*public void Equip(string vehicleId)
    {
        equippedTo = vehicleId;
    }
    public void Unequip()
    {
        equippedTo = null;
    }*/
}
