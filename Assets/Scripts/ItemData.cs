using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Data")]
    public string name;
    public string description;
    public Sprite visual;
    public GameObject prefab;
    public bool stackable;
    public int maxStack;

    [Header("Effects")]
    public float healthEffect;
    public float hungerEffect;
    public float thirstEffect;


    [Header("Armor Stats")]
    [SerializeField]
    public float armorPoints;

    [Header("Attack Stats")]
    [SerializeField]
    public float attackPoints;

    [Header("Types")]
    public ItemType itemType;
    public EquipmentType equipmentType;
}

public enum ItemType
{
    Resource, 
    Equipment, 
    Consumable
}

public enum EquipmentType
{
    Head, 
    Chest, 
    Hands, 
    Legs, 
    Feet, 
    Weapon
}