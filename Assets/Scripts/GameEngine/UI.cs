using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class UI : MonoBehaviour
    {
        public GameObject uiContainer;
        private Vector3 originalContainerPosition;
        
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
            originalContainerPosition = uiContainer.transform.position;
        }

        private void Update()
        {
            setCountersFromEncounter();
        }

        private void setCountersFromEncounter()
        {
            if (Game.currentEncounter != null)
            {
                likesCount.text = Game.currentEncounter.getLikes().ToString();
            }
        }

        public async UniTask changeBatteryLevel(int from, int to)
        {
            batteryLevel.text = to + "%";
        }

        public async UniTask openKeyboard(float sizeWorld)
        {
            await uiContainer.transform.DOMove(new Vector3(uiContainer.transform.position.x,
                uiContainer.transform.position.y + sizeWorld, uiContainer.transform.position.z), 0.5f);
            
        }
        
        public async UniTask closeKeyboard()
        {
            await uiContainer.transform.DOMove(originalContainerPosition, 0.5f);
        }
    }
}