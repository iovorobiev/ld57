using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameEngine.Encounters;
using GameEngine.Tutorial;
using GameEngine.ui;
using UnityEngine;

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
            var tutorialExecutable = new TutorialSequence();
            var tutorialDeck = new TutorialEncounterDeck(tutorialExecutable);
            var deck = new InfiniteEncountersDeck();
            deck.initDeck(tutorialDeck);
            Player.reset();
            await encountersPresenter.init(tutorialDeck.getCurrentEncounter(), tutorialDeck.getNextEncounter());
            tutorialDeck.changePage();
            screenController.startListeningButtons();
            tutorialView.hide();
            while (!Player.winCondition())
            {
                Player.reset();
                while (!Player.winCondition() && !Player.loseCondition())
                {
                    currentDepth++;
                    Player.prepareEncounter(tutorialView.shown);
                    encountersPresenter.closeKeyboard();
                    keyboard.clearHand();

                    await encountersPresenter.presentEcnounter();
                    await encountersPresenter.changeEncounter(deck.getNextEncounter());
                }

                if (Player.loseCondition() && !Player.winCondition())
                {
                    await loseSequence();
                    deck.initDeck(null);
                }
            }

            await winSequence();
        }

        private static async UniTask loseSequence()
        {
            turnOffSequence.gameObject.SetActive(true);
            await turnOffSequence.doTurnOffSequence();
            turnOffSequence.gameObject.SetActive(false);
        }

        private static async UniTask winSequence()
        {
            await turnOffSequence.doWinSequence();
        }
    }
}