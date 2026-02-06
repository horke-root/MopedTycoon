using TMPro;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Instance;
    public TextMeshPro moneyText;
    public OfflineSaveSystem _playerSave;
    public OfflineSaveSystem _bikesSave;
    public PlayerData playerData;
    public BikesData bikesData;
    void Awake()
    {
        _playerSave = new OfflineSaveSystem();
        _bikesSave = new OfflineSaveSystem("bikesdata.json");
        
        playerData = _playerSave.Load<PlayerData>();
        bikesData = _bikesSave.Load<BikesData>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance  = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EquipItem(ItemInstance item, BikeVisual visual, BikeData bike = null)
    {
        ItemInstance findItem = playerData.ownedItems.Find(i => i.instanceId == item.instanceId);
        bike.Equip(findItem);
        visual.EquipItem(findItem);
        var bikesData = new OfflineSaveSystem("bikesdata.json").Load<BikesData>();
        bikesData.SaveBike(bike);
        new OfflineSaveSystem("bikesdata.json").Save(bikesData);
    }

    public PlayerData LoadPlayerData()
    {
        playerData = _playerSave.Load<PlayerData>();
        return playerData;
    }
    public void SavePlayerData()
    {
        _playerSave.Save(playerData);
    }

    public void SavePlayer()
    {
        _playerSave.Save(playerData);
    }
    public void LoadPlayer()
    {
        playerData = _playerSave.Load<PlayerData>();
    }

    public void SaveBike()
    {
        _bikesSave.Save(bikesData);
    }
    public void LoadBike()
    {
        bikesData = _bikesSave.Load<BikesData>();
    }

    public void SaveAll()
    {
        SavePlayer();
        SaveBike();
    }

    public void LoadAll()
    {
        LoadPlayer();
        LoadBike();
    }


    //Code:
    public TuningCatalogSO catalog;
    public BikeVisual bikeVisual;
}
