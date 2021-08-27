using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementManager : MonoBehaviour
{
    private int carrotoQuantity;
    private int peanksQuantity;
    private int tomeloneQuantity;

    public GameObject carrotoPlus;
    public GameObject carrotoMinus;
    public GameObject carrotoQuantityText;

    public GameObject peanksPlus;
    public GameObject peanksMinus;
    public GameObject peanksQuantityText;

    public GameObject tomelonePlus;
    public GameObject tomeloneMinus;
    public GameObject tomeloneQuantityText;

    public GameObject priceText;
    private int price;
    public GameObject moneyText;
    private int money;

    public GameObject confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        carrotoQuantity = 0;
        peanksQuantity = 0;
        tomeloneQuantity = 0;

        money = 250;
        string temp = "Money: " + money;
        moneyText.GetComponent<Text>().text = temp; 
    }

    // Update is called once per frame
    void Update()
    {
        if (money >= price && (carrotoQuantity + peanksQuantity + tomeloneQuantity) > 0) {
            confirmButton.SetActive(true);
        } else {
            confirmButton.SetActive(false);
        }

        price = (carrotoQuantity * 50) + (peanksQuantity * 100) + (tomeloneQuantity * 200);
        string temp = "Price: " + price;
        priceText.GetComponent<Text>().text = temp;

        if (carrotoQuantity == 1) {
            carrotoPlus.SetActive(false);
            carrotoMinus.SetActive(true);
        } else if (carrotoQuantity == 0) {
            carrotoPlus.SetActive(true);
            carrotoMinus.SetActive(false);
        }

        if (peanksQuantity == 1) {
            peanksPlus.SetActive(false);
            peanksMinus.SetActive(true);
        } else if (peanksQuantity == 0) {
            peanksPlus.SetActive(true);
            peanksMinus.SetActive(false);
        }

        if (tomeloneQuantity == 1) {
            tomelonePlus.SetActive(false);
            tomeloneMinus.SetActive(true);
        } else if (tomeloneQuantity == 0) {
            tomelonePlus.SetActive(true);
            tomeloneMinus.SetActive(false);
        }        
    }

/////////////////////////////////////////////////////////////////////////
//HELPER FUNCTIONS
/////////////////////////////////////////////////////////////////////////
    public void cPlus() {
        carrotoQuantity++;
        string temp = "" + carrotoQuantity;
        carrotoQuantityText.GetComponent<Text>().text = temp;
    }

    public void pPlus() {
        peanksQuantity++;
        string temp = "" + peanksQuantity;
        peanksQuantityText.GetComponent<Text>().text = temp;        
    }

    public void tPlus() {
        tomeloneQuantity++;
        string temp = "" + tomeloneQuantity;
        tomeloneQuantityText.GetComponent<Text>().text = temp;        
    }

    public void cMinus() {
        carrotoQuantity--;
        string temp = "" + carrotoQuantity;
        carrotoQuantityText.GetComponent<Text>().text = temp;
    }

    public void pMinus() {
        peanksQuantity--;
        string temp = "" + peanksQuantity;
        peanksQuantityText.GetComponent<Text>().text = temp;
    }

    public void tMinus() {
        tomeloneQuantity--;
        string temp = "" + tomeloneQuantity;
        tomeloneQuantityText.GetComponent<Text>().text = temp;
    }

    public void confirm() {
        Debug.Log("Confirmed Purchase");
        //change to next scene
        //subtract from player's money
        //give player the right barrels
    }
}
