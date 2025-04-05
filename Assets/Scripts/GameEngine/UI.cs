using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class UI : MonoBehaviour
    {
        public Image likes;
        public TextMeshProUGUI likesCount;
        public Image comments;

        public Image battery;
        public TextMeshProUGUI batteryLevel;

        private void Awake()
        {
            Game.ui = this;
        }

        private void Start()
        {
            changeBatteryLevel(Player.powerLevel, Player.powerLevel);
        }

        private void Update()
        {
            setCountersFromEncounter();
        }

        private void setCountersFromEncounter()
        {
            likesCount.text = Game.currentEncounter.getLikes().ToString();
        }

        public async UniTask changeBatteryLevel(int from, int to)
        {
            batteryLevel.text = to + "%";
        }
    }
}