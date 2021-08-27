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
            Debug.Log("Item Being Picked Up: " + item);
            pickup();
        }
        if (pScript.holdingItem && Input.GetKeyDown("q") && transform.position == pScript.transform.position) {
            Debug.Log("Item Being Dropped: " + item);
            drop();
        }
        if (canTakeFromStorage && Input.GetKeyDown ("e")) {
            Debug.Log("Taking From Storage");
            takeFromStorage();
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

        if (collision.gameObject.name.Equals("Player") && item.storage && !pScript.holdingItem) {
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
        //handle player's side
        pScript.pickup(gameObject);
        //handle item's side
        GetComponent<SpriteRenderer>().enabled = false;
        activator.GetComponent<SpriteRenderer>().enabled = false;
        pickedUp = true;
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
