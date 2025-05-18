using System;
using Cysharp.Threading.Tasks;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine.Comments.CommentsData
{
    public class BatteryCostExecutable : Executable
    {
        public int batteryCost;
        public Func<int> costEval;

        public BatteryCostExecutable(int batteryCost)
        {
            this.batteryCost = adjustCost(batteryCost);
            this.batteryCost = Mathf.Max(this.batteryCost, 1);
        }

        public BatteryCostExecutable(Func<int> costEval)
        {
            this.costEval = costEval;
        }

        public int adjustCost(int cost)
        {
            return cost - Player.upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.BAT_ARMOR).Count;
        }
        
        public async UniTask execute()
        {
            if (costEval != null)
            {
                batteryCost = adjustCost(costEval());
            }
            if (!Player.hasTempEffect(TempEffect.NEXT_NO_BATTERY))
            {
                await Player.receivePowerDamage(batteryCost);    
            }
            else
            {
                Player.removeFromCommentEffect(TempEffect.NEXT_NO_BATTERY);
            }
        }

        public string getPrice(Executable.Resource r)
        {
            
            if (Player.hasTempEffect(TempEffect.NEXT_NO_BATTERY))
            {
                return "0%";
            }

            if (costEval != null)
            {
                batteryCost = adjustCost(costEval());
            }
            
            return r == Executable.Resource.Battery? "-" + batteryCost + "%" : null;
        }
    }
}