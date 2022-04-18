using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controls;
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject quitButton;
    public GameObject title;

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
            title.SetActive(true);
            playButton.SetActive(true);
            controlsButton.SetActive(true);
            quitButton.SetActive(true);
        }
        else
        {
            controls.SetActive(true);
            title.SetActive(false);
            playButton.SetActive(false);
            controlsButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }
}
