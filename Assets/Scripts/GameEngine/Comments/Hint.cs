using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using ui;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public TextTyping hintText;
    public Transform showPos;
    public Transform hidePos;
    private CancellationTokenSource source = new();
    
    private void Awake()
    {
        Game.hint = this;
    }

    public async void showHint(string text)
    {
        hintText.animateText("");
        source.Cancel();
        source = new CancellationTokenSource();
        await transform.DOMove(showPos.transform.position, 0.2f).ToUniTask();
            // .AttachExternalCancellation(source.Token);
        hintText.animateText(text);
    }

    public async void hideHint() {
        source.Cancel();
        source = new CancellationTokenSource();
        await transform.DOMove(hidePos.transform.position, 0.2f).ToUniTask();
            // .AttachExternalCancellation(source.Token);
        hintText.animateText("");
    }
}
