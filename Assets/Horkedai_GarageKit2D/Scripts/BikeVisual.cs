using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class BikeVisual : MonoBehaviour
{   
    public Dictionary<string, SpriteRenderer> slotRenderers = new Dictionary<string, SpriteRenderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    } 

    // Update is called once per frame

    public void EquipItem(ItemInstance item)
    {
       
    }
}
