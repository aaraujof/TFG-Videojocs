using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controls;
    public GameObject seetings;
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject seetingsButton;
    public GameObject quitButton;
    public GameObject buttonsBackground;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SeeControls()
    {
        if (controls.activeSelf)
        {
            controls.SetActive(false);
            playButton.SetActive(true);
            controlsButton.SetActive(true);
            seetingsButton.SetActive(true);
            quitButton.SetActive(true);
            buttonsBackground.SetActive(true);
        }
        else
        {
            controls.SetActive(true);
            playButton.SetActive(false);
            controlsButton.SetActive(false);
            seetingsButton.SetActive(false);
            quitButton.SetActive(false);
            buttonsBackground.SetActive(false);
        }
    }

    public void SeeSetings()
    {
        if (seetings.activeSelf)
        {
            seetings.SetActive(false);
            playButton.SetActive(true);
            controlsButton.SetActive(true);
            seetingsButton.SetActive(true);
            quitButton.SetActive(true);
            buttonsBackground.SetActive(true);
        }
        else
        {
            seetings.SetActive(true);
            playButton.SetActive(false);
            controlsButton.SetActive(false);
            seetingsButton.SetActive(false);
            quitButton.SetActive(false);
            buttonsBackground.SetActive(false);
        }
    }
}
