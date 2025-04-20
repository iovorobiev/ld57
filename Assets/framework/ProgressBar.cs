using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
   public GameObject progressView;
   private float fullWidth;
   public float progress = 1f;
   public TextMeshProUGUI text;
   private RectTransform _rectTransform;
   private float _duration = 0.5f;

   private void Start()
   {
      _rectTransform = progressView.GetComponent<RectTransform>();
      fullWidth = _rectTransform.sizeDelta.x;
   }

   public void setProgress(float to)
   {
      progress = to;
      _rectTransform.sizeDelta = new Vector2(to * fullWidth, _rectTransform.sizeDelta.y);
      text.text = ((int)(to * 100)).ToString();
   }
   
   public async UniTask animateProgress(float to)
   {
      await UniTask.WhenAll(
         _rectTransform.DOSizeDelta(new Vector2(to * fullWidth, _rectTransform.sizeDelta.y), _duration)
            .ToUniTask(),
         animateText(to)
      );
      progress = to;
   }

   private async UniTask animateText(float to)
   {
      int cur = (int)(progress * 100);
      int toInt = (int)(to * 100);
      await DOTween.To(x => text.text = ((int)x).ToString(CultureInfo.InvariantCulture), cur, toInt, _duration).ToUniTask();
   }
}
