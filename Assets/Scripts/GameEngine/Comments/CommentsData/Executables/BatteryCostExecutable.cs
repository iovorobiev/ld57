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
            if (!Player.hasTempEffect(TempEffect.NEXT_NO_BATTERY))
            {
                await Player.receivePowerDamage(batteryCost);    
            }
        }

        public string getPrice(Executable.Resource r)
        {
            if (Player.hasTempEffect(TempEffect.NEXT_NO_BATTERY))
            {
                return "0%";
            }
            return r == Executable.Resource.Battery? "-" + batteryCost + "%" : null;
        }
    }
}