using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BikesData
{
    public List<BikeData> bikes = new List<BikeData>();
    public int FindBikeIndexById(string bikeId)
    {
        for (int i = 0; i < bikes.Count; i++)
        {
            if (bikes[i].bikeId == bikeId)
            {
                return i;
            }
        }
        return -1; // Not found
    }
    public void SaveBike(BikeData bike)
    {
        int index = FindBikeIndexById(bike.bikeId);
        if (index != -1)
        {
            bikes[index] = bike; // Update existing bike
        }
        else
        {
            bikes.Add(bike); // Add new bike
        }
        new OfflineSaveSystem("bikesdata.json").Save(this);
    }
}
