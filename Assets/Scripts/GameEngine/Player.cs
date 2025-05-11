using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.OSUpgrades;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
    public class Player
    {
        private static int initStressLevel = 50;
        private static int initPowerLevel = 70;
        public static int stressLevel = 50;
        public static int powerLevel = 20;
        public static List<Comment> vocabulary;
        public static Queue<Comment> currentEncounterDeck = new();
        public static List<PostedComment> postedComments = new();
        
        private static int drawHandSize = 4;
        public static int maxHandSize = 6;

        public static int currentHaCount = 0;
        public static int batteryUnderPostSpent = 0;
        public static int batteryUnderPostGained = 0;
        public static int stressUnderPostGained = 0;
        public static int refreshUnderPost = 0;

        public static List<OSUpgrade> upgrades = new();

        public static List<TempEffect> nextComment = new();
        public static List<TempEffect> nextRefresh = new();
        public static List<TempEffect> nextEncounter = new();
        
        public static bool loseFlag;

        public static void AddOSUpgrade(OSUpgrade upgrade)
        {
            upgrades.Add(upgrade);
        }

        public static int calculateLikesWithBonuses(int likes, List<Comments.Tags> tags = null)
        {
            var updatedLikes = likes + upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.INFLUENCER).Count;
            if (tags != null && tags.Contains(Comments.Tags.FINISHER))
            {
                var finisherMultipler = Mathf.Pow(2,
                    Player.upgrades.FindAll((up) => OSUpgradesBase.FINISHERS == up.upgradeID).Count);
                updatedLikes *= (int) finisherMultipler;
            }
            return updatedLikes;
        }

        public static void reset()
        {
            stressLevel = initStressLevel - upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.EASY_START).Count;

            Game.screenController.changeStressLevel(stressLevel, stressLevel);
            powerLevel = initPowerLevel + upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.CHARGE_MORE).Count;
            loseFlag = false;
            prepareVocabulary();
        }
        
        public static int getDrawHandSize()
        {
            var handUps = upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.ENHANCED_HAND);
            return drawHandSize + handUps.Count;
        }
        
        public static void prepareVocabulary()
        {
            vocabulary = Comments.CommentsData.CommentsBase.getInitialVocabulary();
            Game.vocabularyView.resetVocab(vocabulary);
        }
        
        public static void prepareEncounterDeck(bool forTutorial = false)
        {
            Game.vocabularyView.resetVocab(vocabulary);
            currentEncounterDeck.Clear();
            int[] firstHand;
            var checkedPositions = new bool[vocabulary.Count];
            if (forTutorial)
            {
                firstHand = new[] { 0, 5, 3, 9 };
                currentEncounterDeck.Enqueue(vocabulary[firstHand[0]]);
                currentEncounterDeck.Enqueue(vocabulary[firstHand[1]]);
                currentEncounterDeck.Enqueue(vocabulary[firstHand[2]]);
                currentEncounterDeck.Enqueue(vocabulary[firstHand[3]]);
                checkedPositions[firstHand[0]] = true;
                checkedPositions[firstHand[1]] = true;
                checkedPositions[firstHand[2]] = true;
                checkedPositions[firstHand[3]] = true;
            }
            else
            {
                firstHand = Array.Empty<int>();
            }
            
            for (int i = 0; i < vocabulary.Count - firstHand.Length; i++)
            {
                int index; 
                do
                {
                    index = Random.Range(0, vocabulary.Count);
                } while (checkedPositions[index]);

                checkedPositions[index] = true;
                currentEncounterDeck.Enqueue(vocabulary[index]);
            }
        }

        public static bool hasTempEffect(TempEffect effect)
        {
            return nextComment.Contains(effect) || nextRefresh.Contains(effect) || nextEncounter.Contains(effect);
        }

        public static void removeFromCommentEffect(TempEffect effect)
        {
            nextComment.Remove(effect);
        }

        public static void prepareEncounter(bool isTutorial = false)
        {
            prepareEncounterDeck(isTutorial);
            currentHaCount = 0;
            batteryUnderPostSpent = 0;
            batteryUnderPostGained = 0;
            stressUnderPostGained = 0;
            refreshUnderPost = 0;
            nextEncounter.Clear();
            nextRefresh.Clear();
            nextComment.Clear();
        }

        public static async UniTask addToVocabulary(Comment comment)
        {
            vocabulary.Add(comment);
            await Game.vocabularyView.addComment(comment);
        }
        
        public static async UniTask receivePowerDamage(int dmg)
        {
            int from = powerLevel;
            powerLevel -= dmg;
            if (dmg > 0)
            {
                batteryUnderPostSpent += dmg;
            }
            else
            {
                batteryUnderPostGained -= dmg;
            }
            await Game.screenController.changeBatteryLevel(from, powerLevel);
        }

        public static async UniTask receiveStressDamage(int dmg)
        {
            var prev = stressLevel;
            if (dmg > 0)
            {
                dmg -= upgrades.FindAll((up) => OSUpgrades.OSUpgradesBase.ARMOR == up.upgradeID).Count;
                dmg = Mathf.Max(dmg, 0);
                stressUnderPostGained += dmg;
            }
            stressLevel += dmg;
            await Game.screenController.changeStressLevel(prev, stressLevel);
        }
        
        public static bool winCondition()
        {
            return stressLevel <= 0;
        }

        public static bool loseCondition()
        {
            return stressLevel >= 100 || powerLevel <= 0 || loseFlag;
        }
    }
}