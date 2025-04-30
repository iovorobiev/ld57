using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GameEngine.Comments
{
    public class Comment
    {
        public static string PATH_TO_COMMENT_PREFAB = "Prefabs/Comment/";
        public Comment(string text, string description, LikeInteractionType type, int rarity, Executable script, List<Tags> tags = null)
        {
            this.text = text;
            this.description = description;
            this.script = script;
            this.type = type;
            this.rarity = rarity;
            if (tags != null)
            {
                this.tags = tags;
            }
        }

        public string text;
        public string description;
        public List<Tags> tags = new();
        public int rarity;
        public LikeInteractionType type;
        public string prefab = PATH_TO_COMMENT_PREFAB + "comment_bg";
        public Executable script;
    }

    public class PostedComment
    {
        public Comment originalComment;
        public int currentLikes;
        public int currentBattery;
        public int currentStress;

        public PostedComment(Comment originalComment, int currentLikes = 0, int currentBattery = 0, int currentStress = 0)
        {
            this.originalComment = originalComment;
            this.currentLikes = currentLikes;
            this.currentBattery = currentBattery;
            this.currentStress = currentStress;
        }
    }

    public enum TempEffect
    {
        MAX_RANDOM
    }

    public enum Tags
    {
        FINISHER
    }
}