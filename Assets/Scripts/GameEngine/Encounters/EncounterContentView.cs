using UnityEngine;

namespace GameEngine.Encounters
{
    public class EncounterContentView : MonoBehaviour
    {
        protected SpecificData data;
        
        public virtual void setData(SpecificData data)
        {
            this.data = data;
        }
    }
}