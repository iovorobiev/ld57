using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyEncounter : Encounter
    {
        public static string ENEMY_PATH = "Prefabs/Enemy/";
        
        private int likes = Random.Range(1, 10);
        
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

        public List<Comment> getComments()
        {
            return new List<Comment>();
        }
    }
}