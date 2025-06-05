using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class TutorialEmptyExecutable : EncounterExecutable
    {
        public async UniTask execute()
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            // do nothing
        }
    }
}