using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<PostedCommentView> comments = new();

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
        var postedComment = postedCommentObject.GetComponent<PostedCommentView>();
        var postedCommentData = new PostedComment(commentItem.comment);
        comments.Add(postedComment);
        Player.postedComments.Add(postedCommentData);
        postedComment.SetData(postedCommentData);
    }

    public async UniTask animateAttack()
    {
        await comments.Last().animateAttack();
    }

    public void clearComments()
    {
        foreach (var commentItem in comments)
        {
            Destroy(commentItem.gameObject);
        }
        Player.postedComments.Clear();
        comments.Clear();
    }
}