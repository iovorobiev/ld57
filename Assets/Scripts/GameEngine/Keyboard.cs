using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    private int lastFreePosition = 0;

    public Transform openPos;
    public Transform closePos;
    public Transform closeInCommentsPos;

    public KeyboardState state = KeyboardState.CLOSED;
    
    private void Awake()
    {
        Game.keyboard = this;
        refresh.comment = CommentsBase.getRefresh();
    }

    public async UniTask open()
    {
        state = KeyboardState.SHOWN;
        await transform.DOMove(openPos.position, 0.5f).ToUniTask();
    }

    public void hideRefresh()
    {
        refresh.gameObject.SetActive(false);
    }

    public async UniTask showRefresh()
    {
       refresh.gameObject.SetActive(true);
       var localScale = refresh.transform.localScale;
       await refresh.transform.DOScale(localScale, 0.25f).From(Vector3.zero).ToUniTask();
    }
    
    public async UniTask close()
    {
        state = KeyboardState.CLOSED;
        await transform.DOMove(closePos.position, 0.5f).ToUniTask();
    }

    public async UniTask closeInComments()
    {
        state = KeyboardState.HIDDEN_COMMENTS;
        await transform.DOMove(closeInCommentsPos.position, 0.5f).ToUniTask();
    }

    public async UniTask waitForOpen()
    {
        await UniTask.WaitUntil(() => state == KeyboardState.SHOWN);
    }

    public bool canDrawMore()
    {
        return currentHand.Count < Player.getDrawHandSize();
    }
    
    public async UniTask OnShow()
    {
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
            AddCommentToHand(hand[i], i);
        }

        lastFreePosition = currentHand.Count;
        
        Game.currentEncounterController.OnKeyboardOpened();
    }

    public bool isEmpty()
    {
        return currentHand.Count == 0;
    }

    public async UniTask fillHand()
    {
        for (int i = currentHand.Count; i < Player.getDrawHandSize(); i++)
        {
            await draw();
        }
    }
    
    public async UniTask draw()
    {
        if (currentHand.Count > Player.maxHandSize || Player.currentEncounterDeck.Count == 0)
        {
            return;
        }

        var comment = Player.currentEncounterDeck.Dequeue();
        
        Game.vocabularyView.removeComment(comment);
        AddCommentToHand(comment, currentHand.Count);
    }

    private void AddCommentToHand(Comment comment, int indexInHand)
    {
        var commentObj = positionsForComments[indexInHand];
        commentObj.SetActive(true);
        var commentItem = commentObj.GetComponent<CommentItem>();
        commentItem.SetIsInHand(true);
        commentItem.setComment(comment);
        UniTask.WhenAll(commentObj.GetComponent<SpriteRenderer>().DOFade(1f, 0.2f).From(0f).ToUniTask(),
            commentObj.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f).From(new Vector3(0.1f, 0.1f, 0.1f))
                .ToUniTask());
        currentHand.Add(commentItem);
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
        var (_, result) = await UniTask.WhenAny(clickListeners).AttachExternalCancellation(cancellationToken);
        if (result == refresh)
        {
            return result.comment;
        }

        var index = currentHand.FindIndex((obj) =>  obj == result);
        var comment = result.comment;
        var prevPosition = result.transform.position;
        var tasksList = new List<UniTask>();
        for (int i = index + 1; i < positionsForComments.Count; i++)
        {
            tasksList.Add(positionsForComments[i].transform.DOMove(prevPosition, 0.2f).ToUniTask());
            prevPosition = positionsForComments[i].transform.position;
        }
        tasksList.Add(Game.currentEncounterController.commentView.claimComment(result));
        await UniTask.WhenAll(tasksList);
        result.transform.position = prevPosition;
        result.gameObject.SetActive(false);
        currentHand.Remove(result);
        positionsForComments.Remove(result.gameObject);
        positionsForComments.Add(result.gameObject);
        return comment;
    }

    public enum KeyboardState
    {
        SHOWN,
        HIDDEN_COMMENTS,
        CLOSED,
    }
}
