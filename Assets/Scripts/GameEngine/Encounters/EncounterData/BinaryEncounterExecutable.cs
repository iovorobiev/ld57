using Cysharp.Threading.Tasks;

namespace GameEngine.Encounters.EncounterData
{
    public class BinaryEncounterExecutable : EncounterExecutable
    {
        private int batteryRestore;

        public BinaryEncounterExecutable(int batteryRestore)
        {
            this.batteryRestore = batteryRestore;
        }

        public async UniTask execute()
        {
            Player.receivePowerDamage(-batteryRestore);
        }

        public UniTask setEncounterController(EncounterController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}