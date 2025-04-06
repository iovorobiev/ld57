namespace GameEngine.OSUpgrades
{
    public class OSUpgrade
    {
        public string upgradeID;
        public int rarity;
        public int maxRepeats;
        public string text;

        public OSUpgrade(string upgradeID, string text, int maxRepeats, int rarity)
        {
            this.text = text;
            this.upgradeID = upgradeID;
            this.maxRepeats = maxRepeats;
            this.rarity = rarity;
        }
    }
}