using System.Collections.Generic;

namespace GameEngine.Comments.CommentsData
{
    public class Comments
    {
        public static List<Comment> getAllComments()
        {
            var allComments = new List<Comment>();

            allComments.Add(
                    new Comment("WTF??", "WTF will definitely get 1 like", 1, new AttackExecutable()));
            return allComments;
        }

        public static List<Comment> getInitialVocabulary()
        {
            var result = new List<Comment>();
            var allComments = getAllComments();
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            result.Add(allComments[0]);
            return result;
        }
    }
}