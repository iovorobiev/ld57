using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine.Comments;
using UnityEngine;
using utils;

namespace GameEngine
{
    public class EncountersPresenter : MonoBehaviour
    {
        private static int SWIPE_COST = 1;
        private InfiniteEncountersDeck deck = new();
        public GameObject encounterView;
        public GameObject nextEncounterView;

        public GameObject keyboard;
        public bool keyboardOpened = false;

        private Encounter currentEncounter;
        private Encounter nextEncounter;

        private EncounterController nextEncounterController;

        private Vector3 encounterPosition;
        private Vector3 nextEncounterPosition;

        private AwaitableClickListener<bool> swipeAwayListener = new();

        private CancellationTokenSource encounterCancellationToken;

        private bool firstEncounter = true;

        private void Awake()
        {
            Game.encountersPresenter = this;
        }

        public async UniTask OnSwipe()
        {
            if (keyboardOpened)
            {
                await closeKeyboard();
            }
            else
            {
                swipeAwayListener.notifyClick(true);
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
            if (keyboardOpened)
            {
                return;
            }
            var keyboardHeight = keyboard.GetComponent<Renderer>().bounds.size.y;
            var newPos = new Vector3(keyboard.transform.position.x,
                keyboard.transform.position.y + keyboardHeight, keyboard.transform.position.z);
            await UniTask.WhenAll(
                keyboard.transform.DOMove(newPos, 0.5f).ToUniTask(),
                Game.ui.openKeyboard(keyboardHeight)
            );
            await Game.keyboard.OnShow();
            keyboardOpened = true;
        }

        public async UniTask closeKeyboard()
        {
            if (!keyboardOpened)
            {
                return;
            }
            keyboardOpened = false;
            var newPos = new Vector3(keyboard.transform.position.x,
                keyboard.transform.position.y - keyboard.GetComponent<Renderer>().bounds.size.y,
                keyboard.transform.position.z);
            await UniTask.WhenAll(
                keyboard.transform.DOMove(newPos, 0.5f).ToUniTask(),
                Game.ui.closeKeyboard()
            );
        }

        async UniTask swipeEncounter(Encounter next)
        {
            await UniTask.WhenAll(
                encounterView.transform.DOMove(Vector3.up * encounterView.GetComponent<Renderer>().bounds.size.y, 0.5f)
                    .ToUniTask(),
                nextEncounterView.transform
                    .DOMove(encounterPosition, 0.5f).ToUniTask());
            Game.currentEncounter = nextEncounter;
            await Player.receivePowerDamage(SWIPE_COST);
            Game.currentEncounterController.destroy();
            Game.currentEncounterController = nextEncounterController;
            Game.commentView = nextEncounterView.gameObject.GetComponentInChildren<CommentView>();
            encounterCancellationToken.Cancel();
            encounterCancellationToken = new CancellationTokenSource();
            createNextEncounter(next);
        }

        void createNextEncounter(Encounter next)
        {
            Debug.Log("Creating next encounter ");
            (encounterView, nextEncounterView) = (nextEncounterView, encounterView);
            nextEncounterView.transform.position = nextEncounterPosition;
            nextEncounter = next;
            nextEncounterController = InflateEncounter(nextEncounter, nextEncounterView);
            nextEncounterView.GetComponentInChildren<CommentView>().clearComments();
        }

        private EncounterController InflateEncounter(Encounter encounter, GameObject view)
        {
            Debug.Log("Encounter is " +encounter);
            var prefab = Resources.Load(encounter.getPrefabAddress()) as GameObject;
            var gameObject = Instantiate(prefab, view.transform);
            var encounterController = gameObject.GetComponent<EncounterController>();
            encounterController.setEncounterData(encounter);
            return encounterController;
        }

        public async UniTask changeEncounter(Encounter next)
        {
            await swipeEncounter(next);
        }

        public async UniTask init(Encounter current, Encounter next)
        {
            encounterPosition = encounterView.transform.position;
            nextEncounterPosition = nextEncounterView.transform.position;
            Game.currentEncounter = current;
            nextEncounter = next;
            Game.currentEncounterController = InflateEncounter(Game.currentEncounter, encounterView);
            nextEncounterController = InflateEncounter(nextEncounter, nextEncounterView);
            encounterCancellationToken = new CancellationTokenSource();
            firstEncounter = false;
        }

        public async UniTask presentEcnounter()
        {
            if (Game.currentEncounter.isBlocking())
            {
                await Game.currentEncounterController.runExecutable().AttachExternalCancellation(encounterCancellationToken.Token);
            }
            else
            {
                Game.currentEncounterController.runExecutable().AttachExternalCancellation(encounterCancellationToken.Token);
            }
            
            await swipeAwayListener.awaitClick();
            encounterCancellationToken.Cancel();
        }

        public async UniTask encounterCompleted()
        {
            Debug.Log("Encounter completed");
            
        }

        public async UniTask<Comment> playersComment()
        {
            Debug.Log("Waiting keyboard " + keyboardOpened);
            if (!keyboardOpened)
            {
                await UniTask.WaitUntil(() => keyboardOpened, cancellationToken: encounterCancellationToken.Token);
            }
            Debug.Log("Keyboard opened, waiting comment");
            return await keyboard.GetComponent<Keyboard>().awaitComment(encounterCancellationToken.Token);
        }
    }
}