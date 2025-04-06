using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class HaExecutable : Executable
    {
        public async UniTask execute()
        {
            Player.currentHaCount++;
        }
    }
}