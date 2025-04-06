namespace GameEngine.Encounters.EncounterData
{
    public class DoubleTextData : SpecificData
    {
        public string topText;
        public string botText;

        public DoubleTextData(string topText, string botText)
        {
            this.topText = topText;
            this.botText = botText;
        }
    }
}