using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.Comments;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CommentView : MonoBehaviour
{
    private List<CommentItem> comments = new();

    private void Awake()
    {
        Game.commentView = this;
    }

    public async UniTask claimComment(CommentItem commentItem)
    {
        var tasksList = new List<UniTask>();
        tasksList.Add(commentItem.transform.DOMove(transform.position, 0.2f).ToUniTask());
        commentItem.transform.parent = transform;
        foreach (var com in comments)
        {
            tasksList.Add(
                com.transform.DOMove(new Vector3(com.transform.position.x, com.transform.position.y - 0.8f, com.transform.position.z), 0.2f).ToUniTask()
                );
        }
        comments.Add(commentItem);
        commentItem.changeSortingLayer("Encounter");
        commentItem.isInHand = false;
        await UniTask.WhenAll(tasksList);
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
