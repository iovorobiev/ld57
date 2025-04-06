using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GameEngine.Comments.CommentsData
{
    public class CommentsBase
    {
        public static List<Comment> getAllComments()
        {
            var allComments = new List<Comment>();

            allComments.Add(
                new Comment("WTF??", "WTF will definitely get 1 like", 1, LikeInteractionType.REDUCE_STRESS,
                    0,
                    new EmptyExecutable()));
            allComments.Add(new Comment(
                "Refresh", 
                "It is extremely stressful that I need to find words!", 
                0,
                LikeInteractionType.SKIP_TURN,
                -1,
                new RefreshExecutable()));
            return allComments;
        }

        public static List<Comment> rollComments(int number)
        {
            var result = new List<Comment>();
            for (int i = 0; i < number; i++)
            {
                int maxRarity = 0;
                var rarity = randomRarity();
                var allCommentsThisRarity = getAllComments().FindAll((comment) => rarity == comment.rarity);
                var index = Random.Range(0, allCommentsThisRarity.Count - 1);
                result.Add(allCommentsThisRarity[index]);    
            }

            return result;
        }

        private static int randomRarity()
        {
            var prob = Random.Range(0f, 1f);
            return 0;
        }

        public static List<Comment> getInitialVocabulary()
        {
            var result = new List<Comment>();
            var allComments = getAllComments();
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            return result;
        }
    }
}