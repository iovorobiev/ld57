using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.OSUpgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class TurnOffSequence : MonoBehaviour
{
    public List<Button> options = new();
    public TextMeshProUGUI text;
    private CancellationTokenSource source = new();

    private void Awake()
    {
        Game.turnOffSequence = this;
        gameObject.SetActive(false);
    }

    public async UniTask doTurnOffSequence()
    {
        var upgrades = OSUpgradesBase.getRandomOsUpgrades(options.Count);
        var allAwaitables = new List<UniTask<OSUpgrade>>();
        for (int i = 0; i < upgrades.Count; i++)
        {
            var listener = new AwaitableClickListener<OSUpgrade>();
            var upgrade = upgrades[i];
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = upgrade.text;
            options[i].gameObject.SetActive(true);
                
            options[i].onClick.AddListener(() =>
            {
                listener.notifyClick(upgrade);
            });
            allAwaitables.Add(listener.awaitClick().AttachExternalCancellation(cancellationToken: source.Token));
        }
        var (_, result) = await UniTask.WhenAny(allAwaitables);
        var alreadyHas = 0;
        if (result.maxRepeats != -1)
        {
            var alreadyHasList = Player.upgrades.FindAll((up) => result.upgradeID == up.upgradeID);
            alreadyHas = alreadyHasList.Count;
        }

        if (alreadyHas < result.maxRepeats)
        {
            Player.AddOSUpgrade(result);
        }
        else
        {
            Game.hint.showHint("I already maxed this out, shoot!");
        }
        // source.Cancel();
    }

    public async UniTask doWinSequence()
    {
        gameObject.SetActive(true);
        text.text = "Finally, you reached tranquility. Now go, spend some time outside.";
        foreach (var option in options)
        {
            option.gameObject.SetActive(false);
        }
    }
}
