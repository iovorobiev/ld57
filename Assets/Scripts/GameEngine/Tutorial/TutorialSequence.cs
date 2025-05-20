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
            tutorialText.Add("To finish <wave><palette colors=#C1D6AC;#679E85>Doom Scrolling</></> reach stress level 0%");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(-1.5f, -5.5f), new Vector2(5.5f, 3)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("<shake>Stressful</> content will increase your stress <sprite=23>. More <sprite=0> - more <sprite=23>.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 2.5f), new Vector2(10f, 14f)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("<wave>Click</> anywhere to swipe and try");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 2.5f), new Vector2(10f, 14f)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("You won't get <sprite=23> if your comments get more <sprite=0> than the <wave>post</>.");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(3f, -5.5f), new Vector2(2.5f, 3)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("<wave>Click</> to open comments section.");
            listOfAwaitables.Add(async () => { });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 6.5f), new Vector2(2f, 2f)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("This is how many <sprite=0> left to earn for you.<br> <wave>Bring it to 0.</>");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });

            
            tutorialRects.Add(new Rect(new Vector2(0f, -1f), new Vector2(10f, 4f)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("Your keyboard suggests you <wave>comments</> to use. No need to type anything.<br> <wave>Click</> on the comment to post it.");
            listOfAwaitables.Add(async () =>
            {
                // await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 2.5f), new Vector2(10f, 18f)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("");
            listOfAwaitables.Add(async () =>
            {
               
            });
            
            tutorialRects.Add(new Rect(new Vector2(3.5f, 8.5f), new Vector2(2.5f, 1)));
            arrowPosition.Add(ArrowState.BELOW);
            tutorialText.Add("Posting the comment reduced your <sprite=3>.<br> You <wave><palette colors=#82679e;#679E85>LOSE</></> if you run out of <sprite=3> before you finished <wave><palette colors=#C1D6AC;#679E85>Doom Scrolling.</>");
            listOfAwaitables.Add(async () =>
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            });
            
            tutorialRects.Add(new Rect(new Vector2(0f, 2.5f), new Vector2(10f, 18f)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("");
            listOfAwaitables.Add(async () =>
            {
               
            });
            
            tutorialRects.Add(new Rect(new Vector2(1.45f, -5.2f), new Vector2(3.5f, 2)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("<wave>Refresh</> button will fetch comments until you have 4 to choose from.<br> It will add 2% of <sprite=23>. <br> <wave>Click</> it.");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(3.1f, -8f), new Vector2(3.5f, 2)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("You can only post as many comments as your deck has.<br> Switch to <wave>comments deck</> tab to see which comments you have left.");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(-3f, -8f), new Vector2(3.5f, 2)));
            arrowPosition.Add(ArrowState.JUST_ARROW);
            tutorialText.Add("");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(-3.5f, -6.5f), new Vector2(1.5f, 1f)));
            arrowPosition.Add(ArrowState.ABOVE);
            tutorialText.Add("You can <wave>reopen</> your keyboard by clicking this button.<br> After that you can hide your keyboard using the same button.<br> Handy to view <wave>posted comments</>");
            listOfAwaitables.Add(async () => {});
            
            tutorialRects.Add(new Rect(new Vector2(0f, 0f), new Vector2(10f, 18)));
            arrowPosition.Add(ArrowState.HIDDEN);
            tutorialText.Add("Great job on getting <wave>many</> <sprite=0>. Your <sprite=23> will be reduced by the amount of <sprite=0> you got.<br> You also get a <wave><palette colors=#C1D6AC;#679E85>reward</></>.");
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
            JUST_ARROW,
            HIDDEN,
        }
    }
}