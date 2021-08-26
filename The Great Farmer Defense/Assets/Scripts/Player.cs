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
    public GameObject itemObj;
    public bool holdingItem;
    public bool justPickedUpItem;

    void Start()
    {
        holdingItem = false;
        speed = 0;
        justPickedUpItem = false;
        item = null;
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
        Debug.Log("Player Script Item Being Dropped: " + item);
        //player is no longer holding item
        holdingItem = false;
        //update item & change indicator image
        item = null;
        inventory.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void plant() {
        Debug.Log("Player Script Planting:" + item.name);
        holdingItem = false;
        item = null;
        inventory.GetComponent<SpriteRenderer>().sprite = null;
        Destroy(itemObj);
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
