using System;
using System.Threading;
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
   public AudioSource source;
   private AudioClip clip;

   private void Start()
   {
      source = GetComponent<AudioSource>();
   }

   public virtual void setEncounterData(Encounter encounter)
   {
    
      encounterData = encounter;
      encounterScript = encounter.executable;
      encounterScript.setEncounterController(this);
      source.Stop();
   }

   public async UniTask runExecutable()
   {
      if (encounterData.audioPath != null)
      {
         clip = await Resources.LoadAsync(encounterData.audioPath).ToUniTask() as AudioClip;
         source.PlayOneShot(clip);
      }
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
      view = encounterContent.GetComponent<EncounterContentView>();
      view.setData(encounter.visData);
      setEncounterData(encounter);
   }

   public void destroy()
   {
      Destroy(encounterContent);
   }
}
