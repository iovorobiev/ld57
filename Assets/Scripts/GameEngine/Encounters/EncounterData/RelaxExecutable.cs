using System.Threading;
using Cysharp.Threading.Tasks;
using GameEngine.Comments.CommentsData;
using UnityEngine;

namespace GameEngine.Encounters.EncounterData
{
    public class RelaxExecutable : EncounterExecutable
    {
        private EncounterController encounterController;
        private readonly int stress;
        private readonly int charge;

        public RelaxExecutable(int stress, int charge)
        {
            this.stress = stress;
            this.charge = charge;
        }
        
        public async UniTask execute()
        {
            var encounterView = encounterController.view as RelaxEncounterView;
            var cancelSource = new CancellationTokenSource();
            Debug.Log("Waiting button click");
            var result = await UniTask.WhenAny(
                encounterView!.stressButton.OnClickAsync(cancelSource.Token),
                encounterView.batteryButton.OnClickAsync(cancelSource.Token),
                encounterView.commentButton.OnClickAsync(cancelSource.Token)
            );
            cancelSource.Cancel();
            Debug.Log("Clicked " + result);
            switch (result)
            {
                case 0:
                    await Player.receiveStressDamage(-stress);
                    break;
                case 1:
                    await Player.receivePowerDamage(-charge);
                    break;
                case 2:
                    var reward = CommentsBase.rollComments(3);
                    var choosenReward = await encounterView.showReward(reward, 0, 0);
                    await Player.addToVocabulary(choosenReward);
                    break;
            }

            await encounterView.finishEncounter();
            Debug.Log("Finished encounter");
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            encounterController = controller;
        }
    }
}