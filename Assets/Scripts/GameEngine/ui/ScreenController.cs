using System;
using System.Numerics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
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
        public TextMeshProUGUI stressStatus;

        public TextMeshProUGUI cardsCounter;
        
        public GameObject subtractionPrefab;
        
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
            int diff = to - from;
            batteryLevel.text = to + "%";
            if (diff < 0)
            {
                animateChangeResource(diff, batteryLevel.gameObject);
            }
        }

        private async void animateChangeResource(int diff, GameObject parentObject)
        {
            var subtraction = Instantiate(subtractionPrefab, parentObject.transform);
            subtraction.transform.position = parentObject.transform.position;
            subtraction.GetComponent<TextMeshProUGUI>().text = diff + "%";
            await UniTask.WhenAll(
                subtraction.transform.DOMove(subtraction.transform.position + 1.5f * Vector3.down, 1.5f).ToUniTask(),
                subtraction.GetComponent<TextMeshProUGUI>().DOFade(0f, 1.5f).ToUniTask()
            );
            Destroy(subtraction);

        }

        private void Update()
        {
            cardsCounter.text = Player.currentEncounterDeck.Count + "/" + Player.vocabulary.Count;
        }

        public async UniTask changeStressLevel(int from, int to)
        {
            var diff = to - from;
            if (diff > 0)
            {
                animateChangeResource(diff, stressStatus.gameObject);
            }

            stressStatus.text = to + "%";
            // stressLevel.updateStress(to);
        }
    }
}