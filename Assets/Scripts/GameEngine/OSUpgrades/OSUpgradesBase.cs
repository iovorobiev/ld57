using System.Collections.Generic;

namespace GameEngine.OSUpgrades
{
    public class OSUpgradesBase
    {
        public static string ENHANCED_HAND = "ENHANCED_HAND";
        
        public static List<OSUpgrade> getAllOsUpgrades()
        {
            var result = new List<OSUpgrade>();
            result.Add(
                new OSUpgrade(ENHANCED_HAND, "V1.1 Keyboard buttons +1",4, 0)
                );
            return result;
        }

        public static List<OSUpgrade> getRandomOsUpgrades(int number)
        {
            var result = new List<OSUpgrade>();
            for (int i = 0; i < number; i++)
            {
                result.Add(getAllOsUpgrades()[0]);
            }

            return result;
        }
    }
}