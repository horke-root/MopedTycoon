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
        if (!data.defaultSetted)
        {
            foreach (var item in defaultLoadoutSO.slots)
            {
                data.Add(new ItemInstance(catalogSO.Get(item)));
            }
            data.defaultSetted = true;
            _save.Save(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
