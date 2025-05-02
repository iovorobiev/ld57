using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class HaExecutable : Executable
    {
        private int haIndex;
        private readonly int haCount;

        public HaExecutable(int haIndex, int haCount = 1)
        {
            this.haIndex = haIndex;
            this.haCount = haCount;
        }

        public async UniTask execute()
        {
            Player.currentHaCount += haCount;
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }
    }
}