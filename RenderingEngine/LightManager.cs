using System.Collections.Generic;
using Mogre;

namespace RenderingEngine
{
    public class LightManager
    {
        private static LightManager mInstance;

        private readonly SceneManager mSceneManager = Engine.Engine.Instance.SceneManager;

        public ColourValue AmbientLightColor = new ColourValue(0.25f, 0.25f, 0.25f);
        public Dictionary<string, Light> WorldLights;
        public Dictionary<string,Light> PointLights;
        public Dictionary<string, Light> SpotLights;

        public static LightManager Instance
        {
            get { return mInstance ?? (mInstance = new LightManager()); }
        }

        public LightManager()
        {
            WorldLights = new Dictionary<string, Light>();
            PointLights = new Dictionary<string, Light>();
            SpotLights = new Dictionary<string, Light>();
            mSceneManager.AmbientLight = AmbientLightColor;
            

//            // Allow self shadowing (note: this only works in conjunction with the shaders defined above)
//             // Set the caster material which uses the shaders defined above
//            mSceneManager.ShadowTextureSelfShadow = true;
//             mSceneManager.SetShadowTextureCasterMaterial("Ogre/DepthShadowmap/Caster/Float");
//             // Set the pixel format to floating point
//             mSceneManager.SetShadowTexturePixelFormat(PixelFormat.PF_FLOAT32_R);
//             // You can switch this on or off, I suggest you try both and see which works best for you
//            mSceneManager.ShadowCasterRenderBackFaces = false;
//             // Finally enable the shadows using texture additive integrated
//            mSceneManager.ShadowTechnique = ShadowTechnique.SHADOWDETAILTYPE_INTEGRATED;
        }

        public void CreateWorldLight(){
            Light dirLight = mSceneManager.CreateLight("worldLight1");
            dirLight.Type = Light.LightTypes.LT_DIRECTIONAL;
            dirLight.Direction = new Vector3(0, -1, -1);
            dirLight.DiffuseColour = ColourValue.White;
            dirLight.SpecularColour = ColourValue.White;
        }


        public Light CreatePointLight(string name, Vector3 position, ColourValue color)
        {
            Light pointLight = mSceneManager.CreateLight(name);
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = position;
            pointLight.DiffuseColour = color;
            pointLight.SpecularColour = color;
            PointLights.Add(name,pointLight);
            return pointLight;
        }

        public Light CreateSpotLight(string name, Vector3 position, Vector3 direction, ColourValue color, Degree innerAngle, Degree outerAngle)
        {
            Light spotLight = mSceneManager.CreateLight(name);
            spotLight.Type = Light.LightTypes.LT_SPOTLIGHT;
            spotLight.Position = position;
            spotLight.Direction = direction;
            spotLight.SetSpotlightRange(innerAngle, outerAngle);
            spotLight.DiffuseColour = color;
            spotLight.SpecularColour = color;
            return spotLight;
        }
    }
}
