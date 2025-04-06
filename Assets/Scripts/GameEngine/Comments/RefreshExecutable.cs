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
            await Player.receiveStressDamage(5);
            Debug.Log("Finish refresh");
        }
    }
}