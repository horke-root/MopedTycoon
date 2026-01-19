using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "GarageKit2D/Tuning Catalog", fileName = "TuningCatalog_")]
public class TuningCatalogSO : ScriptableObject
{
    public List<TuningItemSO> items = new List<TuningItemSO>();

    private Dictionary<string, TuningItemSO> _byId;

    private void EnsureIndex()
    {
        if (_byId != null) return;

        _byId = items
            .Where(i => i != null && !string.IsNullOrWhiteSpace(i.itemId))
            .GroupBy(i => i.itemId)
            .ToDictionary(g => g.Key, g => g.First());
    }

    public TuningItemSO Get(string itemId)
    {
        EnsureIndex();
        return _byId.TryGetValue(itemId, out var it) ? it : null;
    }

    public List<string> GetAllSlotIds()
    {
        return items
            .Where(i => i != null && !string.IsNullOrWhiteSpace(i.slotId))
            .Select(i => i.slotId.Trim())
            .Distinct()
            .OrderBy(s => s)
            .ToList();
    }

    public List<TuningItemSO> GetItemsBySlot(string slotId)
    {
        slotId = (slotId ?? "").Trim();
        return items
            .Where(i => i != null && (i.slotId ?? "").Trim() == slotId)
            .OrderBy(i => i.displayName)
            .ToList();
    }
}
