using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;
using TMPro;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class DoubleTextEnemyView : EncounterController
    {
        public TextMeshProUGUI topText;
        public TextMeshProUGUI botText;

        public override void setEncounterData(Encounter encounter)
        {
            var data = encounter.getData();
            if (data is not DoubleTextData enemyData)
            {
                return;
            }

            topText.text = enemyData.topText;
            botText.text = enemyData.botText;
        }
    }
}