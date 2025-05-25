using Cysharp.Threading.Tasks;
using GameAnalyticsSDK;
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
            currentHp = 0;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Tutorial Encounter");
            Game.keyboard.hideRefresh();
            // Tell about comment button
            await _tutorialSequence.showNextTip();
            // Ask to press
            await _tutorialSequence.showNextTip();
            await Game.keyboard.waitForOpen();
            await ((EnemyEncounterView)Game.currentEncounterController.view).showDescription();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Tutorial Encounter", "keyboard opened");
            // Tell about goal of fight
            await _tutorialSequence.showNextTip();
            // Tell about comments
            await _tutorialSequence.showNextTip();
            var chosenComment = await Game.encountersPresenter.playersComment();
            Game.tutorialView.hide();
            await chosenComment.script.execute();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Tutorial Encounter", "first comment");
            await _tutorialSequence.showNextTip();
            await UniTask.WaitForSeconds(0.5f);
            // Tell about battery cost
            await _tutorialSequence.showNextTip();
            await _tutorialSequence.showNextTip();
            chosenComment = await Game.encountersPresenter.playersComment();
            Game.tutorialView.hide();
            await chosenComment.script.execute();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Tutorial Encounter", "Second comment");
            await UniTask.WaitForSeconds(0.25f);
            // Tell about refresh
            await _tutorialSequence.showNextTip();
            await Game.keyboard.showRefresh();
            chosenComment = await Game.encountersPresenter.playersComment();
            Game.tutorialView.hide();
            await chosenComment.script.execute();
            await UniTask.WaitForSeconds(0.5f);
            // Tell about inventory
            await _tutorialSequence.showNextTip();
            await Game.screenController.waitForVocabulary();
            await _tutorialSequence.showNextTip();
            await Game.screenController.waitForEncounter();
            await _tutorialSequence.showNextTip();
            await Game.keyboard.waitForOpen();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Tutorial Encounter", "Open battle");
            Game.tutorialView.hide();
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
            await ((EnemyEncounterView)Game.currentEncounterController.view).hideDescription();

            var enemyView = (EnemyEncounterView) encounterController.view;
            if (!Player.loseCondition())
            {
                var prev = Player.stressLevel;
                await Player.receiveStressDamage(-currentHp);
                Game.currentEncounterController.clearComments();
                await Game.encountersPresenter.closeKeyboard();
                await _tutorialSequence.showNextTip();
                Game.tutorialView.hide();
                var reward = CommentsBase.rollComments(3);
                var chosenReward = await enemyView.showReward(reward, prev, Player.stressLevel);
                await Player.addToVocabulary(chosenReward);
            }
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Tutorial Encounter");
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