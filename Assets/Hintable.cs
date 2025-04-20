using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hintable : MonoBehaviour, IPointerEnterHandler
{
    private string hint;

    public void SetStringToHint(string hint)
    {
        this.hint = hint;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {  
    }
}
