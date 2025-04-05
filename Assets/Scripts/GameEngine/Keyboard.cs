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
        refresh.comment = Comments.getAllComments()[1];
    }
    
    public async UniTask OnShow()
    {
        var hand = new List<Comment>();
        if (currentHand.Count == 0)
        {
            for (int i = 0; i < Player.drawHandSize && Player.currentEncounterDeck.Count > 0; i++)
            {
                hand.Add(Player.currentEncounterDeck.Dequeue());
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

    public bool isEmpty()
    {
        return currentHand.Count == 0;
    }

    public async UniTask clearHand()
    {
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
        var (_, result) = await UniTask.WhenAny(clickListeners).AttachExternalCancellation(cancellationToken);
        if (result == refresh)
        {
            return result.comment;
        }
        
        await Game.commentView.claimComment(result);
        currentHand.Remove(result);

        return result.comment;
    }
}
