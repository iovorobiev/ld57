using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class BatteryCostExecutable : Executable
    {
        public int batteryCost;

        public BatteryCostExecutable(int batteryCost)
        {
            this.batteryCost = batteryCost;
        }

        public async UniTask execute()
        {
            await Player.receivePowerDamage(batteryCost);
        }
    }
}