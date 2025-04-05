using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine
{
    public class Game
    {
        public static Encounter currentEncounter;
        public static EncountersPresenter encountersPresenter;
        public static UI ui;
        public static Keyboard keyboard;
        public static CommentView commentView;

        public static async UniTask GameLoop()
        {
            // while (!Player.winCondition())
            // {
            while (!Player.winCondition() && !Player.loseCondition())
            {
                    Debug.Log("Presenting encounter");
                    await encountersPresenter.presentEcnounter();
                    Debug.Log("Changing encounter");
                    await encountersPresenter.changeEncounter();
            }
            //     if (Player.loseCondition())
            //     {
            //         Debug.Log("Awaiting lose");
            //
            //         await loseSequence();
            //     }    
            // }
            //
            // Debug.Log("Awaiting win");
            // await winSequence();

        }

        private static async UniTask loseSequence()
        {
            
        }

        private static async UniTask winSequence()
        {
            
        }
    }
}