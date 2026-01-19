using UnityEngine;

public class ItemInstance
{
    public string instanceId; // unique id for this instance
    public string itemId;     // refers to the item definition
    public string slotId;

    public int durability; // current durability

    public string equippedTo; // vehicleId this item is equipped to, null if in inventory

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
        durability = newDurability;
        cost = item.cost;
    }

    public void Reload() //function for reload all dynamic data
    {
        GetEstCost();
    }

    public bool IsEquipped()
    {
        return !string.IsNullOrWhiteSpace(equippedTo);
    }
}
