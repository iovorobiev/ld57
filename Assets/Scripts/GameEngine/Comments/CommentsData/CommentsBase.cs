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
                new Comment("WTF??", "WTF will definitely get 1 like", () => 1, LikeInteractionType.REDUCE_STRESS,
                    0,
                    new EmptyExecutable()));
            allComments.Add(new Comment(
                "Refresh",
                "It is extremely stressful that I need to find words!",
                () => 0,
                LikeInteractionType.SKIP_TURN,
                -1,
                new RefreshExecutable()));

            allComments.Add(new Comment(
                "ha",
                "Ha stays in vocabulary after use. 1 like, 1 power",
                () => 1,
                LikeInteractionType.REDUCE_STRESS,
                1,
                new BatteryCostExecutable(1)
            ));
            allComments.Add(new Comment(
                    "haha",
                    "Haha stays in vocabulary after use. 2 likes, 2 power",
                    () => 2,
                    LikeInteractionType.REDUCE_STRESS,
                    1,
                    new CombinedExecutable(
                        new BatteryCostExecutable(2),
                        new HaExecutable()
                    )
                )
            );
            allComments.Add(new Comment(
                "lol",
                "lol gets 1 like per ha commented. Costs 3 power",
                () => Player.currentHaCount,
                LikeInteractionType.REDUCE_STRESS,
                1,
                new BatteryCostExecutable(3))
            );
            
            allComments.Add(new Comment(
                "#58008",
                "Machine code! Will charge 1 power",
                () => 0,
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new RestorePowerExecutable(1))
            );
            
            allComments.Add(new Comment(
                "#58008_918",
                "Machine code! Will charge 2 power!",
                () => 0,
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new RestorePowerExecutable(2))
            );
            
            allComments.Add(new Comment(
                "0xdeadbeef",
                "Gets 1 like per 1 power spent on this post. Costs 2 power",
                () => Player.batteryUnderPost,
                LikeInteractionType.REDUCE_STRESS,
                1,
                new BatteryCostExecutable(2))
            );
            
            allComments.Add(new Comment(
                "Let me think...",
                "Will add 1 comment to the keyboard for 1 power",
                () => Player.batteryUnderPost,
                LikeInteractionType.NO_EFFECT,
                1,
                new CombinedExecutable(
                    new BatteryCostExecutable(2),
                    new GetCommentExecutable(1)
            )));
            return allComments;
        }

        public static List<Comment> rollComments(int number)
        {
            var result = new List<Comment>();
            for (int i = 0; i < number; i++)
            {
                int maxRarity = 1;
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
            return 1;
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