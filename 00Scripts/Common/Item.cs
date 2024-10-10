using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    public Sprite icon;
    public string name;
    public string description;
    public Enums.ItemType itemType;
    public GameObject spawnObject;
    
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}