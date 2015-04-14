using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mogre;

namespace RenderingEngine
{
    public class ColorMaterialManager
    {
        private const string FrustumGroupName = "frustum_materials";
        private const string DefaultGroupName = "default";

        public static readonly IList<ColourValue> VgaColors = new ReadOnlyCollection<ColourValue>
        (new List<ColourValue> { 
         new ColourValue(1f,0f,0f,0),
         //new ColourValue(1f,1f,0f,0),
         new ColourValue(0.5f,0.5f,0f,0),
         new ColourValue(0.75f,0.75f,0.75f,0),
         new ColourValue(0f,1f,0f,0),
         new ColourValue(0f,0.5f,0f,0),
         new ColourValue(0f,1f,1f,0),
         new ColourValue(0f,0f,1f,0),
         new ColourValue(0f,0f,0.5f,0),
         new ColourValue(1f,0f,1f,0),
        });

        private static ColorMaterialManager mInstance;

        private readonly Dictionary<ColourValue, string> mColorMaterials;
        private int mCounter;

        public static ColorMaterialManager Instance
        {
            get { return mInstance ?? (mInstance = new ColorMaterialManager()); }
        }

        public ColorMaterialManager()
        {
            mColorMaterials = new Dictionary<ColourValue, string>();
        }

        public void Init()
        {
            CreateDefaultGroup();
            CreateFrustumMaterialsGroup();
            CreateFrustumColorsMaterials();
        }

        private void CreateDefaultGroup()
        {
            if (!ResourceGroupManager.Singleton.ResourceGroupExists(DefaultGroupName))
            {
                ResourceGroupManager.Singleton.CreateResourceGroup(DefaultGroupName);
            }
        }

        private void CreateFrustumMaterialsGroup()
        {
            if (!ResourceGroupManager.Singleton.ResourceGroupExists(FrustumGroupName))
            {
                ResourceGroupManager.Singleton.CreateResourceGroup(FrustumGroupName);
            }
        }

        private void CreateFrustumColorsMaterials()
        {
            foreach (var vgaColor in VgaColors)
            {
                CreateNewSemiTransparentColorMaterial(vgaColor);
            }
        }

        public string GetSemiTransparentMaterial(ColourValue color)
        {
            return CreateNewSemiTransparentColorMaterial(color);
        }

        public string GetNextFrustumMaterialName(out ColourValue outColor)
        {
            outColor = VgaColors[mCounter];
            var materialName = mColorMaterials[VgaColors[mCounter]];
            mCounter++;
            mCounter %= VgaColors.Count;
            return materialName;
        }

        public string GetSolidColorMaterialName(float r, float g, float b, float a = 0)
        {
            var color = new ColourValue(r,g,b,a);
            if (mColorMaterials.ContainsKey(color))
            {
                return mColorMaterials[color];
            }
            return CreateNewSolidColorMaterial(color);
        }

        public string GetSolidColorMaterialName(ColourValue color)
        {
            if (mColorMaterials.ContainsKey(color))
            {
                return mColorMaterials[color];
            }
            return CreateNewSolidColorMaterial(color);
        }

        private string CreateNewSolidColorMaterial(ColourValue color)
        {
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float a = color.a;
            string newMaterialName = String.Format("solid_color_{0}_{1}_{2}_{3}", r, g, b, a);
            MaterialPtr materialPtr = MaterialManager.Singleton.Create(newMaterialName, DefaultGroupName);
            materialPtr.ReceiveShadows = false;
            materialPtr.GetTechnique(0).SetLightingEnabled(true);
            materialPtr.GetTechnique(0).GetPass(0).SetDiffuse(r, g, b, a);
            materialPtr.GetTechnique(0).GetPass(0).SetAmbient(r, g, b);
            materialPtr.GetTechnique(0).GetPass(0).SetSelfIllumination(r, g, b);
            materialPtr.Dispose();
            
            if (!mColorMaterials.ContainsKey(color))
            {
                mColorMaterials.Add(color, newMaterialName);    
            }
            
            return newMaterialName;
        }

        private string CreateNewSemiTransparentColorMaterial(ColourValue color)
        {
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float a = 0.5f;
            string newMaterialName = String.Format("solid_color_{0}_{1}_{2}_{3}_semi_trasparent", r, g, b, a);
            MaterialPtr materialPtr = MaterialManager.Singleton.Create(newMaterialName, DefaultGroupName);
            materialPtr.ReceiveShadows = false;
            materialPtr.GetTechnique(0).SetSceneBlending(SceneBlendType.SBT_TRANSPARENT_ALPHA);
            materialPtr.GetTechnique(0).SetLightingEnabled(true);
            materialPtr.GetTechnique(0).GetPass(0).SetDiffuse(r, g, b, a);
            materialPtr.GetTechnique(0).GetPass(0).SetAmbient(r, g, b);
            materialPtr.GetTechnique(0).GetPass(0).SetSelfIllumination(r, g, b);
            materialPtr.GetTechnique(0).GetPass(0).CullingMode = CullingMode.CULL_NONE;
            materialPtr.GetTechnique(0).GetPass(0).ManualCullingMode= ManualCullingMode.MANUAL_CULL_NONE;
            materialPtr.Dispose();
            
            if (!mColorMaterials.ContainsKey(color))
            {
                mColorMaterials.Add(color, newMaterialName);    
            }
            return newMaterialName;
        }
    }
}