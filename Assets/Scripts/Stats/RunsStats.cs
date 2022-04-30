using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunsStats : MonoBehaviour
{
    public int larva;
    public int slime;
    public int mouse;
    public int mushroom;
    public int bamboo;
    public int skull;
    public int Frog;
    public int FrogGreen;
    public int Flam;
    public int Cyclop;
    public int Runs;
    public int Win;
    public int Lose;

    public TMP_Text larvaKills;
    public TMP_Text slimeKills;
    public TMP_Text mouseKills;
    public TMP_Text mushroomKills;
    public TMP_Text bambooKills;
    public TMP_Text skullKills;
    public TMP_Text FrogKills;
    public TMP_Text FrogGreenKills;
    public TMP_Text FlamKills;
    public TMP_Text CyclopKills;
    public TMP_Text attempts;
    public TMP_Text win;
    public TMP_Text lose;

    public GameObject larvaChallenge;
    public GameObject slimeChallenge;
    public GameObject mouseChallenge;
    public GameObject mushroomChallenge;
    public GameObject bambooChallenge;
    public GameObject skullChallenge;
    public GameObject FrogChallenge;
    public GameObject FrogGreenChallenge;
    public GameObject FlamChallenge;
    public GameObject CyclopChallenge;

    // Start is called before the first frame update
    void Start()
    {
        larva = PlayerPrefs.GetInt("larva");
        slime = PlayerPrefs.GetInt("slime");
        mouse = PlayerPrefs.GetInt("mouse");
        mushroom = PlayerPrefs.GetInt("mushroom");
        bamboo = PlayerPrefs.GetInt("bamboo");
        skull = PlayerPrefs.GetInt("skull");
        Frog = PlayerPrefs.GetInt("Frog");
        FrogGreen = PlayerPrefs.GetInt("FrogGreen");
        Flam = PlayerPrefs.GetInt("Flam");
        Cyclop = PlayerPrefs.GetInt("Cyclop");
        Win = PlayerPrefs.GetInt("Win");
        Lose = PlayerPrefs.GetInt("Lose");
        Runs = PlayerPrefs.GetInt("Runs");
        
        larvaKills.text = larva.ToString();
        slimeKills.text = slime.ToString();
        mouseKills.text = mouse.ToString();
        mushroomKills.text = mushroom.ToString();
        bambooKills.text = bamboo.ToString();
        skullKills.text = skull.ToString();
        FrogKills.text = Frog.ToString();
        FrogGreenKills.text = FrogGreen.ToString();
        FlamKills.text = Flam.ToString();
        CyclopKills.text = Cyclop.ToString();
        attempts.text = "Total Attempts: " + Runs.ToString();
        win.text = "Games Won: " + Win.ToString();
        lose.text = "Games Lost: " + Lose.ToString();

        if (larva >= 100)
        {
            larvaChallenge.SetActive(true);
        }
        else
        {
            larvaChallenge.SetActive(false);
        }
        if (slime >= 100)
        {
            slimeChallenge.SetActive(true);
        }
        else
        {
            slimeChallenge.SetActive(false);
        }
        if (mouse >= 100)
        {
            mouseChallenge.SetActive(true);
        }
        else
        {
            mouseChallenge.SetActive(false);
        }
        if (mushroom >= 100)
        {
            mushroomChallenge.SetActive(true);
        }
        else
        {
            mushroomChallenge.SetActive(false);
        }
        if (bamboo >= 100)
        {
            bambooChallenge.SetActive(true);
        }
        else
        {
            bambooChallenge.SetActive(false);
        }
        if (skull >= 100)
        {
            skullChallenge.SetActive(true);
        }
        else
        {
            skullChallenge.SetActive(false);
        }
        if (Frog >= 15)
        {
            FrogChallenge.SetActive(true);
        }
        else
        {
            FrogChallenge.SetActive(false);
        }
        if (FrogGreen >= 15)
        {
            FrogGreenChallenge.SetActive(true);
        }
        else
        {
            FrogGreenChallenge.SetActive(false);
        }
        if (Flam >= 15)
        {
            FlamChallenge.SetActive(true);
        }
        else
        {
            FlamChallenge.SetActive(false);
        }
        if (Cyclop >= 15)
        {
            CyclopChallenge.SetActive(true);
        }
        else
        {
            CyclopChallenge.SetActive(false);
        }
    }
}
