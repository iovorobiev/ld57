using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Comments;
using GameEngine.Comments.CommentsData;
using GameEngine.Encounters;
using GameEngine.Encounters.EncounterData;
using GameEngine.OSUpgrades;
using UnityEngine;

namespace GameEngine.EncounterData
{
    public class EnemyExecutable : EncounterExecutable, BattleEncounter
    {
        public  int maxHp;
        public int currentHp;
        private EncounterController encounterController;

        public EnemyExecutable(int likes)
        {
            maxHp = likes;
            currentHp = 0;
        }

        public async UniTask setEncounterController(EncounterController encounterController)
        {
            this.encounterController = encounterController;
        }

        public int getMaxHp()
        {
            return maxHp;
        }

        public int getCurrentHp()
        {
            return currentHp;
        }

        public async UniTask receiveDamage(int dmg)
        {
            currentHp += dmg;
            encounterController.ui.setLikesCount(maxHp - currentHp);
        }

        public async UniTask execute()
        {
            currentHp = 0;
            while (currentHp < maxHp && !Player.loseCondition() && !Player.winCondition())
            {
                if (Game.currentEncounter.tags.Contains(Tags.Blocking) && Player.currentEncounterDeck.Count == 0 && Game.keyboard.isEmpty())
                {
                    Player.loseFlag = true;
                    break;
                }
                var playersComment = await Game.encountersPresenter.playersComment();
                await playersComment.script.execute();
            }

            var enemyView = (EnemyEncounterView)encounterController.view;
            if (!Player.loseCondition() && !Player.winCondition())
            {
                await Player.receiveStressDamage(-currentHp);
                Game.currentEncounterController.clearComments();
                await Game.encountersPresenter.closeKeyboard();
                var reward = CommentsBase.rollComments(3);
                var chosenReward = await enemyView.showReward(reward);
                await Player.addToVocabulary(chosenReward);
                await enemyView.finishEncounter();
            }
        }
    }
}