using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject inventory;
    public GameObject inventoryBackground;

    private float speed;
    public Item item;
    public GameObject itemObj;
    public bool holdingItem;
    public bool justPickedUpItem;

    static public int money;
    static public int runningShoesLVL;
    static public int seedQualityLVL;
    static public int wateringCanLVL;

    //if player purchases a barrel they are true else false
    static public bool carroto;
    static public bool peanks;
    static public bool tomelone;

    private float maxSpeed;
    private float acceleration;

    void Start()
    {
        maxSpeed = 2;
        acceleration = 0.1f;
        carroto = true;
        peanks = false;
        tomelone = false;
        holdingItem = false;
        speed = 0;
        justPickedUpItem = false;
        item = null;

        runningShoesLVL = 0;
        seedQualityLVL = 0;
        wateringCanLVL = 0;
        money = 1000;
    }

    void Update()
    {

        //IF JUST PICKED UP ITEM THEN FADE OUT INDICATOR ABOVE PLAYER'S HEAD
        if (justPickedUpItem) {
            StartCoroutine(fadeOut(inventory.GetComponent<SpriteRenderer>(), 1f));
            StartCoroutine(fadeOut(inventoryBackground.GetComponent<SpriteRenderer>(), 1f));
            justPickedUpItem = false;
        }
        
        //MOVEMENT CODE
        Vector3 pos = transform.position;

        if (Input.GetKey (KeyCode.UpArrow)) {
            if (speed < maxSpeed + Player.runningShoesLVL)
                speed += acceleration;           
            pos.y += speed * Time.deltaTime;
        } else if (Input.GetKey (KeyCode.DownArrow)) {
            if (speed < maxSpeed + Player.runningShoesLVL)
                speed += acceleration;
            pos.y -= speed * Time.deltaTime;
        } else if (Input.GetKey (KeyCode.RightArrow)) {
            if (speed < maxSpeed + Player.runningShoesLVL)
                speed += acceleration;
            pos.x += speed * Time.deltaTime;
        } else if (Input.GetKey (KeyCode.LeftArrow)) {
            if (speed < maxSpeed + Player.runningShoesLVL)
                speed += acceleration; 
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }

/////////////////////////////////////////////////////////////////////////
//HELPER FUNCTIONS
/////////////////////////////////////////////////////////////////////////    

    public void pickup(GameObject newItemObj) {
        //player is now holding the item
        holdingItem = true;
        justPickedUpItem = true;
        itemObj = newItemObj;
        item = newItemObj.GetComponent<ItemInteraction>().item;
        //update image & show indicator above head to show what item the player picked up
        inventory.GetComponent<SpriteRenderer>().sprite = item.image;
        inventory.SetActive(true);
        inventoryBackground.SetActive(true);
    }

    public void drop() {
        //player is no longer holding item
        holdingItem = false;
        //update item & change indicator image
        item = null;
        itemObj = null;
        inventory.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void plant() {
        holdingItem = false;
        item = null;
        Destroy(itemObj);
        itemObj = null;
        inventory.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void sell() {
        money += calculatePrice(item.sellPrice);
        GameManager.moneyEarnedToday += calculatePrice(item.sellPrice);;
        GameManager.moneyEarned += calculatePrice(item.sellPrice);;
        holdingItem = false;
        item = null;
        Destroy(itemObj);
        itemObj = null;
        inventory.GetComponent<SpriteRenderer>().sprite = null;
        GameManager.cropsSold++;
        GameManager.cropsSoldToday++;
    }

    private int calculatePrice(int value) {
        if (Player.seedQualityLVL == 0) {
            return value / 2;
        } else if (Player.seedQualityLVL == 1) {
            return value;
        } else if (Player.seedQualityLVL == 2) {
            return value + (value / 2);
        } else if (Player.seedQualityLVL == 3) {
            return value * 2;
        }
        return 0;
    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration) {
        float counter = 0;
        Color spriteColor = MyRenderer.material.color;
        while (counter < duration) {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }    
}
