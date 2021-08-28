using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static public float timeRemainingInDay;

    static public int currentDay;
    static public int moneyEarnedToday;
    static public int cropsSoldToday;
    static public int moneyEarned;
    static public int cropsSold;

    public GameObject canvas;
    public GameObject breakdown;
    public GameObject upgrade;
    public GameObject dayText;

    public GameObject Management;
    public GameObject Upgrade;

    ManagementManager manageMan;
    UpgradeManager upMan;

    private bool justChanged;
    private int currentCanvas; //0 = play, 1 = post day, 2 = seeds, 3 = upgrades, 4 = game over

    // Start is called before the first frame update
    void Start()
    {
        manageMan = Management.GetComponent<ManagementManager>();
        upMan = Upgrade.GetComponent<UpgradeManager>();
        timeRemainingInDay = 5f;
        currentCanvas = 0;
        currentDay = 1;
        canvas.SetActive(false);
        breakdown.SetActive(false);
        upgrade.SetActive(false);
        justChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemainingInDay > 0) {
            timeRemainingInDay -= Time.deltaTime;
            currentCanvas = 0;
        } else {
            Player.carroto = false;
            Player.peanks = false;
            Player.tomelone = false;
            currentCanvas = 1;
            justChanged = true;
        }

        handleCanvas();

        dayText.GetComponent<Text>().text = "Day" + currentDay;
    }


    void handleCanvas() {
        if (currentCanvas == 1 && justChanged)
            changeToBreakdown();
        else if (currentCanvas == 2 && justChanged)
            changeToManage();
        else if (currentCanvas == 3 && justChanged)
            changeToUpgrade();
    }

    void changeToBreakdown() {
        breakdown.SetActive(true);
        justChanged = false;
    }
    
    void changeToManage() {
        canvas.SetActive(true);
        breakdown.SetActive(false);
        justChanged = false;
    }

    void changeToUpgrade() {
        manageMan.confirm();
        upgrade.SetActive(true);
        canvas.SetActive(false);
        justChanged = false;
    }

    void gameOver() {
        //show game over screen
            //display:
                //Days Survived
                //Total Crops Sold
                //Total Money Made
    }

    public void setConfirmed() {
        currentCanvas++;
        justChanged = true;
        if (currentCanvas > 3) {
            upMan.confirm();
            currentCanvas = 0;
        }
    }
}
