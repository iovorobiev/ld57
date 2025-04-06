using Cysharp.Threading.Tasks;
using GameEngine.Encounters.EncounterData;

namespace GameEngine.Encounters
{
    public class TutorialEnemyEncounter : TutorialExecutable
    {
        public TutorialEnemyEncounter(string mainText, string calltoAction, string hintText) : base(mainText, calltoAction, hintText)
        {
        }

        public override async UniTask execute()
        {
            await base.execute();
            
            
        }
    }
}