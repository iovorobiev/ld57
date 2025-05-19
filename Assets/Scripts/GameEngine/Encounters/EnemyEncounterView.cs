using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        public GameObject stressView;
        
        public List<CommentItem> options;
        private GameObject inflatedObject;

        public TMP_Text stressValue;
        public GameObject stressObject;

        private CancellationTokenSource source = new();

        public override void setData(SpecificData data)
        {
            base.setData(data);
            if (data is TextPrefabData textPrefabData)
            {
                var prefab = Resources.Load(textPrefabData.prefabPath) as GameObject;
                Destroy(inflatedObject);
                inflatedObject = Instantiate(prefab, encounterView.transform);
                encounterView.SetActive(true);
            }
        }

        public async UniTask<Comment> showReward(List<Comment> reward, int prevStress, int newStress)
        {
            encounterView.SetActive(false);
            if (prevStress != newStress)
            {
                Game.encountersPresenter.blockSwipe = true;
                if (stressView != null)
                {
                    stressView.SetActive(true);
                }

                await UniTask.WhenAll(
                    stressObject.transform.DOScale(new Vector3(stressObject.transform.localScale.x + 1f, stressObject.transform.localScale.y + 1f, stressObject.transform.localScale.z + 1f), 0.5f).SetLoops(3, LoopType.Yoyo).ToUniTask(),
                    DOTween.To((x) => stressValue.text = (int)x + "%", prevStress, newStress, 1.5f).ToUniTask()
                );    
                Game.encountersPresenter.blockSwipe = false;
            }
            
            return await showCommentsChoice(reward);
        }

        private async Task<Comment> showCommentsChoice(List<Comment> reward)
        {
            rewardView.SetActive(true);
            if (stressView != null)
            {
                stressView.SetActive(false);
            }
            Comment resultComment;
            var allAwaitables = new List<UniTask<CommentItem>>();
            for (int i = 0; i < reward.Count; i++)
            {
                var comment = reward[i];
                
                options[i].gameObject.SetActive(true);
                options[i].setComment(comment);
                allAwaitables.Add(options[i].clickListener.awaitClick());
            }

            var (_, result) = await UniTask.WhenAny(allAwaitables);
            resultComment = result.comment;
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