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
    public int Frog;
    public int FrogGreen;
    public int Runs;
    public int Win;
    public int Lose;

    public TMP_Text larvaKills;
    public TMP_Text slimeKills;
    public TMP_Text mouseKills;
    public TMP_Text FrogKills;
    public TMP_Text FrogGreenKills;
    public TMP_Text attempts;
    public TMP_Text win;
    public TMP_Text lose;

    // Start is called before the first frame update
    void Start()
    {
        larva = PlayerPrefs.GetInt("larva");
        slime = PlayerPrefs.GetInt("slime");
        mouse = PlayerPrefs.GetInt("mouse");
        Frog = PlayerPrefs.GetInt("Frog");
        FrogGreen = PlayerPrefs.GetInt("FrogGreen");
        Win = PlayerPrefs.GetInt("Win");
        Lose = PlayerPrefs.GetInt("Lose");
        Runs = PlayerPrefs.GetInt("Runs");

        larvaKills.text = larva.ToString();
        slimeKills.text = slime.ToString();
        mouseKills.text = mouse.ToString();
        FrogKills.text = Frog.ToString();
        FrogGreenKills.text = FrogGreen.ToString();
        attempts.text = "Total Attempts: " + Runs.ToString();
        win.text = "Games Won: " + Runs.ToString();
        lose.text = "Games Lost: " + Runs.ToString();
    }
}
