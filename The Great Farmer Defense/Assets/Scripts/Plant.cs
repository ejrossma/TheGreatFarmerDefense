using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    public new string name;
    public Sprite[] growthImages;
    public int index; //0 is carroto, 1 is peanks, 2 is tomelone
}
