using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WictoryChecker
{
    public static void CheckWin(int selectedValue)
    {
        int errorCount = Mathf.Abs(Level.Instance.itemsCount - selectedValue);
        if (errorCount <= Map.Instance.currentSafeRange)
            Win();
        else
            Lose();
    }

    private static void Win()
    {
        Interface.Instance.Windows.Complete.Show();
    }

    private static void Lose()
    {
        Interface.Instance.Windows.Fail.Show();
    }
}
