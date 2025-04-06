using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameEngine.Encounters;
using UnityEngine;

namespace GameEngine
{
    public class Game
    {
        public static Encounter currentEncounter;
        public static EncountersPresenter encountersPresenter;
        public static EncounterController currentEncounterController;
        public static UI ui;
        public static Keyboard keyboard;
        public static CommentView commentView;
        public static TurnOffSequence turnOffSequence;
        public static VocabularyView vocabularyView;
        public static Hint hint;
        public static int currentDepth = 0;

        public static async UniTask GameLoop()
        {
            var tutorialDeck = new TutorialEncounterDeck();
            var deck = new InfiniteEncountersDeck();
            deck.initDeck();
            Player.reset();
            await encountersPresenter.init(tutorialDeck.getCurrentEncounter(), tutorialDeck.getNextEncounter());
            while (!tutorialDeck.isEmpty())
            {
                Debug.Log("Tutorial loop");
                tutorialDeck.changePage();
                Player.prepareEncounter();
                encountersPresenter.closeKeyboard();
                keyboard.clearHand();
                await encountersPresenter.presentEcnounter();
                if (tutorialDeck.isEmpty())
                {
                    Debug.Log("Changing from deck");
                    await encountersPresenter.changeEncounter(deck.getNextEncounter());
                }
                else
                {
                    await encountersPresenter.changeEncounter(tutorialDeck.getNextEncounter());
                }
            }
            
            while (!Player.winCondition())
            {
                Player.reset();
                // await encountersPresenter.init(deck.getCurrentEncounter(), deck.getNextEncounter());
                while (!Player.winCondition() && !Player.loseCondition())
                {
                    currentDepth++;
                    Player.prepareEncounter();
                    ui.comments.gameObject.SetActive(true);
                    encountersPresenter.closeKeyboard();
                    keyboard.clearHand();
                    Debug.Log("Presenting encounter");

                    await encountersPresenter.presentEcnounter();
                    await encountersPresenter.changeEncounter(deck.getNextEncounter());
                }

                if (Player.loseCondition())
                {
                    await loseSequence();
                    deck.initDeck();
                }
            }

            Debug.Log("Awaiting win");
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