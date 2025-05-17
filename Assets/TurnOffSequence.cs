using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine;
using GameEngine.OSUpgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class TurnOffSequence : MonoBehaviour
{
    public GameObject blackOutScreen;
    public GameObject battery;
    public TMP_Text text;
    public GameObject stress;
    
    public RestartButtonClickListener restartButtonClickListener;
    private Material material;
    private SpriteRenderer _spriteRenderer;
    private string _progressField = "_progress";
    public AudioSource audioSource;

    private void Awake()
    {
        Game.turnOffSequence = this;
        gameObject.SetActive(false);
        var spriteRenderer = blackOutScreen.GetComponent<SpriteRenderer>();
        _spriteRenderer = spriteRenderer;
        material = new Material(_spriteRenderer.material);
        _spriteRenderer.material = material;
        audioSource = GetComponent<AudioSource>();
    }

    public void resetValues()
    {
        battery.SetActive(false);
        text.gameObject.SetActive(false);
        stress.gameObject.SetActive(false);
        material.SetFloat(_progressField, 1f);
        material.SetFloat("_delta", 0f);
        gameObject.SetActive(false);
        restartButtonClickListener.gameObject.GetComponent<SpriteRenderer>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    }

    public async UniTask doBatteryTurnOffSequence()
    {
        await DOTween.To((x) => _spriteRenderer.material.SetFloat(_progressField, x), 1f, 0f,  0.25f).ToUniTask();
        battery.SetActive(true);
        await battery.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetLoops(3, LoopType.Yoyo).ToUniTask();
        text.text = "<fade uniformity=0>Do you wish to charge and start again?</>";
        text.gameObject.SetActive(true);
        battery.SetActive(false);
        await restartButtonClickListener.gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f).ToUniTask();
        await restartButtonClickListener.observable.awaitClick();
    }
    
    public async UniTask doStressTurnOff()
    {
        await DOTween.To((x) => material.SetFloat(_progressField, x), 1f, 0f,  0.25f).ToUniTask();
        stress.SetActive(true);
        await stress.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetLoops(3, LoopType.Yoyo).ToUniTask();
        text.text = "<fade uniformity=0>Too much for today... Or maybe... One more?</>";
        text.gameObject.SetActive(true);
        stress.SetActive(false);
        await restartButtonClickListener.gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f).ToUniTask();
        await restartButtonClickListener.observable.awaitClick();
    }

    public async UniTask doWinSequence()
    {
        gameObject.SetActive(true);
        audioSource.Play();
        await DOTween.To((x) => material.SetFloat(_progressField, x), 1f, 0f,  0.25f).ToUniTask();
        Color bgColor;
        ColorUtility.TryParseHtmlString("#DAEDFE", out bgColor);
        Color textColor;
        ColorUtility.TryParseHtmlString("#141a0d", out textColor);
        await _spriteRenderer.DOColor(bgColor, 2.5f).ToUniTask();
        text.gameObject.SetActive(true);
        text.color = textColor;
        text.text = "Finally, you reached <wave>tranquility</>.<br> Now go, spend some time <wave>outside</wave>.";
        await restartButtonClickListener.observable.awaitClick();
    }
}
