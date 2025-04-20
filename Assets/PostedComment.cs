using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PostedComment : MonoBehaviour
{
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI likesCount;
    public GameObject likesImage;

    public async UniTask SetData(string text, int likes)
    {
        commentText.text = text;
        likesImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var scaleSequence = DOTween.Sequence();
        scaleSequence.Append(likesImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f));
        scaleSequence.Append(likesImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.05f));
        await UniTask.WhenAll(
            scaleSequence.Play().ToUniTask(),
            DOTween.To(x => likesCount.text = likes.ToString(), 0, likes, 0.2f).ToUniTask()
        );
    }

}
