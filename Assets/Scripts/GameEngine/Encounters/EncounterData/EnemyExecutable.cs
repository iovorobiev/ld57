using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.Comments.CommentsData;
using GameEngine.Encounters;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyExecutable : EncounterExecutable
    {
        private int maxHp;
        private int currentHp;
        private EncounterController encounterController;

        public EnemyExecutable(int likes)
        {
            maxHp = likes;
            currentHp = 0;
        }

        public async UniTask setEncounterController(EncounterController encounterController)
        {
            Debug.Log("Setting controller " + encounterController);
            this.encounterController = encounterController;
            await UniTask.WhenAll(
                // this.encounterController!.changeCurrentHp(currentHp),
                // this.encounterController.changeTotalHp(maxHp)
            );
        }

        public async UniTask receiveDamage(int dmg)
        {
            currentHp += dmg;
            // encounterController.changeCurrentHp(currentHp);
        }

        public async UniTask execute()
        {
            // await encounterController.showEncounter();
            await Player.receiveStressDamage(maxHp - currentHp);
            while (currentHp < maxHp && !Player.loseCondition() && !Player.winCondition())
            {
                if (Game.currentEncounter.tags.Contains(Tags.Blocking) && Player.currentEncounterDeck.Count == 0 && Game.keyboard.isEmpty())
                {
                    Player.loseFlag = true;
                    break;
                }
                Debug.Log("Awaiting comment");
                var playersComment = await Game.encountersPresenter.playersComment();
                Debug.Log("Awaited comment");
                await playersComment.script.execute();
                Debug.Log("Executing comment script");
                if (playersComment.type == LikeInteractionType.REDUCE_STRESS)
                {
                    Debug.Log("Receiving damage");
                    await receiveDamage(Player.getLikesFromComment(playersComment));
                    Debug.Log("Damage received");
                } 
            }

            if (!Player.loseCondition())
            {
                await Player.receiveStressDamage(-currentHp);
                Game.currentEncounterController.clearComments();
                await Game.encountersPresenter.closeKeyboard();
                var reward = CommentsBase.rollComments(3);
                // var chosenReward = await encounterController.showReward(reward);
                // await Player.addToVocabulary(chosenReward);
            }
            // await encounterController.finishEncounter();
        }
    }
}