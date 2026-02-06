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
    public Text itemMassText;
    public Text estBikeCost;
    public Text BikeMass;
    public PartRepair partRepairUI;
    GameService gs;
    

    public GameObject infoPanel;


    private ItemInstance selectedItem;
    

    private List<GameObject> inventoryItems;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gs = GameService.Instance;
        Init();
        partRepairUI.Init(this);
        
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
        itemMassText.text = "0";
        infoPanel.SetActive(false);
        gs.LoadAll();
        partRepairUI.UpdateUI();
        foreach (ItemInstance item in gs.playerData.ownedItems)
        {
            GameObject itemObj = Instantiate(itemPrefab, content);
            InventoryButton button = itemObj.GetComponent<InventoryButton>();
            button.Init(item, this);
            inventoryItems.Add(itemObj);
        }
        ReloadBikeInfo();

        equipButton.onClick.AddListener(OnEquip);
        unequipButton.onClick.AddListener(OnUnequip);

    }

    private void ReloadBikeInfo()
    {
            var bike = gs.playerData.GetCurrentBike(gs);
            int estCost = bike.GetEstCost(gs.playerData);
            float mass = bike.GetCurrentMass(gs.playerData);
            estBikeCost.text = estCost.ToString();
            BikeMass.text = mass.ToString();
    }
    
    public void Select(InventoryButton button)
    {
        selectedItem = button.cItem;

        foreach (var b in inventoryItems) // Unselect all buttons on panel
        {
            b.GetComponent<InventoryButton>().Deselect();
        }

        partRepairUI.ChooseItem(gs.playerData.FindItem(selectedItem));


        globalTitle.text = selectedItem.itemId;
        durabilityBar.value = selectedItem.durability / 100f;
        itemCostText.text = selectedItem.GetEstCost().ToString();
        itemMassText.text = selectedItem.mass.ToString();
        infoPanel.SetActive(true);
        button.isSelected = true;

        ReloadBikeInfo();
    }
    public void OnEquip()
    {
        
        gs.playerData.EquipItem(selectedItem, GameService.Instance.bikeVisual, gs);
        //gs.SaveAll();
        ReloadBikeInfo();
    }

    public void OnUnequip()
    {

        gs.playerData.UnequipItem(selectedItem, GameService.Instance.bikeVisual, gs);
        //gs.SaveAll();
        ReloadBikeInfo();
    }

    public void OnRepair()
    {

        partRepairUI.Repair();
        
    }

}
