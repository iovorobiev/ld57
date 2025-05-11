using System;
using System.Collections.Generic;
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
            tutorialText.Add("Stressful content will increase your stress by the amount of likes if you just swipe away");
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
            tutorialText.Add("This is how many likes you need to get. Bring it to 0 (or lower)");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });

            
            tutorialRects.Add(new Rect(new Vector2(0f, -1f), new Vector2(10f, 4f)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("These are all the comments available to you. Choose and post a couple.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(3.5f, 8.5f), new Vector2(2.5f, 1)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("Posting the comment reduced your battery. If your phone is discharged you lose. But you can start again.");
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
            tutorialText.Add("You can only post a few comments under each post. Check which ones you have left by switching to memory tab. Click it.");
            listOfAwaitables.Add(async () => {});
            
            // tutorialRects.Add(new Rect(new Vector2(-2.5f, -8f), new Vector2(3.5f, 2)));
            // arrowPosition.Add(ArrowState.ABOVE);
            // tutorialText.Add("Click Doom Scrolling button in order to get back to posting whenever you are ready");
            // listOfAwaitables.Add(UniTask.CompletedTask);
            
            tutorialRects.Add(new Rect(new Vector2(0f, 0f), new Vector2(10f, 18)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("Once you get enough likes, you reduce you stress by the amount of likes your comments got. You then can learn more comments to post.");
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

        public enum ArrowState
        {
            ABOVE,
            BELOW,
            HIDDEN,
        }
    }
}