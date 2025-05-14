using UnityEngine;
using UnityEngine.EventSystems;
using utils;

namespace GameEngine
{
    public class RestartButtonClickListener : MonoBehaviour, IPointerClickHandler
    {
        public AwaitableClickListener<GameObject> observable = new();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            observable.notifyClick(gameObject);
        }
    }
}