using System;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData.Executables
{
    public class TempEffectExecutable : Executable
    {
        private int batteryCost;
        private int stressCost;
        private Length length;
        private TempEffect _tempEffect;

        public TempEffectExecutable(Length length, TempEffect effect)
        {
            this.length = length;
            this._tempEffect = effect;
        }

        public async UniTask execute()
        {
            switch (length)
            {
                case Length.COMMENT:
                    Player.nextComment.Add(_tempEffect);
                    break;
                case Length.REFRESH:
                    Player.nextRefresh.Add(_tempEffect);
                    break;
                case Length.ENCOUNTER:
                    Player.nextEncounter.Add(_tempEffect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string getPrice(Executable.Resource resource)
        {
            return null;
        }

        public enum Length
        {
            COMMENT,
            REFRESH,
            ENCOUNTER,
        }
    }
}