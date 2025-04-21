using Cysharp.Threading.Tasks;

namespace GameEngine.Comments.CommentsData
{
    public class AddStressExecutable : Executable
    {
        private int stress;

        public AddStressExecutable(int stress)
        {
            this.stress = stress;
        }

        public async UniTask execute()
        {
            await Player.receiveStressDamage(stress);
        }

        public string getPrice(Executable.Resource resource)
        {
            return resource == Executable.Resource.Stress ? stress.ToString() : null;
        }
    }
}