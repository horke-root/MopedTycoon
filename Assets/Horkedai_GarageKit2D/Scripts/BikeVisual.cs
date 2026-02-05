using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SlotField
{
    public string slotId;
    public SpriteRenderer sprite;
}

public class BikeVisual : MonoBehaviour
{   
    public List<SlotField> slotF = new List<SlotField>();
    public TuningCatalogSO catalogSO;

    private float humanMass = 60f;
    

    private Dictionary<string, SpriteRenderer> slotRenderers = new Dictionary<string, SpriteRenderer>();

    public void Reload()
    {
        foreach (SlotField f in slotF)
        {
            slotRenderers.Add(f.slotId, f.sprite);
            Debug.Log(f.slotId + "In a dict");
        }
    }

    void Awake()
    {
        Reload();
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
        SpriteRenderer sp = FindSlotRenderer(item);
        var rbI = sp.GetComponent<Rigidbody2D>();
        if (item.slotId == "frame") 
        {
            rbI.mass = humanMass + item.mass;
        } else
        {
            rbI.mass = item.mass;
        }
        Debug.Log(GameService.Instance.catalog.Get(item.itemId).itemId + " = cat");
        sp.sprite = GameService.Instance.catalog.Get(item.itemId).itemSprite;
    }

    public void UnequipItem(ItemInstance item)
    {
        SpriteRenderer sp = FindSlotRenderer(item);
        var rbI = sp.GetComponent<Rigidbody2D>();

        if (item.slotId != "frame") //if all rbs be = 0 bike will fly away
        {
            rbI.mass = humanMass;
        }
        
        sp.sprite = null;
    }

    private SpriteRenderer FindSlotRenderer(ItemInstance item)
    {
        humanMass = new OfflineSaveSystem().Load().humanMass;
        string itemSlot = item.slotId;
        SpriteRenderer sp = new SpriteRenderer();
        
        //SpriteRenderer sp = slotRenderers[itemSlot];
        foreach (var s in slotRenderers)
        {
            Debug.Log(s);
            if (s.Key == itemSlot)
            {
                sp = s.Value;
                Debug.Log(item.itemId + " find for equip");
                break;

            }
        }
        return sp;
    }
}
