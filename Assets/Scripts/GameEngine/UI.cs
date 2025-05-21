using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;
using GameEngine.ui;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        public Image skip;
        public TMP_Text skipText;

        public TMP_Text tag;

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
        if (Game.currentEncounter.tags.Contains(Tags.NO_COMMENTS))
        {
            comments.gameObject.SetActive(false);
        }
        else
        {
            comments.gameObject.SetActive(true);
        }

        if (Game.currentEncounter.tags.Contains(Tags.Blocking))
        {
            tag.text = "<color=#82679E>#blocking</color>: blocks scrolling if <sprite=0> above 0";
        } else if (Game.currentEncounter.tags.Contains(Tags.Stressful))
        {
            tag.text = "<color=#82679E>#stressful</color>: +" + Game.currentEncounter.likes + "% <sprite=23> if scrolled when <sprite=0> above 0";
        }
        
        uiContainer.gameObject.SetActive(!Game.currentEncounter.tags.Contains(Tags.NO_UI));
        if (Game.currentEncounter.tags.Contains(Tags.Stressful))
        {
            skipText.text = "+" + Game.currentEncounter.likes + "%";
            skip.gameObject.SetActive(true);
            skipText.gameObject.SetActive(true);
        }
        else
        {
            skipText.text = "+0%";
        }

        if (Game.currentEncounter.tags.Contains(Tags.Blocking))
        {
            skip.gameObject.SetActive(false);
            skipText.gameObject.SetActive(false);
        }
        if (Game.currentEncounter.executable is BattleEncounter executable)
        {
            var newProgress = (float) (executable.getMaxHp() - executable.getCurrentHp()) / executable.getMaxHp();
            if (!Mathf.Approximately(newProgress, progress))
            {
                progressSource.Cancel();
                progressSource = new CancellationTokenSource();
                UniTask.WhenAll(
                    DOTween.To(x => likesProgress.fillAmount = x, progress, newProgress, 0.5f).ToUniTask(),
                    likes.transform.DOShakePosition(0.5f, new Vector3(0.25f, 0.25f)).ToUniTask()
                ).AttachExternalCancellation(progressSource.Token);
                
                progress = newProgress;
            }
            likesCount.text = (executable.getMaxHp() - executable.getCurrentHp()).ToString();
            if (executable.getCurrentHp() >= executable.getMaxHp())
            {
                skipText.text = "+0%";
            }
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