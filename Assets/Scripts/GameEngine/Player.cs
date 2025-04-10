using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.OSUpgrades;
using Unity.Mathematics.Geometry;
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

        public static int currentHaCount = 0;
        public static int batteryUnderPost = 0;

        public static List<OSUpgrade> upgrades = new();
        public static bool loseFlag;

        public static void AddOSUpgrade(OSUpgrade upgrade)
        {
            upgrades.Add(upgrade);
        }

        public static void reset()
        {
            stressLevel = initStressLevel - upgrades.FindAll((up) => up.upgradeID == OSUpgradesBase.EASY_START).Count ;
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
        
        public static void prepareEncounterDeck()
        {
            Game.vocabularyView.resetVocab(vocabulary);
            currentEncounterDeck.Clear();
            var checkedPositions = new bool[vocabulary.Count];
            Debug.Log("Vocab is " + vocabulary.Count);
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

        public static void prepareEncounter()
        {
            prepareEncounterDeck();
            currentHaCount = 0;
            batteryUnderPost = 0;
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
            batteryUnderPost += dmg;
            await Game.ui.changeBatteryLevel(from, powerLevel);
        }

        public static async UniTask restorePower(int amount)
        {
            int from = powerLevel;
            powerLevel += amount;
            await Game.ui.changeBatteryLevel(from, powerLevel);
        }

        public static async UniTask receiveStressDamage(int dmg)
        {
            var prev = stressLevel;
            if (dmg > 0)
            {
                dmg -= upgrades.FindAll((up) => OSUpgrades.OSUpgradesBase.ARMOR == up.upgradeID).Count;
                dmg = Mathf.Max(dmg, 0);
            }
            stressLevel += dmg;
            await Game.ui.changeStressLevel(prev, stressLevel);
        }
        
        public static bool winCondition()
        {
            return stressLevel <= 0;
        }

        public static bool loseCondition()
        {
            if (powerLevel <= 0)
            {
                Game.hint.showHintAndLock("Ah snap! Ran out of battery :( Let me boot it again!");
            }

            if (stressLevel >= 100)
            {
                Game.hint.showHintAndLock("This is unbearable! Maybe if I reboot it will be better...");
            }
            
            return stressLevel >= 100 || powerLevel <= 0 || loseFlag;
        }
    }
}