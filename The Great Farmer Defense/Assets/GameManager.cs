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
    public GameObject gameover;
    public GameObject background;
    public GameObject dayText;

    public GameObject Management;
    public GameObject Upgrade;
    public GameObject player;

    ManagementManager manageMan;
    UpgradeManager upMan;
    Player pScript;

    public GameObject playerMoneyText;

    public AudioSource ambiance;

    private bool justChanged;
    private bool postDay;
    private int currentCanvas; //0 = play, 1 = post day, 2 = seeds, 3 = upgrades, 4 = game over

    // Start is called before the first frame update
    void Start()
    {
        manageMan = Management.GetComponent<ManagementManager>();
        upMan = Upgrade.GetComponent<UpgradeManager>();
        pScript = player.GetComponent<Player>();
        timeRemainingInDay = 10f;
        currentCanvas = 0;
        currentDay = 1;
        canvas.SetActive(false);
        breakdown.SetActive(false);
        upgrade.SetActive(false);
        gameover.SetActive(false);
        justChanged = false;
        postDay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemainingInDay > 0) {
            timeRemainingInDay -= Time.deltaTime;
            currentCanvas = 0;
            postDay = false;
            ambiance.mute = false;
        } else if (!postDay) {
            Player.carroto = false;
            Player.peanks = false;
            Player.tomelone = false;
            pScript.reset();
            currentCanvas = 1;
            postDay = true;
            justChanged = true;
            ambiance.mute = true;
        }

        handleCanvas();

        dayText.GetComponent<Text>().text = "Day" + currentDay;
        playerMoneyText.GetComponent<Text>().text = "" + Player.money;
    }


    void handleCanvas() {
        if (justChanged) {
            if (currentCanvas == 1)
                changeToBreakdown();
            else if (currentCanvas == 2)
                changeToManage();
            else if (currentCanvas == 3)
                changeToUpgrade();
            justChanged = false;
        }
    }

    void changeToBreakdown() {
        breakdown.SetActive(true);
        breakdown.GetComponent<BreakdownManager>().setValues();
    }
    
    void changeToManage() {
        canvas.SetActive(true);
        breakdown.SetActive(false);
    }

    void changeToUpgrade() {
        manageMan.confirm();
        upgrade.SetActive(true);
        canvas.SetActive(false);
    }

    void changeToGameOver() {
        breakdown.SetActive(false);
        gameover.SetActive(true);
    }

    public void gameOver() {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void setConfirmed() {
        if (Player.money < 50 && currentCanvas == 1) {
            changeToGameOver();
            return;
        }
        currentCanvas++;
        justChanged = true;
        if (currentCanvas > 3) {
            upMan.confirm();
            currentCanvas = 0;
        }
    }
}
