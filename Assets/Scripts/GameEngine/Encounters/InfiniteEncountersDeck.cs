using System;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK;
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
        private List<String> enemyPrefabs = new();
        private List<String> enemyAudio = new();

        private string prefabsPath = "Prefabs/Enemy/";
        private string audioPath = "Audio/";

        private TutorialEncounterDeck deck;
        private bool tutorialPassed;
        private int prevEnemyData = -1;
        
        public void initDeck(TutorialEncounterDeck deck)
        {
            this.deck = deck;
            
            levels = new List<Vector2Int>();
            levels.Add(new Vector2Int(5, 8));
            levels.Add(new Vector2Int(8, 11));
            levels.Add(new Vector2Int(11, 16));
            levels.Add(new Vector2Int(16, 22));
            levels.Add(new Vector2Int(22, 30));

            enemyPrefabs = new();
            enemyPrefabs.Add(prefabsPath +  "Knight");
            enemyPrefabs.Add(prefabsPath +  "Clock");
            enemyPrefabs.Add(prefabsPath +  "Dress");
            enemyPrefabs.Add(prefabsPath +  "Gnome");
            enemyPrefabs.Add(prefabsPath +  "RIP");
            enemyPrefabs.Add(prefabsPath +  "troll");

            enemyAudio = new();
            enemyAudio.Add(audioPath + "Knight");
            enemyAudio.Add(audioPath + "Clock");
            enemyAudio.Add(null);
            enemyAudio.Add(audioPath + "Knight");
            enemyAudio.Add(audioPath + "RIP");
            enemyAudio.Add(audioPath + "Troll");
        }
        
        public Encounter getCurrentEncounter()
        {
            return getRandomEncounter();
        }
        
        public Encounter getNextEncounter()
        {
            if (deck != null && deck?.isEmpty() != true)
            {
                deck?.changePage();
                return deck?.getCurrentEncounter();
            }

            if (!tutorialPassed || Game.currentRun == 0)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "tutorial");
                tutorialPassed = true;
            }
            return getRandomEncounter();
        }

        private Encounter getRandomEncounter()
        {
            if ((Game.currentDepth + 1) % 5 == 0)
            {
                return createRelaxEncounter();
            }

            var level = levels[Math.Min(Game.currentDepth / 5, levels.Count - 1)];
            var positiveProb = Random.Range(0, 100);
            return positiveProb < 20 ? createMeme(level.x / 2, level.y / 2) : createRandomEncounter(level.x, level.y, "Prefabs/Enemy/Knight");
        }

        private Encounter createMeme(int minLikes, int maxLikes)
        {
            int likes = Random.Range(minLikes, maxLikes + 1);
            return new Encounter(
                likes,
                new[] { Tags.Meme, Tags.NO_COMMENTS }.ToList(),
                "Prefabs/Memes/MemeEncounter",
                null,
                new MemeEncounterExecutable(likes),
                new TextPrefabData("Prefabs/Memes/Cat_0", "")
            );
        }

        private Encounter createRandomEncounter(int minLikes, int maxLikes, string prefabPath)
        {
            var likes = Random.Range(minLikes, maxLikes + 1);
            var enemyData = -1;
            do
            {
                enemyData = Random.Range(0, enemyPrefabs.Count);
            } while (enemyData == prevEnemyData);

            prevEnemyData = enemyData;
            return new Encounter(
                likes,
                new[] { Tags.Stressful }.ToList(),
                "Prefabs/Enemy/EnemyEncounter",
                enemyAudio[enemyData],
                new EnemyExecutable(likes),
                new TextPrefabData(enemyPrefabs[enemyData], ""));
        }

        private Encounter createRelaxEncounter()
        {
            return new Encounter(
                0,
                new [] { Tags.NO_COMMENTS }.ToList(),
                "Prefabs/Enemy/RewardEncounter",
                null,
                new RelaxExecutable(10, 10),
                null);
        }

        public bool isEmpty()
        {
            return false;
        }
    }
}