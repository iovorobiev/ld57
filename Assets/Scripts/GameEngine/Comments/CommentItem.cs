using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.Comments;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using utils;

public class CommentItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isInHand = true;

    public Comment comment;
    public readonly AwaitableClickListener<CommentItem> clickListener = new();
    
    public TextMeshPro title;
    public TextMeshPro description;
    public TextMeshPro likes;
    public TextMeshPro battery;
    public TextMeshPro stress;

    public float selectScale = 1.1f;
    
    private Vector3 originalScale;
    private SortingGroup _sortingGroup;
    
    private void Start()
    {
        originalScale = transform.localScale;
        _sortingGroup = GetComponent<SortingGroup>();
    }

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
    
    public async UniTask discard()
    {
        await GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickListener.notifyClick(this);
    }

    private CancellationTokenSource source = new();
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        source.Cancel();
        originalScale = transform.localScale;
        source = new CancellationTokenSource();
        transform.DOScale(new Vector3(selectScale, selectScale, selectScale), 0.1f).ToUniTask(cancellationToken: source.Token);
        _sortingGroup.sortingOrder = 5;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        source.Cancel();
        source = new CancellationTokenSource();
        transform.localScale = originalScale;
        _sortingGroup.sortingOrder = 2;
    }
}
