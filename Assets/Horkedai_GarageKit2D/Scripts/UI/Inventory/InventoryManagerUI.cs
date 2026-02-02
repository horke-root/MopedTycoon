using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerUI : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform content;

    private List<GameObject> inventoryItems;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (inventoryItems != null)
        {
            foreach (GameObject obj in inventoryItems)
            {
                Destroy(obj);
            }
        }
        inventoryItems = new List<GameObject>();
        var data = new OfflineSaveSystem().Load();
        foreach (ItemInstance item in data.ownedItems)
        {
            GameObject itemObj = Instantiate(itemPrefab, content);
            InventoryButton button = itemObj.GetComponent<InventoryButton>();
            button.Init(item);
            inventoryItems.Add(itemObj);
        }
    }

}
