using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider volumeSlider;

    // Start is called before the first frame update
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void DeleteGameSave()
    {
        PlayerPrefs.SetInt("larva", 0);
        PlayerPrefs.SetInt("slime", 0);
        PlayerPrefs.SetInt("mouse", 0);
        PlayerPrefs.SetInt("Frog", 0);
        PlayerPrefs.SetInt("FrogGreen", 0);
        PlayerPrefs.SetInt("Win", 0);
        PlayerPrefs.SetInt("Lose", 0);
        PlayerPrefs.SetInt("Runs", 0);
        PlayerPrefs.SetFloat("musicVolume", 0.5f);
        Load();
        PlayerPrefs.SetInt("mushroom", 0);
        PlayerPrefs.SetInt("bamboo", 0);
        PlayerPrefs.SetInt("skull", 0);
        PlayerPrefs.SetInt("Flam", 0);
        PlayerPrefs.SetInt("Cyclop", 0);
        PlayerPrefs.Save();
    }
}
