using System;
using Cysharp.Threading.Tasks;
using GameEngine.EncounterData;
using TMPro;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class EnemyEncounterController : EncounterController
    {
        public TextMeshProUGUI currentHp;
        public TextMeshProUGUI totalHp;
        public async UniTask changeCurrentHp(int to)
        {
            currentHp.text = to.ToString();
        }
        
        public async UniTask changeTotalHp(int to)
        {
            totalHp.text = to.ToString();
        }
    }
}