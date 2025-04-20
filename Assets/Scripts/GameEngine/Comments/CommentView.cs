using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.Comments;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CommentView : MonoBehaviour
{
    private List<PostedComment> comments = new();

    public GameObject emptyView;
    public GameObject content;
    public Transform firstCommentPosition;

    public Transform shownPosition;
    public Transform hidePosition;

    public GameObject viewObject;

    public GameObject commentsPrefab;
    private float _duration = 0.5f;

    public async UniTask showCommentsView()
    {
        viewObject.SetActive(true);
        await viewObject.transform.DOMove(shownPosition.position, _duration).ToUniTask();
    }

    public async UniTask hideCommentsView()
    {
        await viewObject.transform.DOMove(hidePosition.position, _duration).ToUniTask();
        viewObject.SetActive(false);
    }
    
    public async UniTask claimComment(CommentItem commentItem)
    {
        if (comments.Count == 0)
        {
            emptyView.SetActive(false);
        }

        await UniTask.WhenAll(
            commentItem.transform.DOMove(firstCommentPosition.position, _duration).ToUniTask(),
            commentItem.GetComponent<SpriteRenderer>().DOFade(0f, _duration).ToUniTask());


        var postedCommentObject = Instantiate(commentsPrefab, content.transform);
        var postedComment = postedCommentObject.GetComponent<PostedComment>();
        comments.Add(postedComment);
        Debug.Log("Setting data");
        var comment = commentItem.comment;
        postedComment.SetData(comment.text, Player.getLikesFromComment(comment));
        Debug.Log("destroying");
        Destroy(commentItem.gameObject);
    }

    public void clearComments()
    {
        foreach (var commentItem in comments)
        {
            Destroy(commentItem.gameObject);
        }

        comments.Clear();
    }
}