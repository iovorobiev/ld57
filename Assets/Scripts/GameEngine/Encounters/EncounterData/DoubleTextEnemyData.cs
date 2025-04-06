namespace GameEngine.Encounters.EncounterData
{
    public class DoubleTextEnemyData : EnemySpecificData
    {
        public string topText;
        public string botText;

        public DoubleTextEnemyData(string topText, string botText)
        {
            this.topText = topText;
            this.botText = botText;
        }
    }
}