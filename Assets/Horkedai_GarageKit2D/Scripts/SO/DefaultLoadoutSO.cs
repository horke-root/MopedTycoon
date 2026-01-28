using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GarageKit2D/Default Loadout", fileName = "DefaultLoadout_")]
public class DefaultLoadoutSO : ScriptableObject
{
    [System.Serializable]
    public class DefaultSlot
    {
        public string slotId;  // "exhaust"
        public string itemId;  // "exhaust_01"
    }

    public List<DefaultSlot> slots = new List<DefaultSlot>();
    public BikeSO defaultBikeSO;
}