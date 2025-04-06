using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine
{
    public class Player
    {
        private static int initStressLevel = 50;
        private static int initPowerLevel = 20;
        public static int stressLevel = 50;
        public static int powerLevel = 20;
        public static List<Comment> vocabulary;
        public static Queue<Comment> currentEncounterDeck = new();
        
        private static int drawHandSize = 3;
        public static int maxHandSize = 6;

        public static List<OSUpgrade> upgrades = new();

        public static void AddOSUpgrade(OSUpgrade upgrade)
        {
            upgrades.Add(upgrade);
        }

        public static void reset()
        {
            stressLevel = initStressLevel;
            powerLevel = initPowerLevel;
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
        }
        
        public static void prepareEncounterDeck()
        {
            var checkedPositions = new bool[vocabulary.Count];
            foreach (var _ in vocabulary)
            {
                int index; 
                do
                {
                    index = Random.Range(0, vocabulary.Count);
                } while (checkedPositions[index]);

                checkedPositions[index] = false;
                currentEncounterDeck.Enqueue(vocabulary[index]);
            }
        }

        public static async UniTask addToVocabulary(Comment comment)
        {
            vocabulary.Add(comment);
        }
        
        public static async UniTask receivePowerDamage(int dmg)
        {
            int from = powerLevel;
            powerLevel -= dmg;
            await Game.ui.changeBatteryLevel(from, powerLevel);
        }

        public static async UniTask receiveStressDamage(int dmg)
        {
            var prev = stressLevel;
            stressLevel += dmg;
            await Game.ui.changeStressLevel(prev, stressLevel);
        }
        
        public static bool winCondition()
        {
            return stressLevel == 0;
        }

        public static bool loseCondition()
        {
            return stressLevel >= 100 || powerLevel <= 0;
        }
    }
}