using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class TitleExecutable : EncounterExecutable
    {
        public async UniTask execute()
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            await Game.screenController.showUi();
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            // Do nothing
        }
    }
}