using System.Collections.Generic;
using System.Linq;
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
                new Encounter(
                    likes,
                    new[] {Tags.Meme}.ToList(),
                    "Prefabs/Memes/MemeEncounter",
                    new MemeEncounterExecutable(likes),
                    new TextPrefabData("Prefabs/Memes/" + "Cat_0", "Cat videos always releave stress") 
                    )
                );
            memeEncounters.Add(
                new Encounter(
                    likes,
                    new[] {Tags.Meme}.ToList(),
                    "Prefabs/Memes/MemeEncounter",
                    new MemeEncounterExecutable(likes),
                    new TextPrefabData("Prefabs/Memes/" + "fire_0", "Rest near the fire. Releave some stress")
                )
            );
            
            enemyEncounters.Add(
                new Encounter(
                    5,
                    new[] {Tags.Stressful}.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/Clock", "")
                )
            );
            
            enemyEncounters.Add(
                new Encounter(
                    3,
                    new[] {Tags.Stressful}.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/Dress", "")
                )
            );
            enemyEncounters.Add(
                new Encounter(
                    5,
                    new[] { Tags.Stressful }.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/Gnome", "")
                )
            );
            enemyEncounters.Add(
                new Encounter(
                    5,
                    new[] { Tags.Stressful }.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/Knight", "")
                )
            );
            enemyEncounters.Add(
                new Encounter(
                    8,
                    new[] { Tags.Stressful }.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/RIP", "")
                )
            );
            enemyEncounters.Add(
                    new Encounter(
                        4,
                        new[] { Tags.Stressful, Tags.Blocking }.ToList(),
                        "Prefabs/Enemy/EnemyEncounter",
                        new EnemyExecutable(5),
                        new TextPrefabData("Prefabs/Enemy/Troll", "")
                    )
            );
            enemyEncounters.Add(
                new Encounter(
                    5,
                    new[] { Tags.Stressful, Tags.Blocking }.ToList(),
                    "Prefabs/Enemy/EnemyEncounter",
                    new EnemyExecutable(5),
                    new TextPrefabData("Prefabs/Enemy/Screamer", "")
                )
            );
        }
        
        public Encounter getCurrentEncounter()
        {
            return getRandomEncounter();
        }
        
        public Encounter getNextEncounter()
        {
            return getRandomEncounter();
        }

        private Encounter getRandomEncounter()
        {
            var prob = Random.Range(0f, 1f);
            if (prob < 0.75)
            {
                return enemyEncounters[Random.Range(0, enemyEncounters.Count)];
            }
            else
            {
                return memeEncounters[Random.Range(0, memeEncounters.Count)];
            }
        }

        public bool isEmpty()
        {
            return false;
        }
    }
}