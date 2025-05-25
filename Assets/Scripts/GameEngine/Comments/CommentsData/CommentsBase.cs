using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Comments.CommentsData.Executables;
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

        public static Comment getRefresh()
        {
            return new Comment(
                "Refresh",
                "",
                LikeInteractionType.SKIP_TURN,
                -1,
                new RefreshExecutable());
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
                            () => CommentsRandom.getLikesFor(0, 2)
                        ),
                        new BatteryCostExecutable(2))));
            
            // 1
            allComments.Add(
                new Comment(
                    "Rage Quit",
                    "1 <sprite=1> per posted comment with 0 likes",
                    LikeInteractionType.REDUCE_STRESS,
                    1,
                        new CombinedExecutable(new RandomDmgExecutable(() =>
                        {
                            return Player.postedComments.FindAll((x) => x.currentLikes == 0).Count;
                        } ), new BatteryCostExecutable(4))
                    )
                );

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
                "1<sprite=1> per ha commented",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new RandomDmgExecutable(
                    () => Player.currentHaCount,
                    new[] { Comments.Tags.FINISHER }.ToList()
                ), new BatteryCostExecutable(3)))
            );

            // 5
            allComments.Add(new Comment(
                "#58008",
                "",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(1), new RestorePowerExecutable(2), new RandomDmgExecutable(() => 0)))
            );

            // 6
            allComments.Add(new Comment(
                "#58008_918",
                "",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(2), new RestorePowerExecutable(4), new RandomDmgExecutable(() => 0)))
            );

            // 7
            allComments.Add(new Comment(
                "0xdeadbeef",
                "1 <sprite=1> for each power charged",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new RandomDmgExecutable(() => Player.batteryUnderPostGained, new[]
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
                    new GetCommentExecutable(2),
                    new RandomDmgExecutable(() => 0)
                )));

            // 9
            allComments.Add(
                new Comment(
                    "Mic Drop",
                    "1 for each used comment under this post",
                    LikeInteractionType.REDUCE_STRESS,
                    0,
                    new CombinedExecutable(new RandomDmgExecutable(() => Player.postedComments.Count),

        new BatteryCostExecutable(2))));
            
            // 10
            allComments.Add(new Comment(
                "#$?!#",
                "",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(-1), new BatteryCostExecutable(2), new RandomDmgExecutable(() => 0)))
            );
            
            // 11
            allComments.Add(new Comment(
                "#$?!# you",
                "",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new AddStressExecutable(-2), new BatteryCostExecutable(4), new RandomDmgExecutable(() => 0)))
            );
            
            // 12
            allComments.Add(new Comment(
                "RAGE!!",
                "1<sprite=1>. +1<sprite=1> for each <sprite=23> received under this post",
                LikeInteractionType.RESTORE_DEVICE_POWER,
                1,
                new CombinedExecutable(new RandomDmgExecutable(() => Player.stressUnderPostGained + 1), new BatteryCostExecutable(5)))
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
                new CombinedExecutable(new RestorePowerExecutable(1), new RandomDmgExecutable(() => 0)))
            );
            
            // 16
            allComments.Add(new Comment(
                "BRB",
                "Next Refresh costs 1 less <sprite=23>",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(2), new TempEffectExecutable(TempEffectExecutable.Length.REFRESH, TempEffect.CHEAPER_REFRESH), new RandomDmgExecutable(() => 0)))
            );
            
            // 17
            allComments.Add(new Comment(
                "I'll be back",
                "Next Refresh is free",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(4), new TempEffectExecutable(TempEffectExecutable.Length.REFRESH, TempEffect.FREE_REFRESH), new RandomDmgExecutable(() => 0)))
            );
            
            // 18
            allComments.Add(new Comment(
                "I am back",
                "1<sprite=1> for each refresh used",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(3), new RandomDmgExecutable(() => Player.refreshUnderPost)))
            );
            
            // 19
            allComments.Add(new Comment(
                "Clickbait",
                "Next comments get +1<sprite=1> until Refresh",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(5), new TempEffectExecutable(TempEffectExecutable.Length.REFRESH, TempEffect.COMMENTS_PLUS_1), new RandomDmgExecutable(() => 0))
            ));
            
            // 20
            allComments.Add(new Comment(
                "1337",
                "Next comments doesn't use <sprite=3>",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(2), new TempEffectExecutable(TempEffectExecutable.Length.COMMENT, TempEffect.NEXT_NO_BATTERY), new RandomDmgExecutable(() => 0))
            ));
            
            // 21
            allComments.Add(new Comment(
                "Even",
                "1<sprite=1> for each comment with even <sprite=1>, but not 0",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(3), new RandomDmgExecutable(() =>
                {
                    return Player.postedComments.FindAll((x) => x.currentLikes % 2 == 0 && x.currentLikes != 0).Count;
                }))
            ));
            
            // 22
            allComments.Add(new Comment(
                "Odd",
                "+1<sprite=1> for each comment with odd <sprite=1>",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(3), new RandomDmgExecutable(() =>
                {
                    return Player.postedComments.FindAll((x) => x.currentLikes % 2 != 0).Count;
                }))
            ));
            
            // 23
            allComments.Add(new Comment(
                "Bump",
                "Adds 1<sprite=1> to all past comments. Doesn't count towards score.",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(3), new RandomDmgExecutable(() =>0), new BumpExecutable())
            ));
            
            // 24
            allComments.Add(new Comment(
                "Steal",
                "Steals 1<sprite=1> from previous comment. Doesn't add to score.",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(1), new RandomDmgExecutable(() =>0), new StealExecutable())
            ));
            
            // 25
            allComments.Add(new Comment(
                "SEO",
                "Next comment with random <sprite=1> gets max",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(2), new TempEffectExecutable(TempEffectExecutable.Length.COMMENT, TempEffect.MAX_RANDOM), new RandomDmgExecutable(() => 0))
            ));
            
            // 25
            allComments.Add(new Comment(
                "cHArger",
                "Costs 1 <sprite=3> less for each ha posted",
                LikeInteractionType.REDUCE_STRESS,
                1,
                new CombinedExecutable(new BatteryCostExecutable(() => 8 - Player.currentHaCount), new RandomDmgExecutable(() => 4), new HaExecutable(0, 1))
            ));
            return allComments;
        }

        public static List<Comment> rollComments(int number)
        {
            var result = new List<Comment>();
            var allCommentsThisRarity = getAllComments();

            var checkedIndexes = new bool[allCommentsThisRarity.Count];
            for (int i = 0; i < number; i++)
            {
                int index;
                do
                {
                    index = Random.Range(0, allCommentsThisRarity.Count);
                } while (checkedIndexes[index]);
                result.Add(allCommentsThisRarity[index]);
                checkedIndexes[index] = true;
            }

            return result;
        }
        
        public static List<Comment> getInitialVocabulary()
        {
            var result = new List<Comment>();
            var allComments = getAllComments();
            result.Add(allComments[0]); // WTF
            result.Add(allComments[0]); // WTF
            result.Add(allComments[24]); // STEAL
            result.Add(allComments[6]); // 58008_918
            result.Add(allComments[6]); // 58008_918
            result.Add(allComments[21]); // EVEN
            result.Add(allComments[22]); //ODD
            result.Add(allComments[2]); // ha
            result.Add(allComments[3]); // haha
            result.Add(allComments[3]); // haha
            result.Add(allComments[23]); // Bump
            return result;
        }
    }
}