namespace GameEngine.Comments
{
    public class Comment
    {
        public static string PATH_TO_COMMENT_PREFAB = "Prefabs/Comment/";
        public Comment(string text, string description, int value, Executable script)
        {
            this.text = text;
            this.description = description;
            this.value = value;
            this.script = script;
        }

        public string text;
        public string description;
        public int value;
        public string prefab = PATH_TO_COMMENT_PREFAB + "comment_bg";
        public Executable script;
    }
}