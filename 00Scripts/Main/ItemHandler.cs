using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : ObjectInteractionHandler
{
    [SerializeField] private TooltipTrigger tooltipTrigger;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCount;
    [SerializeField] private TMP_Text itemDescription;
    
    private TooltipHandler tooltipHandler;
    private Transform player;
    private Transform field;

    private bool isBellUsing = false;
    private bool isAlarmUsing = false;
    
    public Item item;
    public byte count;
    private void Awake()
    {
        Transform t = transform.Find("Interaction Icon Position");
        if (t)
            interactionIconPosition = t.position;
    }

    private void Start()
    {
        if(tooltipTrigger)
            tooltipTrigger.item = item;
        field = GameObject.Find("Field").transform;
    }

    public void UpdateUI()
    {
        if(!item) return;
        
        itemIcon.sprite = item.icon;
        itemName.text = item.name;
        itemDescription.text = item.description;
        itemCount.text = "X" + count;
    }

    public void UseItem()
    {
        Debug.Log("Clicked " + item.name);

        switch (item.itemType)
        {
            case Enums.ItemType.Alarm:
                if (ItemManager.I.currentMapType == CurrentMapType.Exploring && !isAlarmUsing)
                {
                    count--;
                    InventoryHandler.instance.UseItem(item, 1);
                    Vector3 pos = ItemManager.I.GetItemSpawnerLocation();
                    Instantiate(item.spawnObject, pos, Quaternion.identity, field);
                    isAlarmUsing = true;
                    WaitAlarm();
                }
                break;
            case Enums.ItemType.Book:
                if (ItemManager.I.currentMapType == CurrentMapType.Home)
                {
                    count--;
                    
                    var book = BookManager.I.bookList.Find(x => x.canRead == false);
                    
                    if(book != null)
                        BookManager.I.bookList.Find(x => x.canRead == false).canRead = true;
                    
                    InventoryHandler.instance.UseItem(item, 1);
                }
                break;
            case Enums.ItemType.Bouquet:
                if (ItemManager.I.currentMapType == CurrentMapType.Home)
                {
                    count--;
                    InventoryHandler.instance.UseItem(item, 1);
                    StatManager.I.ChangePlayerStat(Enums.PlayerStat.Trust, 5);
                }
                break;
            case Enums.ItemType.Flower:
                if (count >= 5)
                {
                    StatManager.I.ChangePlayerStat(Enums.PlayerStat.Trust, 5f);
                    count -= 5;
                    InventoryHandler.instance.UseItem(item, 5);
                    InventoryHandler.instance.AddItem(InventoryHandler.instance.itemManager.item_list[2]);
                }
                break;
            case Enums.ItemType.Food:
                //식량
                break;
            case Enums.ItemType.JumpRope:
                if (ItemManager.I.currentMapType == CurrentMapType.Exploring)
                {
                    count--;
                    InventoryHandler.instance.UseItem(item, 1);
                    Vector3 pos = ItemManager.I.GetItemSpawnerLocation();
                    Instantiate(item.spawnObject, pos, Quaternion.identity, field);
                }
                break;
            case Enums.ItemType.Lock:
                if (ItemManager.I.currentMapType == CurrentMapType.Exploring && !Player.LockedDoor)
                {
                    count--;
                    InventoryHandler.instance.UseItem(item, 1);
                    Vector3 pos = ItemManager.I.GetItemSpawnerLocation();
                    Player.LockedDoor = true;
                    SoundManager.I.PlaySFX_Lock();
                    WaitUnlock();
                }
                break;
            case Enums.ItemType.SelfDefenceBell:
                if (ItemManager.I.currentMapType == CurrentMapType.Exploring && !isBellUsing)
                {
                    count--;
                    InventoryHandler.instance.UseItem(item, 1);
                    RingBell();
                    isBellUsing = true;
                    WaitBell();
                }
                break;
        }
    }

    private async void WaitAlarm()
    {
        await UniTask.Delay(9000);
        isAlarmUsing = false;
    }
    
    private async void WaitBell()
    {
        await UniTask.Delay(4000);
        isBellUsing = false;
    }

    private async void WaitUnlock()
    {
        await UniTask.Delay(10000);
        SoundManager.I.PlaySFX_LockOp();
        Player.LockedDoor = false;
    }
    
    private void RingBell()
    {
        SoundManager.I.PlaySFX_Buzzer();
        GameObject[] foundNPCs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in foundNPCs)
        {
            NpcStateHandler npcStateHandler = npc.GetComponent<NpcStateHandler>();
            if (npcStateHandler)
            {
                npcStateHandler.SetTarget(ItemManager.I.GetPlayerTransform());
            }
        }
    }

    public override void Interact()
    {
        if(Player.playerType != Enums.PlayerType.White) return;
        
        if(!item) return;
        
        SoundManager.I.PlaySFX_Pick();
        
        base.Interact();
        
        Debug.Log("Get " + item.name);

        InventoryHandler.instance.AddItem(item);
        
        gameObject.SetActive(false);
    }
}
