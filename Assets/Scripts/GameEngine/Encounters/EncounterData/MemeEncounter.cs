using System.Collections.Generic;
using GameEngine.Comments;
using GameEngine.EncounterData;

namespace GameEngine.Encounters.EncounterData
{
    public class MemeEncounter : Encounter
    {
        private int likes;
        private readonly MemeData data;
        private readonly EncounterExecutable executable;

        public MemeEncounter(int likes, MemeData data, EncounterExecutable executable)
        {
            this.likes = likes;
            this.data = data;
            this.executable = executable;
        }

        public string getPrefabAddress()
        {
            return "Prefabs/Memes/" + "MemeEncounter";
        }

        public int getLikes()
        {
            return likes;
        }

        public LikeInteractionType getLikeInteractionType()
        {
            return LikeInteractionType.REDUCE_STRESS;
        }

        public EncounterExecutable getScript()
        {
            return executable;
        }

        public List<Comment> getComments()
        {
            return new List<Comment>();
        }

        public SpecificData getData()
        {
            return data;
        }

        public bool isBlocking()
        {
            return false;
        }
    }
}