using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    public Plant[] possiblePlants;
    public Sprite[] indicatorImages;

    public GameObject Indicator;
    
    private int status; //0 = wait, 1 = needs water, 2 = ready to harvest, 3 = dead/needs to be removed
    private int age; //0-5 growth cycle, 6 = dead

    void Update()
    {
        
    }
}
