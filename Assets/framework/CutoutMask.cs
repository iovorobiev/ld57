using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace framework
{
    public class CutoutMask : Image
    {
        public override Material materialForRendering
        {
            get
            {
                var material = new Material(base.materialForRendering);
                material.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
                return material;
            }
        }
    }
}