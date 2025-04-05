using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine;
using UnityEngine;
using Comment = GameEngine.Comments.Comment;

public class Keyboard : MonoBehaviour
{
    private List<Comment> currentHand = new();
    public List<GameObject> positionsForComments = new();

    private void Awake()
    {
        Game.keyboard = this;
    }

    public async UniTask OnShow()
    {
        if (currentHand.Count == 0)
        {
            for (int i = 0; i < Player.drawHandSize || Player.currentEncounterDeck.Count == 0; i++)
            {
                currentHand.Add(Player.currentEncounterDeck.Dequeue());
            }
        }

        for (int i = 0; i < currentHand.Count; i++)
        {
            var prefab = Resources.Load(currentHand[i].prefab) as GameObject;
            var comment = Instantiate(prefab, transform);
            comment.GetComponent<CommentItem>().isInHand = true;
            comment.transform.position = positionsForComments[i].transform.position;
        }
    }
}
