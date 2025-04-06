using System.Collections.Generic;
using GameEngine.Comments;
using GameEngine.Encounters;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyEncounter : Encounter
    {
        public static string ENEMY_PATH = "Prefabs/Enemy/";
        
        private int likes = Random.Range(5, 10);
        
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
            return new EnemyExecutable(getLikes());
        }

        public List<Comment> getComments()
        {
            return new List<Comment>();
        }
    }
}