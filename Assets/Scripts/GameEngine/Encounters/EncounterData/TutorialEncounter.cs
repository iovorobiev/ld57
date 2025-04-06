using System.Collections.Generic;
using GameEngine.Comments;

namespace GameEngine.Encounters.EncounterData
{
    public class TutorialEncounter : Encounter
    {
        public static string TUTORIAL_PATH = "Prefabs/Tutorial/";
        private int likes;
        private string mainText;
        private string callToAction;
        private string hintText;
        private bool blocking;

        public TutorialEncounter(int likes, string mainText, string callToAction, string hintText = "", bool blocking = false)
        {
            this.likes = likes;
            this.mainText = mainText;
            this.callToAction = callToAction;
            this.hintText = hintText;
            this.blocking = blocking;
        }

        public string getPrefabAddress()
        {
            return TUTORIAL_PATH + "Tutorial";
        }
        
        public int getLikes()
        {
            return likes;
        }

        public LikeInteractionType getLikeInteractionType()
        {
            return LikeInteractionType.NO_EFFECT;
        }

        public EncounterExecutable getScript()
        {
            return new TutorialExecutable(mainText, callToAction, hintText);
        }

        public List<Comment> getComments()
        {
            return new();
        }

        public EnemySpecificData getEnemyData()
        {
            return null;
        }

        public bool isBlocking()
        {
            return blocking;
        }
    }
}