using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.Comments;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommentVocabItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Comment comment;

    public TextMeshProUGUI text;

    public void setComment(Comment comment)
    {
        this.comment = comment;
        text.text = comment.text;
    }

    public async void Disappear()
    {
        await text.DOFade(0f, 0.5f).ToUniTask();
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Game.hint.showHint(comment.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Game.hint.hideHint();
    }
}
