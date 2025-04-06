using Cysharp.Threading.Tasks;

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
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            // do nothing
        }
    }
}