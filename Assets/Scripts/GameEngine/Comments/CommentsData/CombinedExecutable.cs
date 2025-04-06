using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class CombinedExecutable : Executable
    {
        private Executable[] combination;

        public CombinedExecutable(params Executable[] combination)
        {
            this.combination = combination;
        }

        public async UniTask execute()
        {
            foreach (var exec in combination)
            {
                await exec.execute();
            }
        }
    }
}