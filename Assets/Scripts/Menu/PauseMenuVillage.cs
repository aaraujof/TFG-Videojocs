using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuVillage : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    public GameObject controls;
    public GameObject seetings;
    public GameObject resumeButton;
    public GameObject mainButton;
    public GameObject controlsButton;
    public GameObject seetingsButton;
    public GameObject menuBackground;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void SeeControls()
    {
        if (controls.activeSelf)
        {
            controls.SetActive(false);
            resumeButton.SetActive(true);
            mainButton.SetActive(true);
            controlsButton.SetActive(true);
            seetingsButton.SetActive(true);
            menuBackground.SetActive(true);
        }
        else
        {
            controls.SetActive(true);
            resumeButton.SetActive(false);
            mainButton.SetActive(false);
            controlsButton.SetActive(false);
            seetingsButton.SetActive(false);
            menuBackground.SetActive(false);
        }
    }

    public void SeeSetings()
    {
        if (seetings.activeSelf)
        {
            seetings.SetActive(false);
            resumeButton.SetActive(true);
            mainButton.SetActive(true);
            controlsButton.SetActive(true);
            seetingsButton.SetActive(true);
            menuBackground.SetActive(true);
        }
        else
        {
            seetings.SetActive(true);
            resumeButton.SetActive(false);
            mainButton.SetActive(false);
            controlsButton.SetActive(false);
            seetingsButton.SetActive(false);
            menuBackground.SetActive(false);
        }
    }
}
