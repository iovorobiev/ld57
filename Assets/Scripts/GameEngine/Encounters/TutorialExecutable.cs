using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class TutorialExecutable : EncounterExecutable
    {
        private TutorialEncounterController _encounterController;

        private string mainText;
        private string calltoAction;
        private string hintText;
        private bool keepComments;

        public TutorialExecutable(string mainText, string calltoAction, string hintText, bool keepComments = true)
        {
            this.mainText = mainText;
            this.calltoAction = calltoAction;
            this.hintText = hintText;
            this.keepComments = keepComments;
        }

        public virtual async UniTask execute()
        {
            if (hintText != "")
            {
                await UniTask.WaitForSeconds(1f);
                Game.hint.showHintAndLock(hintText);
                
            }

            if (!keepComments)
            {
                // Game.ui.comments.gameObject.SetActive(false);
            }
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            _encounterController = (TutorialEncounterController) controller;
            await UniTask.WhenAll(
                _encounterController.showMaintext(mainText),
                _encounterController.showCallToAction(calltoAction)
            );
        }
    }
}