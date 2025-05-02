using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData.Executables
{
    public class BumpExecutable : Executable
    {
        public async UniTask execute()
        {
            foreach (var postedComment in Player.postedComments)
            {
                postedComment.currentLikes++;
            }
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }
    }
}