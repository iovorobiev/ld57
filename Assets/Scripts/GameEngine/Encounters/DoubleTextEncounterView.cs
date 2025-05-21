using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;
using TMPro;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class DoubleTextEncounterView : EnemyEncounterView
    {
        public TextMeshProUGUI botText;

        public GameObject sprite; 

        public override void setData(SpecificData data)
        {
            base.setData(data);
            if (data is not DoubleTextData enemyData)
            {
                return;
            }
            
            // topText.text = enemyData.topText;
            botText.text = enemyData.botText;
        }
    }
}