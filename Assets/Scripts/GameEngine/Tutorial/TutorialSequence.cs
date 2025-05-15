using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using Unity.VisualScripting;
using UnityEngine;

namespace GameEngine.Tutorial
{
    public class TutorialSequence
    {
        private List<Rect> tutorialRects = new();
        private List<ArrowState> arrowPosition = new();
        private List<string> tutorialText = new();
        private List<Func<UniTask>> listOfAwaitables = new();
        
        private int currentTip = 0;

        public TutorialSequence()
        {
            tutorialRects.Add(new Rect(new Vector2(0.25f, 8.3f), new Vector2(2.7f, 1)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("To finish Doom Scrolling reach stress level 0%");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(-1.5f, -5.5f), new Vector2(5.5f, 3)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("Stressful content will increase your stress. More likes - more stress.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 0f), new Vector2(10f, 18)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("Click anywhere to swipe and try");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 2.5f), new Vector2(10f, 14f)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("You won't get stress if your comments get more likes than the post.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(3f, -5.5f), new Vector2(2.5f, 3)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("Click to open comments section.");
            listOfAwaitables.Add(async () => { });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 6.5f), new Vector2(2f, 2f)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("This is how many likes you need to get. Bring it to 0");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });

            
            tutorialRects.Add(new Rect(new Vector2(0f, -1f), new Vector2(10f, 4f)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("Your keyboard suggests you comments to use. No need to type anything. Click on the comment to post it.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(3.5f, 8.5f), new Vector2(2.5f, 1)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("Posting the comment reduced your battery. You lose if you run out of power, before you finished doomscrolling.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(1.45f, -5.2f), new Vector2(3.5f, 2)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("Refresh button will fetch comments until you have 4 to choose from. It will add 2% of stress. Click it.");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(3.1f, -8f), new Vector2(3.5f, 2)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("You can only post as many comments as your keyboard allows. Switch to keyboard memory tab to see which comments you have left.");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(-3.5f, -6.5f), new Vector2(1.5f, 1f)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("You can reopen your keyboard by clicking this button. After that you can hide your keyboard using the same button. Handy to view posted comments");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(0f, 0f), new Vector2(10f, 18)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("Great job on getting many likes. Your stress will be reduced by the amount of likes you got. You also get a reward.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
        }
        
        public async UniTask showNextTip()
        {
            if (currentTip >= tutorialRects.Count)
            {
                Game.tutorialView.hide();
                return;
            }
            Game.tutorialView.showAt(tutorialRects[currentTip], arrowPosition[currentTip], tutorialText[currentTip]);
            await listOfAwaitables[currentTip]();
            currentTip++;
        }

        public bool isInProgress()
        {
            return currentTip < tutorialRects.Count;
        }

        public enum ArrowState
        {
            ABOVE,
            BELOW,
            HIDDEN,
        }
    }
}