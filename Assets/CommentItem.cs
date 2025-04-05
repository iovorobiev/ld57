using System;
using GameEngine;
using UnityEngine;

public class CommentItem : MonoBehaviour
{
    public bool isInHand;
    
    private async void OnMouseDown()
    {
        if (isInHand)
        {
            isInHand = false;
            await Game.commentView.claimComment(this);
        }
    }
}
