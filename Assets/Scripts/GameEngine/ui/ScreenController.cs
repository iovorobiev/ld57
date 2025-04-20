using System;
using System.Numerics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace GameEngine.ui
{
    public class ScreenController : MonoBehaviour
    {
        public GameObject encoutnerHolder;
        public GameObject vocabularyHolder;

        public GameObject screenPosition;
        public GameObject leftHidePosition;
        public GameObject rightHidePosition;

        public Toggle reelsButton;
        public Toggle vocabularyButton;

        public float screenSwitchDuration = 0.2f;
        
        public Image battery;
        public TextMeshProUGUI batteryLevel;

        public Notification stressLevel;

        private void Awake()
        {
            Game.screenController = this;
            changeBatteryLevel(Player.powerLevel, Player.powerLevel);
        }

        public void startListeningButtons()
        {
            reelsButton.Select();
            reelsButton.onValueChanged.AddListener(async isChecked =>
            {
                if (isChecked)
                {
                    await showEncounterScreen();
                }
            });
            vocabularyButton.onValueChanged.AddListener(async isChecked =>
            {
                if (isChecked)
                {
                    await showVocabularyScreen();
                }
            });
        }

        public async UniTask showEncounterScreen()
        {
            await switchScreens(screenPosition.transform.position, rightHidePosition.transform.position);
        }
        
        public async UniTask showVocabularyScreen()
        {
            await switchScreens(leftHidePosition.transform.position, screenPosition.transform.position);
        }

        private async Task switchScreens(Vector3 newEncounterPosition, Vector3 newVocabularyPosition)
        {
            await UniTask.WhenAll(
                encoutnerHolder.transform.DOMove(newEncounterPosition, screenSwitchDuration).ToUniTask(),
                vocabularyHolder.transform.DOMove(newVocabularyPosition, screenSwitchDuration)
                    .ToUniTask()
            );
        }
        
        public async UniTask changeBatteryLevel(int from, int to)
        {
            batteryLevel.text = to + "%";
        } 

        public async UniTask changeStressLevel(int from, int to)
        {
            stressLevel.updateStress(to);
        }
    }
}