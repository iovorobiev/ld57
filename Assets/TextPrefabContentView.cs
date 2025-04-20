using GameEngine;
using GameEngine.Encounters;
using GameEngine.Encounters.EncounterData;
using TMPro;
using UnityEngine;

public class TextPrefabContentView : EncounterContentView
{
    public GameObject myGameObject;
    public TextMeshProUGUI _text;

    public override void setData(SpecificData data)
    {
        base.setData(data);
        var tpData = data as TextPrefabData;

        var obj = Resources.Load(tpData.prefabPath) as GameObject;
        Instantiate(obj, myGameObject.transform);

        _text.text = tpData.text;
    }
}
