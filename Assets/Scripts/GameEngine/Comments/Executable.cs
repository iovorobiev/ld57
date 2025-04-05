using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments
{
    public interface Executable
    {
        List<Requirement> getRequirements();
        UniTask execute(Comment comment);
    }
}