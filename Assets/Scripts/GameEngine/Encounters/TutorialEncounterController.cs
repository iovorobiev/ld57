using Cysharp.Threading.Tasks;
using TMPro;

namespace GameEngine.Encounters
{
    public class TutorialEncounterController : EncounterController
    {
        public TextMeshProUGUI mainText;
        public TextMeshProUGUI callToAction;

        public async UniTask showMaintext(string text)
        {
            mainText.SetText(text);
        }
        
        public async UniTask showCallToAction(string text)
        {
            callToAction.SetText(text);
        }
        
    }
}