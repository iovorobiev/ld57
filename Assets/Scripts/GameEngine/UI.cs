using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        public Image likes;
        public TextMeshProUGUI likesCount;
        public Image comments;

        public Transform openKeyboardPos;
        public Transform closeKeyboardPos;

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

    private void setCountersFromEncounter()
        {
            if (Game.currentEncounter != null)
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