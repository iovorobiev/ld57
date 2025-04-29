using System;
using GameEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using utils;

public class HideButton : MonoBehaviour, IPointerClickHandler
{
    public Sprite shown;
    public Sprite hidden;

    private SpriteRenderer _renderer;

    public bool shownState = true;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public async void toggleState()
    {
        if (Game.keyboard.state == Keyboard.KeyboardState.SHOWN)
        {
            _renderer.sprite = hidden;
            shownState = false;
            await Game.keyboard.closeInComments();
        }
        else if (Game.keyboard.state ==  Keyboard.KeyboardState.HIDDEN_COMMENTS)
        {
            _renderer.sprite = shown;
            shownState = true;
            await Game.keyboard.open();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        toggleState();
    }
}
