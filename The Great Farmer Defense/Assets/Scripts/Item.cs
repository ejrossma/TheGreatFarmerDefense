using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public int cost;
    public int sellPrice;
    public Sprite image;
    public bool canAttack; //farm tools
    public bool canBePlanted; //seeds
    public bool canBeSold; //harvested crops
    public bool canBePurchased;
    public bool canKillCrop; //farm tools
    public bool canWater; //watering bucket
    public bool harvestCrop; //harvested crop
    public Plant plantType; //for knowing which seeds are which plant type

    //for barrels
    public bool storage;
    public Item seedType;
    public bool pickupable;
}
