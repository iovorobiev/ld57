using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.Comments;
using TMPro;
using UnityEngine;
using utils;

public class CommentItem : MonoBehaviour
{
    private bool isInHand = true;

    public Comment comment;
    public readonly AwaitableClickListener<CommentItem> clickListener = new();
    public TextMeshPro title;
    public TextMeshPro description;
    public TextMeshPro likes;
    public TextMeshPro battery;
    public TextMeshPro stress;
    private void Update()
    {
        if (comment == null) return;
        likes.text = comment.script.getPrice(Executable.Resource.Likes);
        battery.text = comment.script.getPrice(Executable.Resource.Battery);
    }

    public void SetIsInHand(bool value)
    {
        isInHand = value;
        if (!isInHand)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void setComment(Comment comment)
    {
        this.comment = comment;
        title.text = comment.text;
        description.text = comment.description;
        var likesText = comment.script.getPrice(Executable.Resource.Likes);
        var batteryText = comment.script.getPrice(Executable.Resource.Battery); 
        var stressText = comment.script.getPrice(Executable.Resource.Stress); 
        battery.text = batteryText ?? "0";
        stress.text = stressText ?? "0";
        likes.text = likesText ?? "0";
    }
    
    private void OnMouseDown()
    {
        if (isInHand)
        {
            clickListener.notifyClick(this);
        }
    }
    public async UniTask discard()
    {
        await GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        Destroy(gameObject);
    }
}
