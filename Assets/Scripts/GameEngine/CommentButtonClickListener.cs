using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommentButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    
    public async void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer click");
        await Game.encountersPresenter.toggleKeyboard();
    }
}
