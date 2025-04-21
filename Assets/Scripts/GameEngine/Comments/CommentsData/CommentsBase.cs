using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.OSUpgrades;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine.Comments.CommentsData
{
    public class CommentsBase
    {
        public class CachedRandomLambda
        {
            private int cached = -1;
            private int minInclusive;
            private int maxInclusive;

            public CachedRandomLambda(int minInclusive, int maxInclusive)
            {
                this.minInclusive = minInclusive;
                this.maxInclusive = maxInclusive;
            }

            public int nextInt()
            {
                if (cached == -1)
                {
                    cached = CommentsRandom.nextInt(minInclusive, maxInclusive);
                }

                return cached;
            }
        }

        public static List<Comment> getAllComments()
        {
            var allComments = new List<Comment>();

            // 0
            allComments.Add(
                new Comment(
                    "WTF??",
                    "",
                    LikeInteractionType.REDUCE_STRESS,
                    0,
                    new CombinedExecutable(new RandomDmgExecutable(
                            () => CommentsRandom.nextInt(0, 2),
                            null,
                            () => CommentsRandom.getLikesFor(0,2)
                            ),
                        new BatteryCostExecutable(2))));
            
            // 1
            allComments.Add(new Comment(
                "Refresh",
                "",
                LikeInteractionType.SKIP_TURN,
                -1,
                new RefreshExecutable()));

            // 2
            allComments.Add(new Comment(
                "ha",
                "",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(
                    new BatteryCostExecutable(2), 
                    new HaExecutable(allComments.Count), 
                    new RandomDmgExecutable(() => 1))
            ));
            
            // 3
            allComments.Add(new Comment(
                "haha",
                "",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(
                    new BatteryCostExecutable(4), 
                    new HaExecutable(allComments.Count, 2), 
                    new RandomDmgExecutable(() => 2))
            ));
            
            // 4
            allComments.Add(new Comment(
                "lol",
                "1 like per ha commented",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new RandomDmgExecutable(
                        () => Player.currentHaCount,
                        new [] { Comments.Tags.FINISHER }.ToList()
                    ), new BatteryCostExecutable(3)))
            );
            
            // 5
            allComments.Add(new Comment(
                "#58008",
                "+1 stress",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(1), new RestorePowerExecutable(2)))
            );
            
            // 6
            allComments.Add(new Comment(
                "#58008_918",
                "+2 stress",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(2), new RestorePowerExecutable(4)))
            );

            // 7
            allComments.Add(new Comment(
                "0xdeadbeef",
                "1 like for each power charged",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new RandomDmgExecutable(() => Player.batteryUnderPostGained, new []
                    {
                        Tags.FINISHER
                    }.ToList()), new BatteryCostExecutable(2)))
            );

            // 8
            allComments.Add(new Comment(
                "Let me think...",
                "Draw 2 comment to the keyboard",
                LikeInteractionType.NO_EFFECT,
                1,
                new CombinedExecutable(
                    new BatteryCostExecutable(2),
                    new GetCommentExecutable(2)
                )));
            
            // 9
            allComments.Add(
                new Comment(
                    "Lame",
                    "",
                    LikeInteractionType.REDUCE_STRESS,
                    0,
                    new CombinedExecutable(new RandomDmgExecutable(() => 1),
                        new BatteryCostExecutable(2))));
            
            // 10
            allComments.Add(new Comment(
                "#$?!#",
                "-1 stress",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(-1), new RestorePowerExecutable(2)))
            );
            
            // 11
            allComments.Add(new Comment(
                "#$?!# you",
                "-2 stress",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(-2), new RestorePowerExecutable(4)))
            );
            
            // 12
            allComments.Add(new Comment(
                "RAGE!!",
                "1<sprite=0>. 4<sprite=0> if stress > 50%",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new RandomDmgExecutable(() => Player.stressLevel > 50 ? 4 : 1), new RestorePowerExecutable(5)))
            );
            
            // 13
            allComments.Add(
                new Comment(
                    "OMG",
                    "",
                    LikeInteractionType.REDUCE_STRESS,
                    0,
                    new CombinedExecutable(
                        new RandomDmgExecutable(() => CommentsRandom.nextInt(0, 1), 
                            null,
                            () => CommentsRandom.getLikesFor(0,1)
                            ),
                        new BatteryCostExecutable(1))));
            
            // 14
            allComments.Add(
                new Comment(
                    "OMFG",
                    "",
                    LikeInteractionType.REDUCE_STRESS,
                    0,
                    new CombinedExecutable(new RandomDmgExecutable(() => CommentsRandom.nextInt(1, 3), null,
                            () => CommentsRandom.getLikesFor(1,3)),
                        new BatteryCostExecutable(4))));
            
            // 15
            allComments.Add(new Comment(
                "#58008_110W5",
                "",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new RestorePowerExecutable(1)))
            );
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
                var index = Random.Range(0, allCommentsThisRarity.Count);
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
            result.Add(allComments[9]);
            result.Add(allComments[9]);
            result.Add(allComments[9]);
            result.Add(allComments[5]);
            result.Add(allComments[5]);
            return result;
        }
    }
}