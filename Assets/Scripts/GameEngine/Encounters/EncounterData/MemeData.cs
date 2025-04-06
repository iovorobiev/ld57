using GameEngine.EncounterData;

namespace GameEngine.Encounters.EncounterData
{
    public class MemeData : SpecificData
    {
        public string prefabPath;
        public string text;

        public MemeData(string prefabPath, string text)
        {
            this.prefabPath = prefabPath;
            this.text = text;
        }
    }
}