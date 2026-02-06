using TMPro;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Instance;
    public TextMeshProUGUI moneyText;
    public OfflineSaveSystem _playerSave;
    public OfflineSaveSystem _bikesSave;
    public PlayerData playerData;
    public BikesData bikesData;

    //Code:
    public TuningCatalogSO catalog;
    public BikeVisual bikeVisual;

    void Awake()
    {
        _playerSave = new OfflineSaveSystem();
        _bikesSave = new OfflineSaveSystem("bikesdata.json");
        
        playerData = _playerSave.Load<PlayerData>();
        bikesData = _bikesSave.Load<BikesData>();

        UpdateMoney();

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
        LoadBike();
        bikesData.SaveBike(bike);
        SaveBike();
    }


    public void SavePlayer()
    {
        _playerSave.Save(playerData);
        Debug.LogWarning("GameService: Saved");
    }
    public void LoadPlayer()
    {
        //SavePlayer();  //for safe load, dont forget old settings
        playerData = _playerSave.Load<PlayerData>();
        Debug.LogWarning("GameService: Loaded");
    }

    public void SaveBike()
    {
        Debug.LogWarning("GameService: Saved");
        _bikesSave.Save(bikesData);
    }
    public void LoadBike()
    {
        //SaveBike(); //for safe load, dont forget old settings
        bikesData = _bikesSave.Load<BikesData>();
        Debug.LogWarning("GameService: Loaded");
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

    /// <summary>
    /// Знімає гроші з балансу безопасно і обновляючи інтерфейс
    /// </summary>
    /// <param name="cost">Скільки знять</param>
    /// <returns>true = succes, false = unsucces</returns>

    public bool Withdraw(int cost) 
    {
        if (playerData.money < cost && playerData.money > 0)
        {
            return false;
        }
        playerData.money = playerData.money - cost;
        UpdateMoney();
        return true;
    }

    /// <summary>
    /// Додоає гроші в баланс безопасно і обновляючи інтерфейс
    /// </summary>
    /// <param name="cost">Скільки добавити</param>

    public void Deposit(int cost)
    {
        playerData.money += cost;
        SavePlayer();
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = playerData.money.ToString();
    }




    
}
