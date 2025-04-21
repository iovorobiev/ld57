using Cysharp.Threading.Tasks;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine.Comments.CommentsData
{
    public class BatteryCostExecutable : Executable
    {
        public int batteryCost;

        public BatteryCostExecutable(int batteryCost)
        {
            this.batteryCost = batteryCost - Player.upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.BAT_ARMOR).Count;
            this.batteryCost = Mathf.Max(this.batteryCost, 1);
        }

        public async UniTask execute()
        {
            await Player.receivePowerDamage(batteryCost);
        }

        public string getPrice(Executable.Resource r)
        {
            return r == Executable.Resource.Battery? "-" + batteryCost + "%" : null;
        }
    }
}