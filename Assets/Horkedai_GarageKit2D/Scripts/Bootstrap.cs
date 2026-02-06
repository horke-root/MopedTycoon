using System;
using UnityEngine;


public class Bootstrap : MonoBehaviour
{
    public TuningCatalogSO catalogSO;
    public GameObject LoadingScreen;
    public DefaultLoadoutSO defaultLoadoutSO;
    private OfflineSaveSystem _save;
    private GameService gs;
    private bool timeSynced = false;
    public int TimeLoadTicksNeeded = 360; // 5 seconds 5*60 fps
    private int tCount = 0;
    
    //private OfflineSaveSystem _bikes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        LoadingScreen.SetActive(true);

        /*

        _save = new OfflineSaveSystem();
        //_bikes = new OfflineSaveSystem("bikesdata.json");
        
        var data = _save.Load<PlayerData>();
        //var bikesData = _bikes.Load<BikesData>();
        
        if (catalogSO == null) // load from GameServices if catalog not setted, optimising for future online
        {
            catalogSO =  GameService.Instance.catalog;
        }
        */
        gs = GameService.Instance;
        gs.LoadAll();
        
        GameService.Instance.bikeVisual.Clear();

        if (!gs.playerData.defaultSetted)
        {
            gs.playerData.AddBike(new BikeData(defaultLoadoutSO.defaultBikeSO));
            foreach (var item in defaultLoadoutSO.slots)
            {
                ItemInstance newItem = new ItemInstance(catalogSO.Get(item.itemId));
                newItem.durability = UnityEngine.Random.Range(50, 80);
                gs.playerData.AddItem(newItem);
                // NEED FIX: change these items to equipped items
               
            }
            gs.playerData.defaultSetted = true;
            gs.SavePlayer();
        }

        Debug.Log("Player data loaded: " + gs.playerData.ownedItems.Count + " items owned.");
        
        foreach (var item in gs.playerData.GetEquippedItems(gs.playerData.GetCurrentBike(gs)))
        {
            gs.bikeVisual.EquipItem(item);
        }
        //TestCurrentTime();

    }
    private void TestCurrentTime()
    {
        DateTime now = NTPTime.Instance.GetCurrentTime();
        Debug.Log("Current NTP Time: " + now.ToString());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (NTPTime.Instance.SessionStartTime != null)
        {
            if (!timeSynced)
            {
                Debug.Log("Game Loaded");
                LoadingScreen.SetActive(false);
                timeSynced = true;
            }
            
        } 
        /*else
        {
            if (tCount >= TimeLoadTicksNeeded )
        }*/
    }
}
