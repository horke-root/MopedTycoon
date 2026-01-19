using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class OfflineSaveSystem : ISaveRepository
{
    private readonly string filePath;
    public OfflineSaveSystem(string fileName = "playerdata.json")
    {
        this.filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
    }

    //New implementation using file system
    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log(json);
    }

    public PlayerData Load()
    {
        if (!File.Exists(filePath))
                return new PlayerData(); // return default data if no save file exists
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }


    //Old implementation using PlayerPrefs
    
    /*public void Save<PlayerData>(string key, PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public PlayerData Load<PlayerData>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return default(PlayerData);
    }*/
}