using Cysharp.Threading.Tasks;

namespace GameEngine.Encounters.EncounterData
{
    public interface BattleEncounter
    {
        int getMaxHp();
        int getCurrentHp();
        
        
        UniTask receiveDamage(int dmg);
    }
}