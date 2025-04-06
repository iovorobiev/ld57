using System.Collections.Generic;
using GameEngine.EncounterData;
using GameEngine.Encounters;
using GameEngine.Encounters.EncounterData;
using UnityEngine;

namespace GameEngine
{
    public class InfiniteEncountersDeck : EncountersDeck
    {
        private List<Encounter> enemyEncounters = new();
        private List<Encounter> memeEncounters = new();
        
        public void initDeck()
        {
            var likes = Random.Range(5, 10);
            memeEncounters.Add(
                new MemeEncounter(
                    likes, 
                    new MemeData("Prefabs/Memes/" + "Cat_0", "Cat videos always releave stress"), 
                    new MemeEncounterExecutable(likes)
                    )
                );
            memeEncounters.Add(
                new MemeEncounter(
                    likes, 
                    new MemeData("Prefabs/Memes/" + "fire_0", "Rest near the fire. Releave some stress"), 
                    new MemeEncounterExecutable(likes)
                )
            );
            
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Clock")
                );
            
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Dress")
            );
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Gnome")
            );
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Knight")
            );
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "RIP")
            );
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Troll", blocking: true)
            );
            enemyEncounters.Add(
                new EnemyEncounter(Random.Range(3,5), enemyPrefab: EnemyEncounter.ENEMY_PATH + "Screamer", blocking: true)
            );
        }
        
        public Encounter getCurrentEncounter()
        {
            return new EnemyEncounter(Random.Range(3,5));
        }
        
        public Encounter getNextEncounter()
        {
            var prob = Random.Range(0f, 1f);
            if (prob < 0.75)
            {
                return enemyEncounters[Random.Range(0, enemyEncounters.Count - 1)];
            }
            else
            {
                return memeEncounters[Random.Range(0, memeEncounters.Count - 1)];
            }
            
        }

        public bool isEmpty()
        {
            return false;
        }
    }
}