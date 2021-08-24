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
    public bool seedStorage; //holds infinite seeds
    public bool harvestStorage; //holds harvested crops

    private int waterLevel; //for watering bucket, goes up to 10, each plant watered takes 1
    
    //only apply if harvestStorage == true
    private int plantAmount; //how many harvested crops are being held
    //only applies if harvestCrop == true or harvestStorage == true
    private Plant plantType; //for selling plants 1 at a time or with collecting bin
}
