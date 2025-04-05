using System.Collections.Generic;

namespace GameEngine
{
    public interface Encounter
    {
        string getPrefabAddress();
        int getLikes();
        LikeInteractionType getLikeInteractionType();
        List<Comment> getComments();
    }

    public enum LikeInteractionType
    {
        REDUCE_STRESS,
        RESTORE_DEVICE_POWER,
        NO_EFFECT,
        INCREASE_STRESS,
        REDUCE_DEVICE_POWER
    }
}