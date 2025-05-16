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
        await Game.encountersPresenter.OnSwipe();
    }
}
