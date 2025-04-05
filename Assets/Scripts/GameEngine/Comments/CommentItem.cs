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
    public Canvas canvas;
    public GameObject text;

    public void changeSortingLayer(string layer)
    {
        var rend = GetComponent<Renderer>();
        rend.sortingLayerName = layer;
        rend.sortingOrder = 3;
        canvas.sortingLayerName = layer;
        canvas.sortingOrder = 4;
    }
    
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
