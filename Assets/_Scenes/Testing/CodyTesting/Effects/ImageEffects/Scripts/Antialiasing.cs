using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    public enum AAMode
    {
        
        SSAA = 0,
        
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof (Camera))]
    [AddComponentMenu("Image Effects/Other/Antialiasing")]
    public class Antialiasing : PostEffectsBase
    {
        public AAMode mode = AAMode.SSAA;

        public bool showGeneratedNormals = false;
        public float offsetScale = 0.2f;
        public float blurRadius = 18.0f;

        public float edgeThresholdMin = 0.05f;
        public float edgeThreshold = 0.2f;
        public float edgeSharpness = 4.0f;

        

        public Shader ssaaShader;
        private Material ssaa;


        public Material CurrentAAMaterial()
        {
            Material returnValue = null;

            switch (mode)
            {
               
                case AAMode.SSAA:
                    returnValue = ssaa;
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return returnValue;
        }


        public override bool CheckResources()
        {
            CheckSupport(false);

           
            ssaa = CreateMaterial(ssaaShader, ssaa);
            

            if (!ssaaShader.isSupported)
            {
                NotSupported();
                ReportAutoDisable();
            }

            return isSupported;
        }


        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CheckResources() == false)
            {
                Graphics.Blit(source, destination);
                return;
            }


            if (mode == AAMode.SSAA && ssaa != null)
            {
				// ----------------------------------------------------------------
                // SSAA antialiasing
                Graphics.Blit(source, destination, ssaa);
            }
           
            else
            {
                // none of the AA is supported, fallback to a simple blit
                Graphics.Blit(source, destination);
            }
        }
    }
}
