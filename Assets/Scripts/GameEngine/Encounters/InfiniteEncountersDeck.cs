using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.EncounterData;
using GameEngine.Encounters;
using GameEngine.Encounters.EncounterData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
    public class InfiniteEncountersDeck : EncountersDeck
    {
        private List<Encounter> enemyEncounters = new();
        private List<Encounter> memeEncounters = new();

        private List<Vector2Int> levels;
        
        public void initDeck()
        {
            levels = new List<Vector2Int>();
            levels.Add(new Vector2Int(5, 8));
            levels.Add(new Vector2Int(8, 11));
            levels.Add(new Vector2Int(11, 16));
            levels.Add(new Vector2Int(16, 22));
            levels.Add(new Vector2Int(22, 30));
            
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
            if ((Game.currentDepth + 1) % 2 == 0)
            {
                return createRelaxEncounter();
            }

            var level = levels[Math.Min(Game.currentDepth / 5, levels.Count - 1)];
            return createRandomEncounter(level.x, level.y, "Prefabs/Enemy/Knight");
        }

        private Encounter createRandomEncounter(int minLikes, int maxLikes, string prefabPath)
        {
            var likes = Random.Range(minLikes, maxLikes + 1);
            return new Encounter(
                likes,
                new[] { Tags.Stressful }.ToList(),
                "Prefabs/Enemy/EnemyEncounter",
                new EnemyExecutable(likes),
                new TextPrefabData(prefabPath, ""));
        }

        private Encounter createRelaxEncounter()
        {
            return new Encounter(
                0,
                new List<Tags>(),
                "Prefabs/Enemy/RewardEncounter",
                new RelaxExecutable(10, 10),
                null);
        }

        public bool isEmpty()
        {
            return false;
        }
    }
}