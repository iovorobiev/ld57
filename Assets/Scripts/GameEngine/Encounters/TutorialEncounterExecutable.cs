using Cysharp.Threading.Tasks;
using GameEngine.Comments.CommentsData;
using GameEngine.Encounters.EncounterData;
using UnityEngine;

namespace GameEngine.Encounters
{
    public class TutorialEncounterExecutable : EncounterExecutable, BattleEncounter
    {
        public  int maxHp;
        public int currentHp;
        private EncounterController encounterController;
        private Tutorial.TutorialSequence _tutorialSequence;

        public TutorialEncounterExecutable(Tutorial.TutorialSequence tutorialSequence, int likes)
        {
            _tutorialSequence = tutorialSequence;
            maxHp = likes;
        } 
        
        public async UniTask execute()
        {
            Debug.Log("Awaiting battle!");
            // Tell about comment button
            await _tutorialSequence.showNextTip();
            // Ask to press
            await _tutorialSequence.showNextTip();
            await Game.keyboard.waitForOpen();
            // Tell about goal of fight
            await _tutorialSequence.showNextTip();
            // Tell about comments
            await _tutorialSequence.showNextTip();
            Game.tutorialView.hide();
            var chosenComment = await Game.encountersPresenter.playersComment();
            await chosenComment.script.execute();
            await UniTask.WaitForSeconds(0.5f);
            // Tell about battery cost
            await _tutorialSequence.showNextTip();
            Game.tutorialView.hide();
            chosenComment = await Game.encountersPresenter.playersComment();
            await chosenComment.script.execute();
            // Tell about refresh
            await _tutorialSequence.showNextTip();
            chosenComment = await Game.encountersPresenter.playersComment();
            Game.tutorialView.hide();
            await chosenComment.script.execute();
            await UniTask.WaitForSeconds(0.5f);
            // Tell about inventory
            await _tutorialSequence.showNextTip();
            
            await Game.screenController.waitForVocabulary();
            Game.tutorialView.hide();
            await Game.screenController.waitForEncounter();
            await _tutorialSequence.showNextTip();
            await Game.keyboard.waitForOpen();
            Game.tutorialView.hide();
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
            
            var enemyView = (DoubleTextEncounterView) encounterController.view;
            if (!Player.loseCondition())
            {
                await Player.receiveStressDamage(-currentHp);
                Game.currentEncounterController.clearComments();
                await Game.encountersPresenter.closeKeyboard();
                await _tutorialSequence.showNextTip();
                Game.tutorialView.hide();
                var reward = CommentsBase.rollComments(3);
                var chosenReward = await enemyView.showReward(reward);
                await Player.addToVocabulary(chosenReward);
            }
            await enemyView.finishEncounter();
        }

        public async UniTask setEncounterController(EncounterController controller)
        {
            encounterController = controller;
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
            Debug.Log("Adjust dmg");
            currentHp += dmg;
            encounterController.ui.setLikesCount(maxHp - currentHp);
        }
    }
}