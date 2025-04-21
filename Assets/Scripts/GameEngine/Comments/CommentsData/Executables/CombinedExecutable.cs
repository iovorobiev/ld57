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

        public string getPrice(Executable.Resource r)
        {
            foreach (var ex in combination)
            {
                var price = ex.getPrice(r);
                if (price != null)
                {
                    return price;
                }
            }

            return null;
        }
    }
}