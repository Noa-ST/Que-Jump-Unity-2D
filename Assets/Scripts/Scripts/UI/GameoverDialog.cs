using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverDialog : Dialog
{
    public Text bestScoreText;
    bool m_playBtnClicked;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (bestScoreText)
        {
            bestScoreText.text = Prefs.bestScore.ToString();
            Debug.Log("Best Score when showing AchievementDialog: " + PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0));
        }
    }

    public void Replay()
    {
        m_playBtnClicked = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void BackToHome()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_playBtnClicked)
        {
            GameGUIManager.Ins.ShowGameGUI(true);

            GameManager.Ins.PlayGame();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
