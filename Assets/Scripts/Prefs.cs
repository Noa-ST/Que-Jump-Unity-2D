using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static int bestScore
    {
        set {
            if(PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0) < value)
            {
                PlayerPrefs.SetInt(Prefconsts.BEST_SCORE, value);
                Debug.Log("Updated Best Score: " + value);
            }
        }

        get => PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0);
    }
}
