using System;
using GameAnalyticsSDK;
using GameEngine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {;
        GameAnalytics.Initialize();
        GameAnalytics.SetCustomId(Player.id);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Run", Game.currentRun.ToString());
        await Game.GameLoop();
    }
}
