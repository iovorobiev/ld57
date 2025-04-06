using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.Comments.CommentsData;
using GameEngine.Encounters;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyExecutable : EncounterExecutable
    {
        private int maxHp;
        private int currentHp;
        private EnemyEncounterController encounterController;

        public EnemyExecutable(int maxHp)
        {
            this.maxHp = maxHp;
            currentHp = 0;
        }

        public async UniTask setEncounterController(EncounterController encounterController)
        {
            this.encounterController = (EnemyEncounterController) encounterController;
            
            await UniTask.WhenAll(
                this.encounterController.changeCurrentHp(currentHp),
                this.encounterController.changeTotalHp(maxHp)
            );
        }

        public async UniTask receiveDamage(int dmg)
        {
            currentHp += dmg;
            encounterController.changeCurrentHp(currentHp);
        }

        public async UniTask execute()
        {
            await encounterController.showEncounter();
            await Player.receiveStressDamage(maxHp - currentHp);
            
            while (currentHp < maxHp && !Player.loseCondition() && !Player.winCondition())
            {
                var playersComment = await Game.encountersPresenter.playersComment();
                Debug.Log("Got comment");
                await playersComment.script.execute();
                Debug.Log("Script executed");

                if (playersComment.type == LikeInteractionType.REDUCE_STRESS)
                {
                    Debug.Log("Receiving damage");
                    await receiveDamage(playersComment.value);
                } 
                if (playersComment.type == LikeInteractionType.SKIP_TURN)
                {
                    Debug.Log("Receiving stress");
                    await Player.receiveStressDamage(maxHp - currentHp);
                }
            }
            var reward = CommentsBase.rollComments(3);
            var chosenReward = await encounterController.showReward(reward);
            await Player.addToVocabulary(chosenReward);
            await encounterController.finishEncounter();
        }
    }
}