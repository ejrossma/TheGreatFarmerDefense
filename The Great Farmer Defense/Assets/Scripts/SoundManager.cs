using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MenuScene"){
            if (!PlayerPrefs.HasKey("musicVolume")) {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            } else {
                Load();
            }
        }
    }

    public void changeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
