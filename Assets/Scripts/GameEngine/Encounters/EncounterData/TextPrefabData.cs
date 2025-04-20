using GameEngine.EncounterData;

namespace GameEngine.Encounters.EncounterData
{
    public class TextPrefabData : SpecificData
    {
        public string prefabPath;
        public string text;

        public TextPrefabData(string prefabPath, string text)
        {
            this.prefabPath = prefabPath;
            this.text = text;
        }
    }
}