using System;
using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.Comments;
using UnityEngine;
using utils;

public class CommentItem : MonoBehaviour
{
    public bool isInHand;

    public Comment comment;
    public readonly AwaitableClickListener<CommentItem> clickListener = new();
    
    private void OnMouseDown()
    {
        if (isInHand)
        {
            isInHand = false;
            clickListener.notifyClick(this);
        }
    }

    public async UniTask discard()
    {
        Destroy(gameObject);
    }
}
