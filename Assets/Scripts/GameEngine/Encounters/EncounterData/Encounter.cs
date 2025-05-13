using System.Collections.Generic;
using GameEngine.Comments;
using GameEngine.Encounters;

namespace GameEngine
{
    public class Encounter
    {
        public int likes;
        public List<Tags> tags;
        public string prefabPath;
        public SpecificData visData;
        public EncounterExecutable executable;
        
        public Encounter(int likes, List<Tags> tags, string prefabPath, EncounterExecutable executable, SpecificData visData = null)
        {
            this.likes = likes;
            this.tags = tags;
            this.executable = executable;
            this.visData = visData;
            this.prefabPath = prefabPath;
        }
    }

    public interface SpecificData
    {
    }

    public enum Tags
    {
        Stressful,
        Blocking,
        Meme,
        Tutorial,
        NO_COMMENTS,
        NO_UI,
    }
    
    public enum LikeInteractionType
    {
        REDUCE_STRESS,
        RESTORE_DEVICE_POWER,
        NO_EFFECT,
        INCREASE_STRESS,
        REDUCE_DEVICE_POWER,
        SKIP_TURN,
    }
}