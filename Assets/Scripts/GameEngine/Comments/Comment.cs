using System;
using Unity.VisualScripting;

namespace GameEngine.Comments
{
    public class Comment
    {
        public static string PATH_TO_COMMENT_PREFAB = "Prefabs/Comment/";
        public Comment(string text, string description, Func<int> value, LikeInteractionType type, int rarity, Executable script)
        {
            this.text = text;
            this.description = description;
            this.value = value;
            this.script = script;
            this.type = type;
            this.rarity = rarity;
        }

        public string text;
        public string description;
        public Func<int> value;
        public int rarity;
        public LikeInteractionType type;
        public string prefab = PATH_TO_COMMENT_PREFAB + "comment_bg";
        public Executable script;
    }
}