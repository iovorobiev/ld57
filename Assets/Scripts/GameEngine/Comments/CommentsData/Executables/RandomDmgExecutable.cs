using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameEngine.EncounterData;

namespace GameEngine.Comments.CommentsData
{
    public class RandomDmgExecutable : Executable
    {
        public Func<int> value;
        private readonly List<Tags> tags;
        private readonly Func<string> likesShow;

        public RandomDmgExecutable(Func<int> value,  List<Tags> tags = null, Func<string> likesShow = null)
        {
            this.value = value;
            this.tags = tags;
            this.likesShow = likesShow;
        }

        public async UniTask execute()
        {
            int dmg = Player.calculateLikesWithBonuses(value(), tags);
            Player.postedComments.Last().currentLikes = dmg;
            if (Game.currentEncounterController.encounterScript is EnemyExecutable)
            {
                await ((EnemyExecutable) Game.currentEncounterController.encounterScript).receiveDamage(dmg);
            }
        }

        public string getPrice(Executable.Resource resource)
        {
            if (resource != Executable.Resource.Likes) return null;
            if (likesShow == null)
            {
                return Player.calculateLikesWithBonuses(value(), tags).ToString();
            }

            return likesShow();
        }
    }
}