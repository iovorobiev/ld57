using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.Comments.CommentsData;
using UnityEngine;
using Comment = GameEngine.Comments.Comment;

public class Keyboard : MonoBehaviour
{
    private List<CommentItem> currentHand = new();
    public List<GameObject> positionsForComments = new();
    private List<UniTask<CommentItem>> clickListeners = new();
    public CommentItem refresh;
    private int lastKnownDepth = -1;

    private void Awake()
    {
        Game.keyboard = this;
        refresh.comment = CommentsBase.getAllComments()[1];
    }
    
    public async UniTask OnShow()
    {
        Debug.Log("On Show");
        var hand = new List<Comment>();
        if (currentHand.Count == 0)
        {
            for (int i = 0; i < Player.getDrawHandSize() && Player.currentEncounterDeck.Count > 0; i++)
            {
                var comment = Player.currentEncounterDeck.Dequeue();
                hand.Add(comment);
                Game.vocabularyView.removeComment(comment);
            }
        }

        for (int i = 0; i < hand.Count; i++)
        {
            var prefab = Resources.Load(hand[i].prefab) as GameObject;
            var comment = Instantiate(prefab, transform);
            var commentItem = comment.GetComponent<CommentItem>();
            commentItem.isInHand = true;
            commentItem.comment = hand[i];
            comment.transform.position = positionsForComments[i].transform.position;
            currentHand.Add(commentItem);
        }
    }

    public async UniTask clearHand()
    {
        Debug.Log("Clear hand");
        var discardTasks = new List<UniTask>();
        foreach (var item in currentHand)
        {
            discardTasks.Add(item.discard());
        }

        await UniTask.WhenAll(discardTasks);
        currentHand.Clear();
        clickListeners.Clear();
    }

    public async UniTask<Comment> awaitComment(CancellationToken cancellationToken)
    {
        clickListeners.Clear();
        foreach (var commentItem in currentHand)
        {
            clickListeners.Add(commentItem.clickListener.awaitClickWithCancellation(cancellationToken));
        }
        clickListeners.Add(refresh.clickListener.awaitClickWithCancellation(cancellationToken));
        Debug.Log("Listening for " + clickListeners.Count + " click listeners");
        var (_, result) = await UniTask.WhenAny(clickListeners).AttachExternalCancellation(cancellationToken);
        if (result == refresh)
        {
            Debug.Log("Refresh picked");
            return result.comment;
        }
        
        await Game.commentView.claimComment(result);
        currentHand.Remove(result);

        return result.comment;
    }
}
