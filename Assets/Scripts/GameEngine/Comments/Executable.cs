using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameEngine.Comments
{
    public interface Executable
    {
        UniTask execute();

        string getPrice(Resource resource);
        
        enum Resource
        {
            Likes,
            Battery,
            Stress,
        }
    }
}