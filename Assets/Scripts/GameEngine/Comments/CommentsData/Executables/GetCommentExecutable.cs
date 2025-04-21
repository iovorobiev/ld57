using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class GetCommentExecutable : Executable
    {
        private int commentsToDraw;

        public GetCommentExecutable(int commentsToDraw)
        {
            this.commentsToDraw = commentsToDraw;
        }

        public async UniTask execute()
        {
            for (int i = 0; i < commentsToDraw; i++)
            {
                await Game.keyboard.draw();
            }
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }
    }
}