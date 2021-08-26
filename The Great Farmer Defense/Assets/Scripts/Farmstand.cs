using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmstand : MonoBehaviour
{
    public GameObject player;
    public GameObject activator;

    private bool canSell;
    private Player pScript;

    // Start is called before the first frame update
    void Start()
    {
        pScript = player.GetComponent<Player>();
        activator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell && Input.GetKeyDown ("e")) {
            pScript.sell();
            activator.SetActive(false);
        }
    }

/////////////////////////////////////////////////////////////////////////
//COLLISION FUNCTIONS
/////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player") && pScript.holdingItem && pScript.item.canBeSold) {
            activator.SetActive(true);
            canSell = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            activator.SetActive(false);
            canSell = false;
        }
    }
}
