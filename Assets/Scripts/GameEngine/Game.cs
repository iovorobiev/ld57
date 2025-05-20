using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameAnalyticsSDK;
using GameEngine.Encounters;
using GameEngine.Tutorial;
using GameEngine.ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameEngine
{
    public class Game
    {
        public static Encounter currentEncounter;
        public static EncountersPresenter encountersPresenter;
        public static EncounterController currentEncounterController;
        public static Keyboard keyboard;
        public static TurnOffSequence turnOffSequence;
        public static VocabularyView vocabularyView;
        public static ScreenController screenController;

        public static TutorialView tutorialView;
        public static int currentDepth = 0;

        public static int currentRun;

        public static async UniTask GameLoop()
        {
            currentDepth = 0;
            var tutorialExecutable = new TutorialSequence();
            var tutorialDeck = new TutorialEncounterDeck(tutorialExecutable);
            var deck = new InfiniteEncountersDeck();
            Player.reset();
            turnOffSequence.resetValues();
            if (currentRun == 0)
            {
                deck.initDeck(tutorialDeck);
                await encountersPresenter.init(tutorialDeck.getCurrentEncounter(), tutorialDeck.getNextEncounter());   
            }
            else
            {
                deck.initDeck(null);
                await encountersPresenter.init(tutorialDeck.getCurrentEncounter(), deck.getNextEncounter());
            }
            
            tutorialDeck.changePage();
            screenController.startListeningButtons();
            tutorialView.hide();
            
            while (!Player.winCondition()  && !Player.loseCondition())
            {
                currentDepth++;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, currentDepth.ToString());
                Player.prepareEncounter(tutorialExecutable.isInProgress());
                encountersPresenter.closeKeyboard();
                keyboard.clearHand();

                await encountersPresenter.presentEcnounter();
                await encountersPresenter.changeEncounter(deck.getNextEncounter());
            }

            if (Player.winCondition())
            {
                await winSequence();
            }
            else
            {
                await loseSequence();
            }

            currentRun++;
            await SceneManager.LoadSceneAsync(0).ToUniTask();
        }

        private static async UniTask loseSequence()
        {
            turnOffSequence.gameObject.SetActive(true);
            if (Player.powerLevel <= 0)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "loss_power", currentDepth);
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Run",currentRun.ToString(), "stress");
                await turnOffSequence.doBatteryTurnOffSequence();
            } else if (Player.stressLevel >= 100)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "loss_stress", currentDepth);
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Run",currentRun.ToString(), "battery");
                await turnOffSequence.doStressTurnOff();
            }
            
        }

        private static async UniTask winSequence()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "win", currentRun);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "win_depth", currentDepth);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Run",currentRun.ToString());
            await turnOffSequence.doWinSequence();
        }
    }
}