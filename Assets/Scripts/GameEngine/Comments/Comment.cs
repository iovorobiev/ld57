namespace GameEngine.Comments
{
    public class Comment
    {
        public static string PATH_TO_COMMENT_PREFAB = "Prefabs/Comment/";
        public Comment(string text, string description, int value, LikeInteractionType type, Executable script)
        {
            this.text = text;
            this.description = description;
            this.value = value;
            this.script = script;
            this.type = type;
        }

        public string text;
        public string description;
        public int value;
        public LikeInteractionType type;
        public string prefab = PATH_TO_COMMENT_PREFAB + "comment_bg";
        public Executable script;
    }
}