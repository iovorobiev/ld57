using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using utils;

namespace GameEngine
{
    public class EncountersPresenter : MonoBehaviour
    {
        private static int SWIPE_COST = 1;
        private EncountersDeck deck = new();
        public GameObject encounterView;
        public GameObject nextEncounterView;

        public GameObject keyboard;
        public bool keyboardOpened = false;

        private Encounter currentEncounter;
        private Encounter nextEncounter;

        private Vector3 encounterPosition;
        private Vector3 nextEncounterPosition;

        private AwaitableClickListener _clickListener = new();
        
        private bool firstEncounter = true;

        private void Awake()
        {
            Game.encountersPresenter = this;
            Player.prepareVocabulary();
        }

        public async UniTask OnSwipe()
        {
            if (keyboardOpened)
            {
                await closeKeyboard();
            }
            else
            {
                Debug.Log("Notifying click");
                _clickListener.notifyClick();
            }
        }

        public async UniTask toggleKeyboard()
        {
            if (keyboardOpened)
            {
                await closeKeyboard();
            }
            else
            {
                await openKeyboard();
            }
        }
        
        public async UniTask openKeyboard()
        {
            Player.prepareEncounterDeck();
            keyboardOpened = true;
            var keyboardHeight = keyboard.GetComponent<Renderer>().bounds.size.y;
            var newPos = new Vector3(keyboard.transform.position.x,
                keyboard.transform.position.y + keyboardHeight, keyboard.transform.position.z);
            await UniTask.WhenAll(
                keyboard.transform.DOMove(newPos, 0.5f).ToUniTask(),
                Game.ui.openKeyboard(keyboardHeight)
            );
            await Game.keyboard.OnShow();
        }
        
        public async UniTask closeKeyboard()
        {
            var newPos = new Vector3(keyboard.transform.position.x,
                keyboard.transform.position.y - keyboard.GetComponent<Renderer>().bounds.size.y, keyboard.transform.position.z);
            await UniTask.WhenAll(
                keyboard.transform.DOMove(newPos, 0.5f).ToUniTask(),
                Game.ui.closeKeyboard()
            );
            keyboardOpened = false;
        }

        async UniTask swipeEncounter()
        {
            await UniTask.WhenAll(
                encounterView.transform.DOMove(Vector3.up * encounterView.GetComponent<Renderer>().bounds.size.y, 0.5f)
                    .ToUniTask(),
                nextEncounterView.transform
                    .DOMove(encounterPosition, 0.5f).ToUniTask());
            Game.currentEncounter = nextEncounter;
            await Player.receivePowerDamage(SWIPE_COST);
            createNextEncounter();
        }

        void createNextEncounter()
        {
            (encounterView, nextEncounterView) = (nextEncounterView, encounterView);
            nextEncounterView.transform.position = nextEncounterPosition;
            nextEncounter = deck.getNextEncounter();
            InflateEncounter(nextEncounter, nextEncounterView);
        }

        private void InflateEncounter(Encounter encounter, GameObject view)
        {
            var prefab = Resources.Load(encounter.getPrefabAddress()) as GameObject;
            Debug.Log("Instantiating enocunter");
            Instantiate(prefab, view.transform);
        }

        public async UniTask changeEncounter()
        {
            await swipeEncounter();
        }

        public async UniTask presentEcnounter()
        {
            Debug.Log("Presenting enocunter");
            if (firstEncounter)
            {
                Debug.Log("First enocunter");
                encounterPosition = encounterView.transform.position;
                nextEncounterPosition = nextEncounterView.transform.position;
                Game.currentEncounter = deck.getCurrentEncounter();
                nextEncounter = deck.getNextEncounter();
                InflateEncounter(Game.currentEncounter, encounterView);
                InflateEncounter(nextEncounter, nextEncounterView);    
            }

            await _clickListener.awaitClick();
            Debug.Log("Click awaited");
        }

        public async UniTask encounterCompleted()
        {
            return;
        }
    }
}