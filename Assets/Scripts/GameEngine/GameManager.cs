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
        GameAnalytics.SetCustomId(Guid.NewGuid().ToString());
        await Game.GameLoop();
    }
}
