using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
            while (!Player.winCondition())
            {
                Player.reset();
                while (!Player.winCondition() && !Player.loseCondition())
                {
                    currentDepth++;
                    Player.prepareEncounterDeck();
                    encountersPresenter.closeKeyboard();
                    keyboard.clearHand();
                    await encountersPresenter.presentEcnounter();
                    await encountersPresenter.changeEncounter();
                }

                if (Player.loseCondition())
                {
                    await loseSequence();
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
        }
    }
}