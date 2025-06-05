using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class TutorialExecutable : EncounterExecutable
    {
        private EncounterController _encounterController;

        private string mainText;
        private string calltoAction;
        private string hintText;
        private bool keepComments;
        private readonly Tutorial.TutorialSequence _tutorialSequence;

        public TutorialExecutable(Tutorial.TutorialSequence tutorialSequence)
        {
            this._tutorialSequence = tutorialSequence;
        }

        public virtual async UniTask execute()
        {
            Game.encountersPresenter.blockSwipe = true;
            // // Describe the goal
            // await _tutorialSequence.showNextTip();
            // // Describe the stress on swipe
            await _tutorialSequence.showNextTip();
            // // Ask to swipe  
            // await UniTask.WaitForSeconds(0.5f);
            await _tutorialSequence.showNextTip();
            Game.encountersPresenter.blockSwipe = false;
            Game.tutorialView.hide();
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
        }
    }
}