using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Nerds.Analytics;

public class GA_Analytics : MonoBehaviour
{
    #region UnityMethods
    private void Awake()
    {
        GameAnalytics.Initialize();

        Analytics.OnLevelStart += LevelStart;
        Analytics.OnLevelFail += LevelFail;
        Analytics.OnLevelComplete += LevelComplete;
    }

    private void OnDestroy()
    {
        Analytics.OnLevelStart -= LevelStart;
        Analytics.OnLevelFail -= LevelFail;
        Analytics.OnLevelComplete -= LevelComplete;
    }
    #endregion

    #region Methods
    public static void LevelComplete(int level)
    {
        GameAnalytics.NewDesignEvent($"LevelWin{level}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version, level.ToString("0000"));
    }

    public static void LevelFail(int level)
    {
        GameAnalytics.NewDesignEvent($"LevelFail{level}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, Application.version, level.ToString("0000"));
    }

    public static void LevelStart(int level)
    {
        GameAnalytics.NewDesignEvent($"LevelStart{level}");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, level.ToString("0000"));
    }
    #endregion
}
