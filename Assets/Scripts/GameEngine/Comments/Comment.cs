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

    public enum TempEffect
    {
        MAX_RANDOM
    }

    public enum Tags
    {
        FINISHER
    }
}