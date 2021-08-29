using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakdownManager : MonoBehaviour
{
    public GameObject cropsSoldAmount;
    public GameObject moneyEarnedAmount;
    public GameObject billsAmount;
    public GameObject netGainLossAmount;

    public Color gain;
    public Color loss;

    public void setValues() {
        cropsSoldAmount.GetComponent<Text>().text = "" + GameManager.cropsSoldToday;
        moneyEarnedAmount.GetComponent<Text>().text = "" + GameManager.moneyEarnedToday;
        float temp;
        if (GameManager.currentDay < 4) {
            temp = Random.Range(2.0f, 3.0f);
        } else {
            temp = Random.Range(3.0f, (float) GameManager.currentDay);
        }
        temp *= 100;
        temp = Mathf.Floor(temp);
        billsAmount.GetComponent<Text>().text = "" + temp;
        Player.money -= (int) temp;

        if ((int)temp < GameManager.moneyEarnedToday) {
            int temp2 = GameManager.moneyEarnedToday - (int)temp;
            netGainLossAmount.GetComponent<Text>().color = gain;
            netGainLossAmount.GetComponent<Text>().text = "" + temp2;
        } else {
            int temp2 = (int)temp - GameManager.moneyEarnedToday;
            netGainLossAmount.GetComponent<Text>().color = loss;
            netGainLossAmount.GetComponent<Text>().text = "" + temp2;
        }
    }
}
