using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public GameObject inventory;
    public GameObject inventoryBackground;

    private float speed;
    public Item item;
    public bool holdingItem;

    void Start()
    {
        holdingItem = false;
        speed = 0;
    }

    void Update()
    {

        //HIDE POP UP IF PLAYER DOESN'T HAVE ITEM
        if (!holdingItem) {
            inventory.SetActive(false);
            inventoryBackground.SetActive(false);
        } else if (Input.GetKeyDown("q")) {
            item = null;
            holdingItem = false;
        }


        
        //MOVEMENT CODE
        Vector3 pos = transform.position;

        if (Input.GetKey ("w")) {
            if (speed < maxSpeed)
                speed += acceleration;           
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey ("s")) {
            if (speed < maxSpeed)
                speed += acceleration;
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey ("d")) {
            if (speed < maxSpeed)
                speed += acceleration;
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey ("a")) {
            if (speed < maxSpeed)
                speed += acceleration; 
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }

}
