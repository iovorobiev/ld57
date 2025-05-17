using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeListener : MonoBehaviour, IPointerClickHandler
{
    List<RaycastResult> _hits = new();

    public async void OnPointerClick(PointerEventData eventData)
    {
        // _hits.Clear();
        // EventSystem.current.RaycastAll(eventData, _hits);
        // Debug.Log("Swipe " + _hits.Count);
        // bool shouldSwipe = _hits.FindAll((x) => x.gameObject.CompareTag("NoClick")).Count == 0;
        // if (shouldSwipe)
        // {
            await Game.encountersPresenter.OnSwipe();
        // }
    }
}
