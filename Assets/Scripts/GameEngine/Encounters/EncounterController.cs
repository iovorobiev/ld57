using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.Comments;
using GameEngine.EncounterData;
using GameEngine.Encounters;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
   protected Encounter encounterData;
   public EncounterExecutable encounterScript;
   private GameObject encounterContent;
   public UI ui;
   public CommentView commentView;
   public EncounterContentView view;
   
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
      if (prefab == null)
      {
         Debug.Log(encounter.prefabPath);
      }
      encounterContent = Instantiate(prefab, transform);
      view = encounterContent.GetComponent<EncounterContentView>();
      view.setData(encounter.visData);
      setEncounterData(encounter);
   }

   public void destroy()
   {
      Destroy(encounterContent);
   }
}
