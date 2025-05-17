using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine.Comments;
using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;
using UnityEngine;
using utils;

namespace GameEngine
{
    public class EncountersPresenter : MonoBehaviour
    {
        private static int SWIPE_COST = 0;
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
        public bool blockSwipe;

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
            else if (!blockSwipe)
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
            await UniTask.WhenAll(
                Game.keyboard.open(),
                Game.currentEncounterController.ui.openKeyboard(),
                Game.currentEncounterController.commentView.showCommentsView()
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
            await UniTask.WhenAll(
                Game.keyboard.close(),
                Game.currentEncounterController.ui.closeKeyboard(),
                Game.currentEncounterController.commentView.hideCommentsView()
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
            
            if (Player.upgrades.Find((up) => up.upgradeID == OSUpgrades.OSUpgradesBase.EFFECTIVE_BATTERY) == null)
            {
                await Player.receivePowerDamage(SWIPE_COST);
            }
            
            Game.currentEncounterController.destroy();
            Game.currentEncounterController = nextEncounterController;
            
            encounterCancellationToken.Cancel();
            encounterCancellationToken = new CancellationTokenSource();
            
            (encounterView, nextEncounterView) = (nextEncounterView, encounterView);

            createNextEncounter(next);
        }

        void createNextEncounter(Encounter next)
        {
            nextEncounterView.transform.position = nextEncounterPosition;
            nextEncounter = next;
            nextEncounterController = nextEncounterView.GetComponent<EncounterController>();
            nextEncounterController.InflateEncounter(nextEncounter);
            nextEncounterView.GetComponentInChildren<CommentView>().clearComments();
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
            var currentEncounterController = encounterView.GetComponent<EncounterController>();
            currentEncounterController.InflateEncounter(Game.currentEncounter);
            Game.currentEncounterController = currentEncounterController;
            nextEncounterController = nextEncounterView.GetComponent<EncounterController>();
            nextEncounterController.InflateEncounter(nextEncounter);
            encounterCancellationToken = new CancellationTokenSource();
            firstEncounter = false;
        }

        public async UniTask presentEcnounter()
        {

            var tasksList = new List<UniTask>();
            tasksList.Add(UniTask.WaitUntil(() => Player.loseCondition(), cancellationToken: encounterCancellationToken.Token));
            if (Game.currentEncounter.tags.Contains(Tags.Blocking))
            {
                tasksList.Add(UniTask.CompletedTask);
                await Game.currentEncounterController.runExecutable().AttachExternalCancellation(encounterCancellationToken.Token);
            }
            else
            {            
                tasksList.Add(swipeAwayListener.awaitClick());
                tasksList.Add(Game.currentEncounterController.runExecutable().AttachExternalCancellation(encounterCancellationToken.Token));
            }
            
            await UniTask.WhenAny(tasksList);
            if (Game.currentEncounter.tags.Contains(Tags.Stressful))
            {
                if (Game.currentEncounter.executable is not BattleEncounter battleEncounter ||
                    battleEncounter.getCurrentHp() < battleEncounter.getMaxHp())
                {
                    await Player.receiveStressDamage(Game.currentEncounter.likes);
                }
            }
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