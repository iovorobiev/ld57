using Cysharp.Threading.Tasks;
using GameEngine.Comments;

namespace GameEngine.Encounters
{
    public interface EncounterExecutable : Executable
    {
        public UniTask setEncounterController(EncounterController controller);
    }
}