using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class EmptyExecutable : Executable
    {
        public async UniTask execute()
        {
            // Do nothing
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }
    }
}