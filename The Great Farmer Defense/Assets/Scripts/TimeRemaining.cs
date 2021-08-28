using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeRemaining : MonoBehaviour
{
    public Slider slider;
    private Scene scene;

    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    void Update() {
        setTime();
    }

    public void setTime() {
        float temp = GameManager.timeRemainingInDay/90;
        slider.value = temp;
    }
}
