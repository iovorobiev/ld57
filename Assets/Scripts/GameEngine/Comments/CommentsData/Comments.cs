using System.Collections.Generic;

namespace GameEngine.Comments.CommentsData
{
    public class Comments
    {
        public static List<Comment> getAllComments()
        {
            var allComments = new List<Comment>();

            allComments.Add(
                new Comment("WTF??", "WTF will definitely get 1 like", 1, LikeInteractionType.REDUCE_STRESS,
                    new EmptyExecutable()));
            allComments.Add(new Comment("Refresh", "It is extremely stressful that I need to find words!", 0,
                LikeInteractionType.SKIP_TURN, new RefreshExecutable()));
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