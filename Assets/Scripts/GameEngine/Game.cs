using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

        public static async UniTask GameLoop()
        {
            currentDepth = 0;
            var tutorialExecutable = new TutorialSequence();
            var tutorialDeck = new TutorialEncounterDeck(tutorialExecutable);
            var deck = new InfiniteEncountersDeck();
            deck.initDeck(tutorialDeck);
            Player.reset();
            turnOffSequence.resetValues();
            await encountersPresenter.init(tutorialDeck.getCurrentEncounter(), tutorialDeck.getNextEncounter());
            tutorialDeck.changePage();
            screenController.startListeningButtons();
            tutorialView.hide();
            
            while (!Player.winCondition()  && !Player.loseCondition())
            {
                currentDepth++;
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

            await SceneManager.LoadSceneAsync(0).ToUniTask();
        }

        private static async UniTask loseSequence()
        {
            turnOffSequence.gameObject.SetActive(true);
            if (Player.powerLevel <= 0)
            {
                await turnOffSequence.doBatteryTurnOffSequence();
            } else if (Player.stressLevel >= 100)
            {
                await turnOffSequence.doStressTurnOff();
            }
            
        }

        private static async UniTask winSequence()
        {
            await turnOffSequence.doWinSequence();
        }
    }
}