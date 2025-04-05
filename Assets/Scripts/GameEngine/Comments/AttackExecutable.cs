using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments
{
    public class AttackExecutable : Executable
    {
        public List<Requirement> getRequirements()
        {
            return new List<Requirement>();
        }

        public async UniTask execute(Comment comment)
        {
            // Do nothing
        }
    }
}