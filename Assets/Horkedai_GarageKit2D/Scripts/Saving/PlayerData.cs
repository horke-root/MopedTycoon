using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int money;
    public int level;
    public List<ItemInstance> ownedItems;

    public List<ItemInstance> GetItems(TuningCatalogSO catalog)
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
    }
}
