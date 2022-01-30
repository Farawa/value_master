using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Nerds.Analytics;
using UnityEngine;

public class FB_Analytics : MonoBehaviour
{
    #region UnityMethods
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Start();
    }

    private void Start()
    {
        if (!FB.IsInitialized)
            FB.Init(InitCallback);
        else
            FB.ActivateApp();

        Analytics.OnLevelComplete += LevelAchieved;
    }

    private void OnDestroy()
    {
        Analytics.OnLevelComplete -= LevelAchieved;
    }
    #endregion

    #region Events
    public static void LevelAchieved(int toLevel)
    {
        toLevel++;

        Dictionary<string, object> Params = new Dictionary<string, object>
        {
            { "to", toLevel }
        };

        FB.LogAppEvent(AppEventName.AchievedLevel, null, Params);
    }
    #endregion

    #region Callbacks
    private void InitCallback()
    {
        if (FB.IsInitialized)
            FB.ActivateApp();
        else
            Debug.Log("Failed to Initialize the Facebook SDK");
    }
    #endregion
}
