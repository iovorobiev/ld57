using Cysharp.Threading.Tasks;

namespace GameEngine
{
    public class Player
    {
        public static int stressLevel = 100;
        public static int powerLevel = 20;

        public static async UniTask receivePowerDamage(int dmg)
        {
            int from = powerLevel;
            powerLevel -= dmg;
            await Game.ui.changeBatteryLevel(from, powerLevel);
        } 
    }
}