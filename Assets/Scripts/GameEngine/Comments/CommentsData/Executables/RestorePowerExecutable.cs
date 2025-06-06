using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class RestorePowerExecutable : Executable
    {
        private int restore;

        public RestorePowerExecutable(int restore)
        {
            this.restore = restore;
        }

        public async UniTask execute()
        {
            await Player.receivePowerDamage(-restore);
        }

        public string getPrice(Executable.Resource resource)
        {
            return resource == Executable.Resource.Battery ? "+" + restore + "%" : null;
        }
    }
}