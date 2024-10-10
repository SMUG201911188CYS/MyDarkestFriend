using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CurrentMapType
{
    Home,
    Exploring,
    None
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager I;
    
    [SerializeField] private GameObject inventoryGroup;
    [SerializeField] private List<ItemHandler> martAItemList;
    [SerializeField] private List<ItemHandler> martBItemList;
    [SerializeField] private List<ItemHandler> martCItemList;
    [SerializeField] private List<ItemHandler> martDItemList;
    [SerializeField] private List<ItemHandler> schoolAItemList;
    [SerializeField] private List<ItemHandler> schoolBItemList;
    [SerializeField] private List<ItemHandler> schoolCItemList;
    [SerializeField] private List<ItemHandler> schoolDItemList;
    [SerializeField] private Transform itemSpawnLocation_White;
    [SerializeField] private Transform itemSpawnLocation_Black;
    public List<Item> item_list;
    public CurrentMapType currentMapType = CurrentMapType.None;
    
    private void Awake()
    {
        if(!I)
            I = this;
    }

    private void Start()
    {
        inventoryGroup.SetActive(true);
    }
    
    public void InitItemObject()
    {
        foreach (var i in martAItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in martBItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in martCItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in martDItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in schoolAItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in schoolBItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in schoolCItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
        foreach (var i in schoolDItemList)
            if(i.gameObject) i.gameObject.SetActive(false);
    }

    public Vector2 GetItemSpawnerLocation()
    {
        if(Player.playerType == Enums.PlayerType.White)
            return itemSpawnLocation_White.position;
        else if(Player.playerType == Enums.PlayerType.Black)
            return itemSpawnLocation_Black.position;
        else
            return Vector2.zero;
    }
    
    public Transform GetPlayerTransform()
    {
        if(Player.playerType == Enums.PlayerType.White)
            return itemSpawnLocation_White.parent;
        else if(Player.playerType == Enums.PlayerType.Black)
            return itemSpawnLocation_Black.parent;
        else
            return null;
    }

    public void SetItemObject(Enums.MapType mapType)
    {
        int p_food = Random.Range(0, 10);
        int p_alarm = Random.Range(0, 100);
        int p_book = Random.Range(0, 10);
        int p_rope = Random.Range(0, 10);
        int p_flower = Random.Range(0, 10);
        int p_bell = Random.Range(0, 10);
        int p_lock = Random.Range(0, 10);
        int a, b;

        switch (mapType)
        {
            case Enums.MapType.Home:
            case Enums.MapType.Yard:
            case Enums.MapType.Explore:
                InitItemObject();
                break;
            case Enums.MapType.Mart_1:
                if (p_food is > 1 and <= 5)
                {
                    martAItemList[Random.Range(0,2)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    martAItemList[0].gameObject.SetActive(true);
                    martAItemList[1].gameObject.SetActive(true);
                }
                if(p_alarm < 20) martAItemList[2].gameObject.SetActive(true);
                if(p_flower < 1) martAItemList[3].gameObject.SetActive(true);
                break;
            case Enums.MapType.Mart_2:
                if (p_food is >= 4 and < 8)
                {
                    martBItemList[Random.Range(0,4)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    a = Random.Range(0, 4);
                    b = Random.Range(0, 4);
                    martBItemList[a].gameObject.SetActive(true);
                    while (a != b) b = Random.Range(0, 4);
                    martBItemList[b].gameObject.SetActive(true);
                }
                if(p_alarm < 20) martBItemList[4].gameObject.SetActive(true);
                break;
            case Enums.MapType.Mart_3:
                if (p_food is >= 4 and < 8)
                {
                    martCItemList[Random.Range(0,3)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    a = Random.Range(0, 3);
                    b = Random.Range(0, 3);
                    martCItemList[a].gameObject.SetActive(true);
                    while (a != b) b = Random.Range(0, 3);
                    martCItemList[b].gameObject.SetActive(true);
                }
                if(p_alarm < 20) martCItemList[3].gameObject.SetActive(true);
                if(p_book < 2) martCItemList[4].gameObject.SetActive(true);
                if(p_flower < 1) martCItemList[5].gameObject.SetActive(true);
                break;
            case Enums.MapType.Mart_4:
                if (p_food is >= 4 and < 8)
                {
                    martDItemList[Random.Range(0,3)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    a = Random.Range(0, 3);
                    b = Random.Range(0, 3);
                    martDItemList[a].gameObject.SetActive(true);
                    while (a != b) b = Random.Range(0, 3);
                    martDItemList[b].gameObject.SetActive(true);
                }
                if(p_alarm < 20) martDItemList[3].gameObject.SetActive(true);
                if(p_book < 2) martDItemList[4].gameObject.SetActive(true);
                if(p_flower < 1) martDItemList[5].gameObject.SetActive(true);
                break;
            case Enums.MapType.School_1:
                if (p_food is >= 4 and < 8)
                {
                    schoolAItemList[Random.Range(0,2)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    schoolAItemList[0].gameObject.SetActive(true);
                    schoolAItemList[1].gameObject.SetActive(true);
                }
                if(p_rope < 2) schoolAItemList[2].gameObject.SetActive(true);
                if(p_bell < 1) schoolAItemList[3].gameObject.SetActive(true);
                if(p_flower < 1) schoolAItemList[4].gameObject.SetActive(true);
                if(p_lock < 1) schoolAItemList[5].gameObject.SetActive(true);
                if(p_book < 2) schoolAItemList[6].gameObject.SetActive(true);
                break;
            case Enums.MapType.School_2:
                if (p_food is >= 4 and < 8)
                {
                    schoolBItemList[Random.Range(0,3)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    a = Random.Range(0, 3);
                    b = Random.Range(0, 3);
                    schoolBItemList[a].gameObject.SetActive(true);
                    while (a != b) b = Random.Range(0, 3);
                    schoolBItemList[b].gameObject.SetActive(true);
                }
                if(p_rope < 2) schoolBItemList[3].gameObject.SetActive(true);
                if(p_lock < 1) schoolBItemList[4].gameObject.SetActive(true);
                if(p_book < 2) schoolBItemList[5].gameObject.SetActive(true);
                if(p_flower < 1) schoolBItemList[6].gameObject.SetActive(true);
                if(p_alarm < 15) schoolBItemList[7].gameObject.SetActive(true);
                break;
            case Enums.MapType.School_3:
                if (p_food is >= 4 and < 8)
                {
                    schoolCItemList[Random.Range(0,2)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    schoolCItemList[0].gameObject.SetActive(true);
                    schoolCItemList[1].gameObject.SetActive(true);
                }
                if(p_book < 2) schoolCItemList[Random.Range(2, 4)].gameObject.SetActive(true);
                if(p_lock < 1) schoolCItemList[4].gameObject.SetActive(true);
                if(p_alarm < 15) schoolCItemList[5].gameObject.SetActive(true);
                if(p_rope < 2) schoolCItemList[6].gameObject.SetActive(true);
                if(p_bell < 1) schoolCItemList[7].gameObject.SetActive(true);
                if(p_flower < 1) schoolCItemList[8].gameObject.SetActive(true);
                break;
            case Enums.MapType.School_4:
                if (p_food is >= 4 and < 8)
                {
                    schoolDItemList[Random.Range(0,3)].gameObject.SetActive(true);
                }
                else if(p_food is >= 8 and < 10)
                {
                    a = Random.Range(0, 3);
                    b = Random.Range(0, 3);
                    schoolDItemList[a].gameObject.SetActive(true);
                    while (a != b) b = Random.Range(0, 3);
                    schoolDItemList[b].gameObject.SetActive(true);
                }
                if(p_book < 2) schoolCItemList[Random.Range(3, 5)].gameObject.SetActive(true);
                if(p_alarm < 15) schoolCItemList[5].gameObject.SetActive(true);
                if(p_rope < 2) schoolCItemList[6].gameObject.SetActive(true);
                if(p_bell < 1) schoolCItemList[7].gameObject.SetActive(true);
                if(p_lock < 1) schoolCItemList[8].gameObject.SetActive(true);
                if(p_flower < 1) schoolCItemList[9].gameObject.SetActive(true);
                break;
        }
    }
}
