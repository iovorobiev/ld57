using System.Collections.Generic;
using GameEngine.Comments;
using GameEngine.Encounters;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyEncounter : Encounter
    {
        public static string ENEMY_PATH = "Prefabs/Enemy/";
        
        public int likes;
        public string text;
        public bool blocking;
        public string hint;
        public string prefabPath;
        public string enemyPrefabPath;
        private readonly SpecificData data;

        public EnemyEncounter(int likes, SpecificData data = null, string hint = "", string enemyPrefab = "Square",  bool blocking = false)
        {
            this.likes = likes;
            this.blocking = blocking;
            this.hint = hint;
            enemyPrefabPath = enemyPrefab;
            this.data = data;
        }
        
        public string getPrefabAddress()
        {
            return ENEMY_PATH + "EnemyEncounter";
        }

        public int getLikes()
        {
            return likes;
        }

        public LikeInteractionType getLikeInteractionType()
        {
            return LikeInteractionType.INCREASE_STRESS;
        }

        public EncounterExecutable getScript()
        {
            return new EnemyExecutable(this);
        }

        public List<Comment> getComments()
        {
            return new List<Comment>();
        }

        public SpecificData getData()
        {
            return data;
        }

        public bool isBlocking()
        {
            return blocking;
        }
    }
}