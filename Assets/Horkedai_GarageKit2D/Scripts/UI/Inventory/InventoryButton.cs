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
    public Text durabilityText;

    private Button button;
     private int durability;
    private bool isEquipped;
    private OfflineSaveSystem save;
    private ItemInstance cItem;



    void Start()
    {
        save = new OfflineSaveSystem();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

    }

    public void Init(ItemInstance item)
    {
        TuningCatalogSO catalog = GameService.Instance.catalog;
        TuningItemSO tItem = catalog.Get(item.itemId);

        iconImage.sprite = tItem.icon;
        isEquipped = item.IsEquipped();
        durability = item.durability;
        durabilityText.text = durability.ToString();
        bgImage.color = isEquipped ? equippedColor : defaultColor;

        cItem = item;
    }



    
    void OnButtonClick()
    {
        var data = save.Load();

        if (cItem.IsEquipped())
        {
            data.UnequipItem(cItem, GameService.Instance.bikeVisual);
        }
        else
        {
            data.EquipItem(cItem, GameService.Instance.bikeVisual);
        }

        isEquipped = !isEquipped;
        bgImage.color = isEquipped ? equippedColor : defaultColor;
        save.Save(data);
    }

    
}
