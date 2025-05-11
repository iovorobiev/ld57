using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeListener : MonoBehaviour, IPointerClickHandler
{
    public async void OnPointerClick(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Swiping!");
            await Game.encountersPresenter.OnSwipe();
        }
    }
}
