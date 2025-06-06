using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Encounters.EncounterData
{
    public class MemeEncounterExecutable : EncounterExecutable
    {
        private int healAmount;

        public MemeEncounterExecutable(int healAmount)
        {
            this.healAmount = healAmount;
        }

        public async UniTask execute()
        {
            await Player.receiveStressDamage(-healAmount);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            // do nothing
        }
    }
}