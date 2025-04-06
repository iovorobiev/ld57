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
        private string hint = "";
        private EnemyEncounterController encounterController;

        public EnemyExecutable(EnemyEncounter encounter)
        {
            maxHp = encounter.getLikes();
            hint = encounter.hint;
            currentHp = 0;
        }

        public async UniTask setEncounterController(EncounterController encounterController)
        {
            Debug.Log("Setting controller " + encounterController);
            this.encounterController = encounterController as EnemyEncounterController;
            await UniTask.WhenAll(
                this.encounterController!.changeCurrentHp(currentHp),
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
            if (hint != "")
            {
                UniTask.WaitForSeconds(0.2f);
                Game.hint.showHintAndLock(hint);
            }
            while (currentHp < maxHp && !Player.loseCondition() && !Player.winCondition())
            {
                var playersComment = await Game.encountersPresenter.playersComment();
                await playersComment.script.execute();

                if (playersComment.type == LikeInteractionType.REDUCE_STRESS)
                {
                    await receiveDamage(playersComment.value);
                } 
            }

            var reward = CommentsBase.rollComments(3);
            var chosenReward = await encounterController.showReward(reward);
            await Player.addToVocabulary(chosenReward);
            await encounterController.finishEncounter();

        }
    }
}