using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Comments
{
    public class RefreshExecutable : Executable
    {
        public async UniTask execute()
        {
            Debug.Log("Executing refresh");
            await Game.keyboard.clearHand();
            await Game.keyboard.OnShow();
            Debug.Log("Finish refresh");
        }
    }
}