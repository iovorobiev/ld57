using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameEngine
{
    public class EncountersPresenter : MonoBehaviour
    {
        private static int SWIPE_COST = 1;
        private EncountersDeck deck = new();
        public GameObject encounterView;
        public GameObject nextEncounterView;

        private Encounter currentEncounter;
        private Encounter nextEncounter;

        private Vector3 encounterPosition;
        private Vector3 nextEncounterPosition;

        private void Start()
        {
            encounterPosition = encounterView.transform.position;
            nextEncounterPosition = nextEncounterView.transform.position;
            Game.currentEncounter = deck.getCurrentEncounter();
            nextEncounter = deck.getNextEncounter();
            InflateEncounter(Game.currentEncounter, encounterView);
            InflateEncounter(nextEncounter, nextEncounterView);
        }

        private async void Update()
        {
            await detectClick();
        }

        async UniTask detectClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                await UniTask.WaitForSeconds(0.1f);
                await OnClick();
            }
        }

        async UniTask OnClick()
        {
            await swipeEncounter();
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
            var nextEncounterObject = Instantiate(prefab, view.transform);
        }
    }
}