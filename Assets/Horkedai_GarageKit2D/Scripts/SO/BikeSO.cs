using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GarageKit2D/Bike Preset")]
public class BikeSO : ScriptableObject
{
    public string bikeName;
    public string bikePrefabPath;
    public List<BikeStat> bikeStats;
}
