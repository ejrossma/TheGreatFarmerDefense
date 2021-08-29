using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject cropsSoldTotal;
    public GameObject moneyEarnedTotal;
    public GameObject daysSurvivedTotal;

    // Update is called once per frame
    void Update()
    {
        cropsSoldTotal.GetComponent<Text>().text = "" + GameManager.cropsSold;
        moneyEarnedTotal.GetComponent<Text>().text = "" + GameManager.moneyEarned;
        daysSurvivedTotal.GetComponent<Text>().text = "" + GameManager.currentDay;
    }
}
