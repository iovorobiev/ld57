using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class LiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId(123);
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0f, 0f, 5f), 0.25f).SetLoops(2, LoopType.Yoyo))
            .Append(transform.DORotate(new Vector3(0f, 0f, -5f), 0.25f).SetLoops(2, LoopType.Yoyo))
            .SetLoops(-1)
            .SetId(124);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(123);
        DOTween.Kill(124);
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.rotation = Quaternion.identity;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Do nothing
    }
}
