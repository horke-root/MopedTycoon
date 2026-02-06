using System;
using UnityEngine;
using UnityEngine.UI;

public class PartRepair : MonoBehaviour
{
    public float repairDuration = 5f; // Duration in minutes
    public int repairCost = 10; 
    
    public Text timer;
    public Button repairButton;

    private ItemInstance currentItem;
    private InventoryManagerUI inventoryManagerUI;
    private GameService gs;

    public void Init(InventoryManagerUI inventoryManagerUI)
    {
        this.inventoryManagerUI = inventoryManagerUI;
    }

    public void ChooseItem(ItemInstance item)
    {
        currentItem = item;
        UpdateUI();
        //inventoryManagerUI.ReloadBikeUnfo();
    }
    public void UpdateUI()
    {
        if (currentItem == null)
        {
            timer.text = "";
            repairButton.interactable = false;
            return;
        }

        if (currentItem.durability >= 100)
        {
            timer.text = "";
            repairButton.interactable = false;
            return;
        }

        if (currentItem.isRepairing)
        {
            timer.text = "";
            repairButton.interactable = false;
        }
        else if (currentItem.durability < 100)
        {
            timer.text = "Need repair";
            repairButton.interactable = true;
        }


        
    }

    public void Repair()
    {
        /*
        if (currentItem == null || currentItem.isRepairing)
            return;

        if (currentItem.durability >= 100)
            return; // No need to repair

        currentItem.isRepairing = true;
        currentItem.repairStarts = NTPTime.Instance.GetCurrentTime().Ticks; */
        if (!gs.Withdraw(repairCost))
        {
            return;
        }
        
        gs.playerData.StartRepairItem(currentItem, NTPTime.Instance.GetCurrentTime().Ticks);
        gs.SavePlayer();
        currentItem = gs.playerData.FindItem(currentItem);
        UpdateUI();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
        repairButton.onClick.AddListener(Repair);
        gs = GameService.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem != null && currentItem.isRepairing)
        {
            TimeSpan timePassed = GetTimePassed();
            double secondsLeft = (repairDuration * 60) - timePassed.TotalSeconds;
            if (secondsLeft <= 0)
            {
                RepairFinish();
                timer.text = "Repair Complete!";
                UpdateUI();
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds(secondsLeft);
                timer.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            }
        }
    }

    public void RepairFinish()
    {
        if (currentItem.isRepairing) {

            TimeSpan timePassed = GetTimePassed();
            
            var fItem = gs.playerData.FindItem(currentItem);

            fItem.isRepairing = false;
            fItem.repairStarts = 0;
            fItem.durability = 100;
            fItem.GetEstCost();
            

            gs.SavePlayer();

            UpdateUI();
            //inventoryManagerUI.Init();
        }
    }

    private TimeSpan GetTimePassed()
    {
        if (currentItem.repairStarts == 0) return TimeSpan.Zero;

        DateTime now = NTPTime.Instance.GetCurrentTime();
        DateTime start = new DateTime(currentItem.repairStarts);

        return now - start;
    }
}
