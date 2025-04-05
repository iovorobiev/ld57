using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameEngine;
using UnityEngine;

public class SwipeListener : MonoBehaviour
{
    public async void OnMouseDown()
    {
        Debug.Log("Mouse click");
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            await Game.encountersPresenter.OnSwipe();
        }
    }
}
