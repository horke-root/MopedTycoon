using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerUI : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform content;
    public Button equipButton;
    public Button unequipButton;
    public Text globalTitle;
    public Slider durabilityBar;
    public Text itemCostText;

    public GameObject infoPanel;


    private ItemInstance selectedItem;
    

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
        globalTitle.text = "";
        durabilityBar.value = 0;
        itemCostText.text = "0";
        infoPanel.SetActive(false);
        var data = new OfflineSaveSystem().Load();
        foreach (ItemInstance item in data.ownedItems)
        {
            GameObject itemObj = Instantiate(itemPrefab, content);
            InventoryButton button = itemObj.GetComponent<InventoryButton>();
            button.Init(item, this);
            inventoryItems.Add(itemObj);
        }

        equipButton.onClick.AddListener(OnEquip);
        unequipButton.onClick.AddListener(OnUnequip);
    }
    
    public void Select(InventoryButton button)
    {
        selectedItem = button.cItem;

        foreach (var b in inventoryItems) // Unselect all buttons on panel
        {
            b.GetComponent<InventoryButton>().Deselect();
        }




        globalTitle.text = selectedItem.itemId;
        durabilityBar.value = selectedItem.durability / 100f;
        itemCostText.text = selectedItem.GetEstCost().ToString();
        infoPanel.SetActive(true);
        button.isSelected = true;
    }
    public void OnEquip()
    {
        var data = new OfflineSaveSystem().Load();
        data.EquipItem(selectedItem, GameService.Instance.bikeVisual);
        new OfflineSaveSystem().Save(data);
    }

    public void OnUnequip()
    {
        var data = new OfflineSaveSystem().Load();
        data.UnequipItem(selectedItem, GameService.Instance.bikeVisual);
        new OfflineSaveSystem().Save(data);
    }

}
