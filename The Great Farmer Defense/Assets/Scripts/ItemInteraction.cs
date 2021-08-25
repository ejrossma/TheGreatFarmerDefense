using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject activator;
    public GameObject player;
    public Item item;
    private bool canPickup;
    private bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        activator.SetActive(false);
        canPickup = false;
        GetComponent<SpriteRenderer>().sprite = item.image;
    }

    void Update() 
    {
        if (canPickup && Input.GetKeyDown ("e")) {
            pickup();
        }

        if (player.GetComponent<Player>().holdingItem && Input.GetKeyDown("q")) {
            Debug.Log("Item Being Dropped: " + item);
            drop();
        }

        if (player.GetComponent<Player>().holdingItem && player.GetComponent<Player>().item.name == item.name) {
            pickedUp = true;
        }

        if (pickedUp) {
            StartCoroutine(fadeOut(player.GetComponent<Player>().inventory.GetComponent<SpriteRenderer>(), 1f));
            StartCoroutine(fadeOut(player.GetComponent<Player>().inventoryBackground.GetComponent<SpriteRenderer>(), 1f));
            transform.position = player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player") && player.GetComponent<Player>().holdingItem == false) {
            activator.SetActive(true);
            canPickup = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            activator.SetActive(false);
            canPickup = false;
        }
    }

    private void pickup() {
        player.GetComponent<Player>().holdingItem = true;
        player.GetComponent<Player>().item = item;
        player.GetComponent<Player>().inventory.GetComponent<SpriteRenderer>().sprite = item.image;
        
        player.GetComponent<Player>().inventory.SetActive(true);
        player.GetComponent<Player>().inventoryBackground.SetActive(true);

        GetComponent<SpriteRenderer>().enabled = false;
        activator.GetComponent<SpriteRenderer>().enabled = false;
        pickedUp = true;
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

        //Destroy(gameObject);
    }

    void drop() {
        
        pickedUp = false;
        player.GetComponent<Player>().holdingItem = false;
        GetComponent<SpriteRenderer>().enabled = true;
        activator.GetComponent<SpriteRenderer>().enabled = true;
    }

}
