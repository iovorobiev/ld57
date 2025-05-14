using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommentButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    
    public async void OnPointerClick(PointerEventData eventData)
    {
        await Game.encountersPresenter.toggleKeyboard();
    }
}
