using System.Collections.Generic;
using GameEngine.Comments;
using GameEngine.Encounters;

namespace GameEngine
{
    public interface Encounter
    {
        string getPrefabAddress();
        int getLikes();
        LikeInteractionType getLikeInteractionType();

        EncounterExecutable getScript();
        List<Comment> getComments();

        EnemySpecificData getEnemyData();

        bool isBlocking();
    }

    public interface EnemySpecificData
    {
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