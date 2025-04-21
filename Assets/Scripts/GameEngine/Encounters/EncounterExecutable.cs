using Cysharp.Threading.Tasks;
using GameEngine.Comments;

namespace GameEngine.Encounters
{
    public interface EncounterExecutable
    {
        UniTask execute();
        public UniTask setEncounterController(EncounterController controller);
    }
}