using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Text scoreCountingText;
    public Image powerBarSlider;

    public Dialog achievementDialog;
    public Dialog helpDialog;
    public Dialog gameoverDialog;


    public override void Awake()
    {
        MakeSingleton(false); 
    }

    public void ShowGameGUI(bool isShow)
    {
        if (gameGUI)
        {
            gameGUI.SetActive(isShow);
        }

        if (homeGUI)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void UpdateScoreCounting(int score)
    {
        if (scoreCountingText)
            scoreCountingText.text = score.ToString();
    }
    
    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if (powerBarSlider)
            powerBarSlider.fillAmount = curVal / totalVal;
    }

    public void ShowAchievementDialog()
    {
        if (achievementDialog)
            achievementDialog.Show(true);
    }
    public void ShowHelptDialog()
    {
        if (helpDialog)
            helpDialog.Show(true);
    }
    public void ShowGameovertDialog()
    {
        if (gameoverDialog)
            gameoverDialog.Show(true);
    }
}
