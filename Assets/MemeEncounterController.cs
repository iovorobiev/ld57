using GameEngine;
using GameEngine.Encounters.EncounterData;
using TMPro;
using UnityEngine;

public class MemeEncounterController : EncounterController
{
    public GameObject myGameObject;
    public TextMeshProUGUI _text;
    
    public override void setEncounterData(Encounter encounter)
    {
        base.setEncounterData(encounter);
        var meme = encounter as MemeEncounter;
        var data = meme.getData() as MemeData;

        var obj = Resources.Load(data.prefabPath) as GameObject;
        Instantiate(obj, myGameObject.transform);

        _text.text = data.text;
    }
}
