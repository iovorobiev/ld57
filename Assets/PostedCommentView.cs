using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine.Comments;
using TMPro;
using UnityEngine;

public class PostedCommentView : MonoBehaviour
{
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI likesCount;
    public GameObject likesImage;
    private int prevLikes = 0;
    public PostedComment postedComment;

    private void Update()
    {
        if (prevLikes != postedComment.currentLikes)
        {
            UpdateLikes(postedComment.currentLikes);
            prevLikes = postedComment.currentLikes;
        }
    }

    public async UniTask SetData(PostedComment postedComment)
    {
        commentText.text = postedComment.originalComment.text;
        this.postedComment = postedComment;
    }

    public async UniTask UpdateLikes(int likes)
    {
        likesImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var scaleSequence = DOTween.Sequence();
        scaleSequence.Append(likesImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f));
        scaleSequence.Append(likesImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.05f));
        await UniTask.WhenAll(
            scaleSequence.Play().ToUniTask(),
            DOTween.To(x => likesCount.text = likes.ToString(), 0, likes, 0.2f).ToUniTask()
        );
    }

}
