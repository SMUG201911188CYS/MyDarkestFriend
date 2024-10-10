using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ItemSlot
{
    public Item item;
    public byte count;
    public ItemHandler itemHandler;
}

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private GameObject InventoryGroup;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Sprite icon;
    public ItemManager itemManager;
    
    public List<ItemSlot> itemSlots;

    public static InventoryHandler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemSlots = new List<ItemSlot>();
        InventoryGroup.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInventory();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Random rand = new Random();
            AddItem(itemManager.item_list[rand.Next(0, 8)]);
        }
    }

    public void OpenInventory()
    {
        Main.I.canPause = false;
        InventoryGroup.SetActive(true);
    }

    public void ExitInventory()
    {
        Main.I.canPause = true;
        InventoryGroup.SetActive(false);
    }

    public void AddItem(Item item)
    {
        ItemSlot foundSlot = GetItemStack(item);
        if (foundSlot != null)
        {
            if (foundSlot.count == 0)
            {
                foundSlot.itemHandler.gameObject.SetActive(true);
            }
            if (item.canStack)
            {
                foundSlot.count++;
                foundSlot.itemHandler.count++;
                foundSlot.itemHandler.UpdateUI();
                return;
            }
        }
        
        AddSlot(item);
    }

    public void AddSlot(Item item)
    {
        ItemSlot newSlot = new ItemSlot();
        newSlot.item = item;
        newSlot.count = 1;
        var obj = Instantiate(itemPrefab, content);
        var itemHandler = obj.GetComponent<ItemHandler>();
        itemHandler.item = item;
        itemHandler.count = 1;
        newSlot.itemHandler = itemHandler;
        newSlot.itemHandler.UpdateUI();
        itemSlots.Add(newSlot);
    }

    public ItemSlot GetItemStack(Item item)
    {
        return itemSlots.Find(slot => slot.item == item);
    }

    public void UseItem(Item item, byte count)
    {
        ItemSlot foundSlot = GetItemStack(item);
        foundSlot.count -= count;
        foundSlot.itemHandler.UpdateUI();
        if (foundSlot.count == 0)
            foundSlot.itemHandler.gameObject.SetActive(false);
        TooltipSystem.Hide();
    }
}
