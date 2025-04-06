using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.EncounterData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace GameEngine.Encounters
{
    public class EnemyEncounterController : EncounterController
    {
        public GameObject encounterView;
        public GameObject rewardView;

        public List<Button> options;

        public Image icon;
        public GameObject stats;
        public TextMeshProUGUI currentHp;
        public TextMeshProUGUI totalHp;

        private CancellationTokenSource source = new();
        public async UniTask changeCurrentHp(int to)
        {
            currentHp.text = to.ToString();
        }
        
        public async UniTask changeTotalHp(int to)
        {
            totalHp.text = to.ToString();
        }

        public async UniTask showEncounter()
        {
            encounterView.SetActive(true);
            rewardView.SetActive(false);
            Debug.Log("Loading enemy");
            var enemy = await Resources.LoadAsync(EnemyEncounter.ENEMY_PATH + ((EnemyEncounter)encounterData).enemyPrefabPath) as GameObject;
            var enemyObject = Instantiate(enemy, encounterView.transform);
            Debug.Log("Encounter instantiated!");
            var encounterController = enemyObject.GetComponent<EncounterController>();
            if (encounterController != null)
            {
                encounterController.setEncounterData(encounterData);
            }
        }

        public async override UniTask OnKeyboardOpened()
        {
            icon.gameObject.SetActive(true);
            stats.SetActive(true);
        }

        public async UniTask<Comment> showReward(List<Comment> reward)
        {
            encounterView.SetActive(false);
            rewardView.SetActive(true);
            var allAwaitables = new List<UniTask<Comment>>();
            for (int i = 0; i < reward.Count; i++)
            {
                var listener = new AwaitableClickListener<Comment>();
                var comment = reward[i];
                options[i].GetComponentInChildren<TextMeshProUGUI>().text = comment.text;
                options[i].gameObject.SetActive(true);
                
                options[i].onClick.AddListener(() =>
                {
                    listener.notifyClick(comment);
                });
                allAwaitables.Add(listener.awaitClick().AttachExternalCancellation(cancellationToken: source.Token));
            }

            var (_, result) = await UniTask.WhenAny(allAwaitables);
            return result;
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