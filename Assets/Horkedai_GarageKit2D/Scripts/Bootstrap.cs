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

        if (!data.defaultSetted)
        {
            foreach (var item in defaultLoadoutSO.slots)
            {
                data.Add(new ItemInstance(catalogSO.Get(item.itemId)));
            }
            data.defaultSetted = true;
            _save.Save(data);
        }

        Debug.Log("Player data loaded: " + data.ownedItems.Count + " items owned.");

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
