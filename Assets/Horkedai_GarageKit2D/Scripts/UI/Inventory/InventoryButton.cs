using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour
{
    public Image bgImage;
    public Image iconImage;
    public Color equippedColor;
    public Color defaultColor = Color.white;
    public Color selectedColor = Color.gray;
    public Text durabilityText;

    private Button button;
    private int durability;
    public bool isSelected = false;
    public ItemInstance cItem;
    private InventoryManagerUI manager;



    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

    }

    public void Init(ItemInstance item, InventoryManagerUI mgr)
    {
        manager = mgr;
        TuningCatalogSO catalog = GameService.Instance.catalog;
        TuningItemSO tItem = catalog.Get(item.itemId);

        iconImage.sprite = tItem.icon;
        durability = item.durability;
        durabilityText.text = durability.ToString();
    
        cItem = item;
        bgImage.color = CurrentColor();
    }



    
    void OnButtonClick()
    {
        DateTime now = NTPTime.Instance.GetCurrentTime();
        Debug.Log("Current NTP Time: " + now.Ticks);


        isSelected = !isSelected;
        manager.Select(this);
        Color color;
        
        if (isSelected)
        {
            color = selectedColor;
        } else
        {
            color = CurrentColor();
        }
        bgImage.color = color;
    }

    public void Deselect()
    {
        isSelected = false;
        bgImage.color = CurrentColor();
    }

    public Color CurrentColor()
    {
        if (cItem.IsEquipped(new OfflineSaveSystem().Load<PlayerData>()))
        {
            return equippedColor;
        } 
        else {
            return defaultColor;
        }
    }
    
}
