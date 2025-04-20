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
   private GameObject encounterContent;
   public UI ui;
   public CommentView commentView;
   
   public virtual void setEncounterData(Encounter encounter)
   {
      encounterData = encounter;
      encounterScript = encounter.executable;
      encounterScript.setEncounterController(this);
   }

   public async UniTask runExecutable()
   {
      await encounterScript.execute().AttachExternalCancellation(destroyCancellationToken);
   }

   public void clearComments()
   {
      commentView.clearComments();
   }
   
   public virtual async UniTask OnKeyboardOpened()
   {
      
   }

   public void InflateEncounter(Encounter encounter)
   {
      var prefab = Resources.Load(encounter.prefabPath) as GameObject;
      encounterContent = Instantiate(prefab, transform);
      var view = encounterContent.GetComponent<EncounterContentView>();
      view.setData(encounter.visData);
      setEncounterData(encounter);
   }

   public void destroy()
   {
      Destroy(encounterContent);
   }
}
