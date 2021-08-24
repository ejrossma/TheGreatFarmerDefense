using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    public Plant[] possiblePlants; //0 = carrato, 1 = peanks , 2 = tomelone
    public Sprite[] indicatorImages;
    public GameObject indicator;
    
    
    private float growTime; //adjustable timer for each growth stage
    private float harvestTime; //how long plant takes to die
    private float growInterval; //interval in between each growth
    private int status; //0 = hide indicator, 1 = need water (Age 0 & 2), 2 = ready to harvest, 3 = dead/needs to be removed
    private int age; //0-3 growth cycle, 4 = ready to harvest, 5 = dead
    private int watered; //0 = no water, 1 = watered once, 2 = watered twice (needs to be watered at age 0 && age 2)
    private SpriteRenderer indicatorSprite; //sprite being shown above plant
    private Plant currentPlant; //current plant being used
    private SpriteRenderer plantSprite; //current image being shown

    void Start() {
        harvestTime = 10;
        growInterval = 5;
        indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        currentPlant = possiblePlants[0];
        age = 0;
        watered = 0; //hasnt been watered yet
        status = 1; //starts out needing water
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
        updateSprite();
        growTime = growInterval;
    }

    void Update()
    {
        //FOR TESTING PURPOSES
            //NEEDS TO BE REPLACED WITH PLAYER COLLIDING WITH THE PLANTS WHILE HAVING THE WATERING CAN
        if (Input.GetButtonDown("Fire1")) {
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
       
        Debug.Log("Status: " + status);
        updateIndicator(); //update indicator
        if (status == 3 && age == 5) { //edge case because of spaghetti code
            updateSprite();
        }

        //GROWTH CYCLE + TIMER
        if (status != 3) { //if crop isn't dead
            updateSprite(); //works for all cases but not harvesting fast enough
            if (growTime > 0) { //if growTime greater than 0 keep counting down
                growTime -= Time.deltaTime;
            } else { //when time runs out the plant ages & is replaced with a new image
                age++;
                Debug.Log("Current Age: " + age);
                //CROP CHECKS
                if ((age == 1 && watered == 0) || (age == 3 && watered == 1) || age == 5) //didn't get watered or harvested
                    status = 3;
                if (age == 2 && watered == 1) //update indicator for second watering
                    status = 1;
                if (age == 4) { //crop ready to be harvested
                    status = 2;
                    growTime = harvestTime;
                } else
                    growTime = growInterval;
            }
        }
    }

    void updateSprite() {
        plantSprite.sprite = currentPlant.growthImages[age]; //update plant image
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
        }
    }
}
