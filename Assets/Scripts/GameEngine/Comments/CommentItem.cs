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
    public Canvas canvas;
    public TextMeshProUGUI text;

    
    private void Start()
    {
        if (comment != null)
        {
            text.text = comment.text;
        }
    }

    public void SetIsInHand(bool value)
    {
        isInHand = value;
        if (!isInHand)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void changeSortingLayer(string layer)
    {
        var rend = GetComponent<Renderer>();
        rend.sortingLayerName = layer;
        rend.sortingOrder = 3;
        
        canvas.sortingLayerName = layer;
        canvas.sortingOrder = 4;
    }

    public void setComment(Comment comment)
    {
        this.comment = comment;
        if (text != null)
        {
            text.text = comment.text;
        }
    }

    public int getLikes()
    {
        return comment.value();
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
