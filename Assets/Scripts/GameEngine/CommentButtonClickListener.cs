using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommentButtonClickListener : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CancellationTokenSource cancelSource = new();
    private Vector3 initScale;

    private void Start()
    {
        initScale = transform.localScale;
        transform.DOLocalMoveY(transform.localPosition.y + 0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        await Game.encountersPresenter.toggleKeyboard();
    }
    
    public async void OnPointerEnter(PointerEventData eventData)
    {
        cancelSource.Cancel();
        cancelSource = new();
        await transform.DOScale(initScale + Vector3.one * 0.2f, 0.25f).ToUniTask().AttachExternalCancellation(cancelSource.Token).SuppressCancellationThrow();
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        cancelSource.Cancel();
        await UniTask.NextFrame();
        transform.localScale = initScale;
    }
}
