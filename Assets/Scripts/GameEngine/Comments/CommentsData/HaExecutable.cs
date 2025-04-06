using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class HaExecutable : Executable
    {
        private int haIndex;

        public HaExecutable(int haIndex)
        {
            this.haIndex = haIndex;
        }

        public async UniTask execute()
        {
            Player.currentHaCount++;
            var ha = CommentsBase.getAllComments()[haIndex];
            Player.currentEncounterDeck.Enqueue(ha);
            Game.vocabularyView.addComment(ha);
        }
    }
}