using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameEngine;
using UnityEngine;

public class SwipeListener : MonoBehaviour
{
    private async void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            await Game.encountersPresenter.OnSwipe();
        }
    }
}
