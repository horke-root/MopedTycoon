using System.Collections.Generic;

using UnityEngine;


public class BikeVisual : MonoBehaviour
{   
    public Dictionary<string, SpriteRenderer> slotRenderers = new Dictionary<string, SpriteRenderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Clear();
    } 

    public void Clear()
    {
        foreach (var sr in slotRenderers.Values) // Clear all slots
        {
            sr.sprite = null;
        }
    }

    // Update is called once per frame

    public void EquipItem(ItemInstance item)
    {
       string itemSlot = item.slotId;
       SpriteRenderer sp = slotRenderers[itemSlot];
       sp.sprite = GameService.Instance.catalog.Get(item.itemId).itemSprite;
    }
}
