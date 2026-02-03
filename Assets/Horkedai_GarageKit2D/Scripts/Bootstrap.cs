using UnityEngine;


public class Bootstrap : MonoBehaviour
{
    public TuningCatalogSO catalogSO;
    public DefaultLoadoutSO defaultLoadoutSO;
    private OfflineSaveSystem _save;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _save = new OfflineSaveSystem();
        var data = _save.Load();
        
        if (catalogSO == null) // load from GameServices if catalog not setted, optimising for future online
        {
            catalogSO =  GameService.Instance.catalog;
        }

        data.AddBike(new BikeData(defaultLoadoutSO.defaultBikeSO));
        GameService.Instance.bikeVisual.Clear();

        if (!data.defaultSetted)
        {
            foreach (var item in defaultLoadoutSO.slots)
            {
                ItemInstance newItem = new ItemInstance(catalogSO.Get(item.itemId));
                newItem.durability = Random.Range(50, 80);
                data.AddItem(newItem);
                // NEED FIX: change these items to equipped items
               
            }
            data.defaultSetted = true;
            _save.Save(data);
        }

        Debug.Log("Player data loaded: " + data.ownedItems.Count + " items owned.");
        
        foreach (var item in data.GetEquippedItems(data.GetCurrentBike()))
        {
            GameService.Instance.bikeVisual.EquipItem(item);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
