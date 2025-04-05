using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.Comments;
using GameEngine.EncounterData;
using GameEngine.Encounters;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
   protected Encounter encounterData;
   protected EncounterExecutable encounterScript;
   
   public void setEncounterData(Encounter encounter)
   {
      encounterData = encounter;
      encounterScript = encounter.getScript();
      encounterScript.setEncounterController(this);
   }

   public async UniTask runExecutable()
   {
      await encounterScript.execute().AttachExternalCancellation(destroyCancellationToken);
   }

   public void destroy()
   {
      Destroy(gameObject);
   }
}
