using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.OSUpgrades
{
    public class OSUpgradesBase
    {
        public static string ENHANCED_HAND = "ENHANCED_HAND";
        public static string EFFECTIVE_BATTERY = "EFFECTIVE_BATTERY";
        public static string INFLUENCER = "INFLUENCER";
        public static string ARMOR = "ARMOR";
        public static string BAT_ARMOR = "BAT_ARMOR";
        public static string FINISHERS = "FINISHERS";
        public static string BAT_ATTACK = "BAT_ATTACK";
        public static string BETTER_REFRESH = "BETTER_REFRESH";
        public static string CHARGE_MORE = "CHARGE_MORE";
        public static string EASY_START = "EASY_START";
        
        public static List<OSUpgrade> getAllOsUpgrades()
        {
            var result = new List<OSUpgrade>();
            result.Add(
                new OSUpgrade(ENHANCED_HAND, "Keyboard buttons +1",4, 0)
                );
            result.Add(
                new OSUpgrade(EFFECTIVE_BATTERY, "Scroll doesn't cost battery",1, 0)
            );
            result.Add(
                new OSUpgrade(INFLUENCER, "You comments get +1 like",-1, 0)
            );
            result.Add(
                new OSUpgrade(ARMOR, "Stress damage is reduced by 1",-1, 0)
            );
            result.Add(
                new OSUpgrade(BAT_ARMOR, "Comments cost 1 less battery (Min 1)",-1, 0)
            );
            result.Add(
                new OSUpgrade(FINISHERS, "Comments depending on other comments get 2x damage",-1, 0)
            );
            result.Add(
                new OSUpgrade(BAT_ATTACK, "Machine codes get +2 likes",-1, 0)
            );
            
            result.Add(
                new OSUpgrade(BETTER_REFRESH, "Refresh causes 1 less stress",-1, 0)
            );
            
            result.Add(
                new OSUpgrade(CHARGE_MORE, "Start with 10% more battery",7, 0)
            );
            
            result.Add(
                new OSUpgrade(EASY_START, "Start with 10% less stress",3, 0)
            );
            
            result.Add(
                new OSUpgrade(EASY_START, "Start with 10% less stress",3, 0)
            );
            return result;
        }

        public static List<OSUpgrade> getRandomOsUpgrades(int number)
        {
            var result = new List<OSUpgrade>();
            for (int i = 0; i < number; i++)
            {
                result.Add(getAllOsUpgrades()[Random.Range(0, getAllOsUpgrades().Count)]);
            }

            return result;
        }
    }
}