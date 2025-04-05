using Cysharp.Threading.Tasks;

namespace GameEngine.Comments
{
    public class RefreshExecutable : Executable
    {
        public async UniTask execute()
        {
            
            await Game.keyboard.clearHand();
            await Game.keyboard.OnShow();
        }
    }
}