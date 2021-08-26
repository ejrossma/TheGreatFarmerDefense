using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    public Item[] possibleHarvest;
    public Sprite[] indicatorImages;
    public GameObject indicator;
    public GameObject player;
    public GameObject activator;

    public GameObject item; //used for when the player harvests a crop
    
    private GameObject harvestedCrop;
    private bool canPlant;
    private bool canPickup; //for harvesting the plant
    private float growTime; //adjustable timer for each growth stage
    private float harvestTime; //how long plant takes to die
    private float growInterval; //interval in between each growth
    private int status; //-1 = need seed, 0 = hide indicator, 1 = need water (Age 0 & 2), 2 = ready to harvest, 3 = dead/needs to be removed
    private int age; //-1 = needs seeds, 0-3 growth cycle, 4 = ready to harvest, 5 = dead
    private int watered; //0 = no water, 1 = watered once, 2 = watered twice (needs to be watered at age 0 && age 2)
    private SpriteRenderer indicatorSprite; //sprite being shown above plant
    private Plant currentPlant; //current plant being used
    private SpriteRenderer plantSprite; //current image being shown

    private Player pScript;

    void Start() {
        pScript = player.GetComponent<Player>();
        harvestTime = 10;
        growInterval = 5;
        indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        age = -1;
        watered = 0; //hasnt been watered yet
        status = -1; //starts out without a seed
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
        growTime = growInterval;
        canPickup = false;
        activator.SetActive(false);
    }

    void Update()
    {
        updateIndicator(); //update indicator
        if (status == 3 && age == 5) { //edge case because of spaghetti code
            updateSprite();
        }

        //PLAYER CAN HARVEST
        if (canPickup && Input.GetKeyDown ("e") && status == 2) {
            pickup();
            Debug.Log("Item Being Picked Up: " + harvestedCrop.GetComponent<ItemInteraction>().item);
        }

        if (canPlant && Input.GetKeyDown ("e")) {
            Debug.Log("Planted " + pScript.item.name);
            plant();   
        }

        //GROWTH CYCLE + TIMER
        if (status != 3 && status != -1 && age < 6) { //if crop isn't dead or doesn't need seeds
            updateSprite(); //works for all cases but not harvesting fast enough
            if (growTime > 0) { //if growTime greater than 0 keep counting down
                growTime -= Time.deltaTime;
            } else { //when time runs out the plant ages & is replaced with a new image
                age++;
                //CROP CHECKS
                if ((age == 1 && watered == 0) || (age == 3 && watered == 1) || age == 5) //didn't get watered or harvested
                    status = 3;
                if ((age == 2 && watered == 1) || (age == 0 && watered == 0)) //update indicator for second watering
                    status = 1;
                if (age == 4) { //crop ready to be harvested
                    status = 2;
                    growTime = harvestTime;
                } else
                    growTime = growInterval;
            }
        }
    }

/////////////////////////////////////////////////////////////////////////
//COLLISION FUNCTIONS
/////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player") && pScript.holdingItem) {
            if (pScript.item.name == "Watering Can") {
                if (age == 0 && watered == 0) {
                    watered = 1;
                    status = 0;
                    Debug.Log("Amount of Times Watered: " + watered);
                } else if (age == 2 && watered == 1) {
                    watered = 2;
                    status = 0;
                    Debug.Log("Amount of Times Watered: " + watered);
                }
            }
        }

        if (collision.gameObject.name.Equals("Player") && !pScript.holdingItem && status == 2) {
            Debug.Log("Player can harvest");
            activator.SetActive(true);
            canPickup = true;
        }

        if (collision.gameObject.name.Equals("Player") && pScript.holdingItem && pScript.item.canBePlanted && status == -1) {
            Debug.Log("Player can Plant");
            activator.SetActive(true);
            canPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            activator.SetActive(false);
            canPickup = false;
            canPlant = false;
        }
    }

/////////////////////////////////////////////////////////////////////////
//HELPER FUNCTIONS
/////////////////////////////////////////////////////////////////////////

    void updateSprite() {
        if (age == -1) {
            plantSprite.sprite = null;
        } else {
            plantSprite.sprite = currentPlant.growthImages[age]; //update plant image
        }
    }

    void updateIndicator() {

        //CASES WHERE TRASH INDICATOR SHOWS UP
            //AGE 1 && AGE 3 IF PLANT NOT WATERED
            //AGE 5 IF NOT HARVESTED IN TIME
        //CASES WHERE CHECK INDICATOR SHOWS UP
            //AGE 4
        //CASES WHERE WATER INDICATOR SHOWS UP
            //AGE 0 && AGE 2   

        if (status == 0) {
            indicator.SetActive(false); //hide indicator game object
        } else if (status == 1) {
            indicator.SetActive(true); //show water indicator
            indicatorSprite.sprite = indicatorImages[0];
        } else if (status == 2) {
            indicator.SetActive(true); //show check indicator
            indicatorSprite.sprite = indicatorImages[1];
        } else if (status == 3) {
            indicator.SetActive(true); //show trash indicator
            indicatorSprite.sprite = indicatorImages[2];
        } else if (status == -1) {
            indicator.SetActive(true); //show seeds indicator
            indicatorSprite.sprite = indicatorImages[3];
        }
    }

    private void pickup() {
        //INSTANTIATE HARVESTED ITEM GAME OBJECT
        harvestedCrop = Instantiate(item);
        harvestedCrop.GetComponent<ItemInteraction>().item = possibleHarvest[currentPlant.index];
        harvestedCrop.GetComponent<SpriteRenderer>().sprite = harvestedCrop.GetComponent<ItemInteraction>().item.image;
        harvestedCrop.GetComponent<ItemInteraction>().player = player;
        harvestedCrop.GetComponent<SpriteRenderer>().enabled = false;
        harvestedCrop.GetComponent<ItemInteraction>().pickedUp = true;
        
        //HANDLE PLAYER'S SIDE ONCE ITEM IS INSTANTIATED
        pScript.pickup(harvestedCrop);
        //GET READY FOR NEXT SEED
        age = -1;
        status = -1;
        watered = 0;
        updateSprite();
    }

    private void plant() {
        //HANDLE CROP SIDE
        currentPlant = pScript.item.plantType; //set plant to right type
        status = 0; //change to status 0 so the indicator goes away for seeds and it starts growing

        //HANDLE PLAYERS SIDE OF IT
        pScript.plant();
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
