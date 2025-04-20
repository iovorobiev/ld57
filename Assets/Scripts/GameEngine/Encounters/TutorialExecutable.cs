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

        public TutorialExecutable()
        {
        }

        public virtual async UniTask execute()
        {
            if (hintText != "")
            {
                await UniTask.WaitForSeconds(1f);
            }

            if (!keepComments)
            {
                // Game.ui.comments.gameObject.SetActive(false);
            }
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
        }
    }
}