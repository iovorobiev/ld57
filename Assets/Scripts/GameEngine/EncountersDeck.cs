using GameEngine.EncounterData;

namespace GameEngine
{
    public class EncountersDeck
    {
        
        
        public Encounter getCurrentEncounter()
        {
            return new EnemyEncounter();
        }
        
        public Encounter getNextEncounter()
        {
            return new EnemyEncounter();
        }
    }
}