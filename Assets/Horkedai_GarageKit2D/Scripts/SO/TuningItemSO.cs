using UnityEngine;


[CreateAssetMenu(fileName = "TuningItem_", menuName = "GarageKit2D/Tuning Item")]
public class TuningItemSO : ScriptableObject
{
    [Header("Identity")]
    public string itemId;
    public string displayName;

    public int cost;
    public float mass;

    [Header("Tuning Details")]
    public string slotId; // The slotId this item fits into
    public Sprite icon; // Optional icon for UI
    public Sprite itemSprite; // This will be applied on vechicle
}

