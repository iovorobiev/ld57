using System;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class UI : MonoBehaviour
    {
        public Image likes;
        public TextMeshProUGUI likesCount;
        public Image comments;
        
        private void Update()
        {
            setCountersFromEncounter();
        }

        private void setCountersFromEncounter()
        {
            likesCount.text = Game.currentEncounter.getLikes().ToString();
        }
    }
}