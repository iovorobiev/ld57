using Cysharp.Threading.Tasks;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine.Comments
{
    public class RefreshExecutable : Executable
    {
        public async UniTask execute()
        {
            if (Player.currentEncounterDeck.Count == 0)
            {
                return;
            }
            Debug.Log("Executing refresh");
            await Game.keyboard.clearHand();
            await Game.keyboard.OnShow();
            int refDmg = 5 - Player.upgrades.FindAll(up => up.upgradeID == OSUpgradesBase.BETTER_REFRESH).Count;
            await Player.receiveStressDamage(Mathf.Max(refDmg, 1));
            Debug.Log("Finish refresh");
        }
    }
}