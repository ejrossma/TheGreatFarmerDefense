using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmstand : MonoBehaviour
{
    public GameObject player;
    public GameObject activator;
    public GameObject indicator;

    private bool canSell;
    private Player pScript;
    private Vector3 startPosition;
    private Vector3 temp;
    private bool fadeComplete;
    private bool justSold;
    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        fadeComplete = false;
        startPosition = indicator.transform.position;
        pScript = player.GetComponent<Player>();
        activator.SetActive(false);
        move = false;
        temp = new Vector3(indicator.transform.position.x, indicator.transform.position.y + 4, indicator.transform.position.z);
        indicator.SetActive(false);
        justSold = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell && Input.GetKeyDown ("e")) {
            pScript.sell();
            indicator.SetActive(true);
            justSold = true;
            activator.SetActive(false);
        }

        if (justSold) {
            indicator.transform.position = startPosition;
            fadeComplete = false;
            move = false;
            StartCoroutine(fadeOutAndMove(indicator.GetComponent<SpriteRenderer>(),  1f));
            justSold = false;
        }
        if (!fadeComplete && move) {
            indicator.transform.position = Vector3.MoveTowards(indicator.transform.position, temp, 0.004f);
        }


        if (Vector3.Distance(indicator.transform.position, temp) < 0.1f && fadeComplete) {
            indicator.transform.position = startPosition;
            fadeComplete = false;
            move = false;
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

    IEnumerator fadeOutAndMove(SpriteRenderer MyRenderer, float duration) {
        float counter = 0;
        Color spriteColor = MyRenderer.material.color;
        move = true;
        while (counter < duration) {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
        fadeComplete = true;
    }

}
