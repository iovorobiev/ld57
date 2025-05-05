using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine.EncounterData;
using GameEngine.ui;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class UI : MonoBehaviour
    {
        public GameObject uiContainer;
        private Vector3 originalContainerPosition;

        public GameObject likes;
        public Image likesProgress;
        public TextMeshProUGUI likesCount;
        public Image comments;

        public Transform openKeyboardPos;
        public Transform closeKeyboardPos;

        private float progress = 0f;

        private void Start()
        {
            originalContainerPosition = uiContainer.transform.position;
        }

        private void Update()
        {
            setCountersFromEncounter();
        }

        public void setLikesCount(int likes)
        {
            likesCount.text = likes.ToString();
        }

        private CancellationTokenSource progressSource = new();
    private void setCountersFromEncounter()
    {
        if (Game.currentEncounter == null) return;
        if (Game.currentEncounter.executable is EnemyExecutable executable)
        {
            var newProgress = (float) (executable.maxHp - executable.currentHp) / executable.maxHp;
            if (!Mathf.Approximately(newProgress, progress))
            {
                progressSource.Cancel();
                progressSource = new CancellationTokenSource();
                UniTask.WhenAll(
                    DOTween.To(x => likesProgress.fillAmount = x, progress, newProgress, 0.5f).ToUniTask(),
                    likes.transform.DOShakePosition(0.25f, new Vector3(0.1f, 0.1f)).ToUniTask()
                ).AttachExternalCancellation(progressSource.Token);
                
                progress = newProgress;
            }
            likesCount.text = (executable.maxHp - executable.currentHp).ToString();
        }
        else
        {
            likesCount.text = Game.currentEncounter.likes.ToString();
        }
    }

    public async UniTask openKeyboard()
    {
        await uiContainer.transform.DOMove(openKeyboardPos.position, 0.5f);
    }
    
    public async UniTask closeKeyboard()
    {
        await uiContainer.transform.DOMove(closeKeyboardPos.position, 0.5f);
    } 
    }
}