using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace GameEngine.Encounters
{
    public class EnemyEncounterView : EncounterContentView
    {
        public GameObject encounterView;
        public GameObject rewardView;

        public List<CommentItem> options;

        private CancellationTokenSource source = new();
        
        public async UniTask<Comment> showReward(List<Comment> reward)
        {
            encounterView.SetActive(false);
            rewardView.SetActive(true);
            var allAwaitables = new List<UniTask<CommentItem>>();
            for (int i = 0; i < reward.Count; i++)
            {
                var comment = reward[i];
                
                options[i].gameObject.SetActive(true);
                options[i].setComment(comment);
                allAwaitables.Add(options[i].clickListener.awaitClick());
            }

            var (_, result) = await UniTask.WhenAny(allAwaitables);
            return result.comment;
        }

        public async UniTask finishEncounter()
        {
            foreach (var option in options)
            {
                option.gameObject.SetActive(false);
            }
            source.Cancel();
        }
    }
}