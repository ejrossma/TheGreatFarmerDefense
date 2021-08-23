using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    public new string name;
    public int cost;
    public int sellPrice;
    public Sprite[] growthImages;
    public Sprite harvestedImage;
}
