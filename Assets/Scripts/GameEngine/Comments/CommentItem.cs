using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        Debug.Log("On mouse down " + isInHand);
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
