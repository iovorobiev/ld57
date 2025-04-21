using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Comments.CommentsData
{
    public class CommentsRandom
    {
        public static int nextInt(int minInclusive, int maxInclusive)
        {
            if (Player.hasTempEffect(TempEffect.MAX_RANDOM))
            {
                return maxInclusive;
            }

            return Random.Range(minInclusive, maxInclusive + 1);
        }

        public static string getLikesFor(int minInclusive, int maxInclusive, List<Tags> tags = null)
        {
            return Player.calculateLikesWithBonuses(minInclusive, tags) + "-" +
                   Player.calculateLikesWithBonuses(maxInclusive, tags);
        }
    }
}