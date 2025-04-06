using GameEngine.EncounterData;
using GameEngine.Encounters;
using UnityEngine;

namespace GameEngine
{
    public class InfiniteEncountersDeck : EncountersDeck
    {
        public Encounter getCurrentEncounter()
        {
            return new EnemyEncounter(Random.Range(3,5));
        }
        
        public Encounter getNextEncounter()
        {
            return new EnemyEncounter(Random.Range(3,5));
        }

        public bool isEmpty()
        {
            return false;
        }
    }
}