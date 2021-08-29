using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject activator;
    public GameObject player;
    public Item item;
    private bool canPickup;
    private bool canTakeFromStorage;
    public bool pickedUp;
    public Sprite emptyImage;

    public GameObject itemToInstantiate;

    private Player pScript;

    // Start is called before the first frame update
    void Start()
    {
        canTakeFromStorage = false;
        pScript = player.GetComponent<Player>();
        activator.SetActive(false);
        canPickup = false;
        GetComponent<SpriteRenderer>().sprite = item.image;
    }

    void Update() 
    {
        if (pickedUp) {
            transform.position = player.transform.position;
        }
        if (canPickup && Input.GetKeyDown ("e") && !pScript.holdingItem) {
            pickup();
        }
        if (pScript.holdingItem && Input.GetKeyDown("q") && transform.position == pScript.transform.position) {
            drop();
        }
        if (canTakeFromStorage && Input.GetKeyDown ("e")) {
            takeFromStorage();
        }

        //check if item.filled needs be updated
        if (item.storage && item.name == "Carroto Barrel" && Player.carroto)
            item.filled = true;
        else if (item.storage && item.name == "Carroto Barrel" && !Player.carroto)
            item.filled = false;

        if (item.storage && item.name == "Peanks Barrel" && Player.peanks)
            item.filled = true;
        else if (item.storage && item.name == "Peanks Barrel" && !Player.peanks)
            item.filled = false;

        if (item.storage && item.name == "Tomelone Barrel" && Player.tomelone)
            item.filled = true;
        else if (item.storage && item.name == "Tomelone Barrel" && !Player.tomelone)
            item.filled = false;

        if (item.storage && item.filled) {
            GetComponent<SpriteRenderer>().sprite = item.image;
        } else if (item.storage && !item.filled) {
            GetComponent<SpriteRenderer>().sprite = emptyImage;
        }
    }

/////////////////////////////////////////////////////////////////////////
//COLLISION FUNCTIONS
/////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player") && pScript.holdingItem == false && item.pickupable) {
            activator.SetActive(true);
            canPickup = true;
        }

        if (collision.gameObject.name.Equals("Player") && item.storage && !pScript.holdingItem && item.filled) {
            activator.SetActive(true);
            canTakeFromStorage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            activator.SetActive(false);
            canPickup = false;
            canTakeFromStorage = false;
        }
    }

/////////////////////////////////////////////////////////////////////////
//HELPER FUNCTIONS
/////////////////////////////////////////////////////////////////////////

    private void pickup() {
        if (!pScript.holdingItem) {
            //handle player's side
            pScript.pickup(gameObject);
            //handle item's side
            GetComponent<SpriteRenderer>().enabled = false;
            activator.GetComponent<SpriteRenderer>().enabled = false;
            pickedUp = true;
        }
    }

    private void drop() {
        //handle player's side
        pScript.drop();
        //handle item's side
        pickedUp = false;
        GetComponent<SpriteRenderer>().sprite = item.image;
        GetComponent<SpriteRenderer>().enabled = true;
        activator.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void takeFromStorage() {
        //instantiate seed to be given to the player
        GameObject temp = Instantiate(itemToInstantiate);
        temp.GetComponent<ItemInteraction>().item = item.seedType;
        temp.GetComponent<ItemInteraction>().player = player;
        temp.GetComponent<ItemInteraction>().pickedUp = true;
        temp.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<ItemInteraction>().item.image;
        temp.GetComponent<SpriteRenderer>().enabled = false;

        activator.SetActive(false);

        pScript.pickup(temp);
    }
}
