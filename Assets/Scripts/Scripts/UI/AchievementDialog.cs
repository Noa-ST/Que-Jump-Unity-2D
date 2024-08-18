using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDialog : Dialog
{
    public Text bestScoreText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (bestScoreText)
        {
            bestScoreText.text = Prefs.bestScore.ToString();
            Debug.Log("Best Score when showing AchievementDialog: " + PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0));
        }


    }

}
