using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BikeStat // Unity JSON dosnt support Dicts
{
    public string slotId;
    public float power;
}


[Serializable]
public class BikeData
{
    public string bikeId;
    public string bikeName;
    public string bikePrefabPath;
    public List<BikeStat> bikeStats;
    

    public BikeData(BikeSO bikeSO)
    {
        bikeId = System.Guid.NewGuid().ToString();
        bikeName = bikeSO.bikeName;
        bikePrefabPath = bikeSO.bikePrefabPath;
        bikeStats = bikeSO.bikeStats;
    }


}