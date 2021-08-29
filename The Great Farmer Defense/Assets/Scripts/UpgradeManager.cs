using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    public GameObject runningShoesText;
    public GameObject runningShoesMinus;
    public GameObject runningShoesPlus;

    public GameObject seedQualityText;
    public GameObject seedQualityMinus;
    public GameObject seedQualityPlus;

    public GameObject wateringCanText;
    public GameObject wateringCanMinus;
    public GameObject wateringCanPlus;

    public GameObject priceText;
    public GameObject moneyText;
    public GameObject confirmButton;

    private int price;

    private int runUpgrades;
    private int seedUpgrades;
    private int waterUpgrades;

    // Update is called once per frame
    void Update()
    {
        moneyText.GetComponent<Text>().text = "Money:" + Player.money;
        price = ((runUpgrades * 100) + (seedUpgrades * 250) + (waterUpgrades * 200));
        priceText.GetComponent<Text>().text = "Price:" + price;

        if (Player.money >= price)
            confirmButton.SetActive(true);
        else
            confirmButton.SetActive(false);

        //running shoes logic
        if (runUpgrades > 0 && Player.runningShoesLVL < 3) {
            runningShoesMinus.SetActive(true);
            runningShoesPlus.SetActive(true);
        } else if (runUpgrades > 0 && Player.runningShoesLVL >= 3) {
            runningShoesMinus.SetActive(true);
            runningShoesPlus.SetActive(false);
        } else if (Player.runningShoesLVL == 0 || (Player.runningShoesLVL > 0 && runUpgrades == 0)) {
            runningShoesMinus.SetActive(false);
            runningShoesPlus.SetActive(true);
        }

        //seed quality logic
        if (seedUpgrades > 0 && Player.seedQualityLVL < 3) {
            seedQualityMinus.SetActive(true);
            seedQualityPlus.SetActive(true);
        } else if (seedUpgrades > 0 && Player.seedQualityLVL >= 3) {
            seedQualityMinus.SetActive(true);
            seedQualityPlus.SetActive(false);
        } else if (Player.seedQualityLVL == 0 || (Player.seedQualityLVL > 0 && seedUpgrades == 0)) {
            seedQualityMinus.SetActive(false);
            seedQualityPlus.SetActive(true);
        }

        //watering can logic
        if (waterUpgrades > 0 && Player.wateringCanLVL < 3) {
            wateringCanMinus.SetActive(true);
            wateringCanPlus.SetActive(true);
        } else if (waterUpgrades > 0 && Player.wateringCanLVL >= 3) {
            wateringCanMinus.SetActive(true);
            wateringCanPlus.SetActive(false);
        } else if (Player.wateringCanLVL == 0 || (Player.wateringCanLVL > 0 && waterUpgrades == 0)) {
            wateringCanMinus.SetActive(false);
            wateringCanPlus.SetActive(true);
        }
    }


/////////////////////////////////////////////////////////////////////////
//HELPER FUNCTIONS
/////////////////////////////////////////////////////////////////////////
    public void runPlus() {
        if (Player.runningShoesLVL < 3) {
            Player.runningShoesLVL++;
            runUpgrades++;
            string temp = "" + Player.runningShoesLVL;
            runningShoesText.GetComponent<Text>().text = temp;            
        }
    }

    public void seedPlus() {
        if (Player.seedQualityLVL < 3) {
            Player.seedQualityLVL++;
            seedUpgrades++;
            string temp = "" + Player.seedQualityLVL;
            seedQualityText.GetComponent<Text>().text = temp;
        }
    }

    public void waterPlus() {
        if (Player.wateringCanLVL < 3) {
            Player.wateringCanLVL++;
            waterUpgrades++;
            string temp = "" + Player.wateringCanLVL;
            wateringCanText.GetComponent<Text>().text = temp;         
        }
       
    }

    public void runMinus() {
        Player.runningShoesLVL--;
        runUpgrades--;
        string temp = "" + Player.runningShoesLVL;
        runningShoesText.GetComponent<Text>().text = temp;
    }

    public void seedMinus() {
        Player.seedQualityLVL--;
        seedUpgrades--;
        string temp = "" + Player.seedQualityLVL;
        seedQualityText.GetComponent<Text>().text = temp;
    }

    public void waterMinus() {
        Player.wateringCanLVL--;
        waterUpgrades--;
        string temp = "" + Player.wateringCanLVL;
        wateringCanText.GetComponent<Text>().text = temp;  
    }
    
    public void confirm() {
        Player.money -= price;
        GameManager.timeRemainingInDay = 90f;
        GameManager.currentDay++;
        gameObject.SetActive(false);
    }
}
