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

    private bool locked = false;
    
    private void Awake()
    {
        Game.hint = this;
    }

    public async void showHint(string text)
    {
        if (locked)
        {
            return;
        }
        hintText.animateText("");
        source.Cancel();
        source = new CancellationTokenSource();
        await transform.DOMove(showPos.transform.position, 0.2f).ToUniTask();
            // .AttachExternalCancellation(source.Token);
        hintText.animateText(text);
    }
    
    public async void showHintAndLock(string text)
    {
        locked = true;
        hintText.animateText("");
        source.Cancel();
        source = new CancellationTokenSource();
        await transform.DOMove(showPos.transform.position, 0.2f).ToUniTask();
        hintText.animateText(text);
        UniTask.WaitForSeconds(2);
        unlock();
    }

    public void unlock()
    {
        locked = false;
    }

    public async void hideHint() {
        if (locked)
        {
            return;
        }
        source.Cancel();
        source = new CancellationTokenSource();
        await transform.DOMove(hidePos.transform.position, 0.2f).ToUniTask();
            // .AttachExternalCancellation(source.Token);
        hintText.animateText("");
    }
}
