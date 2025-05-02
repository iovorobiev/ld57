using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData.Executables
{
    public class StealExecutable : Executable
    {
        public async UniTask execute()
        {
            if (Player.postedComments.Count < 2)
            {
                return;
            }
            
            var lastComment= Player.postedComments[Player.postedComments.Count - 2];
            if (lastComment.currentLikes == 0)
            {
                return;
            }

            lastComment.currentLikes--;
            Player.postedComments.Last().currentLikes = 1;
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }
    }
}