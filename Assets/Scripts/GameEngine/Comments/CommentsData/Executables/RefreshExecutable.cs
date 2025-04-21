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
            await Player.receiveStressDamage(getRefreshDmg());
            Player.nextRefresh.Clear();
            Debug.Log("Finish refresh");
        }

        private static int getRefreshDmg()
        {
            return Mathf.Max(2 - Player.upgrades.FindAll(up => up.upgradeID == OSUpgradesBase.BETTER_REFRESH).Count, 1);
        }

        public string getPrice(Executable.Resource resource)
        {
            if (resource == Executable.Resource.Stress)
            {
                return getRefreshDmg().ToString();
            }

            return null;
        }
    }
}