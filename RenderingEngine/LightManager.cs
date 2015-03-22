﻿using System.Collections.Generic;
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
            //mSceneManager.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }

        public void CreateWorldLight()
        {
            Light worldLight1 = mSceneManager.CreateLight("worldLight1");
            worldLight1.Type = Light.LightTypes.LT_POINT;
            worldLight1.Position = new Vector3(0, 150, 0);
            worldLight1.DiffuseColour = ColourValue.White;
            worldLight1.SpecularColour = ColourValue.White;


            Light worldLight2 = mSceneManager.CreateLight("worldLight2");
            worldLight2.Type = Light.LightTypes.LT_POINT;
            worldLight2.Position = new Vector3(1500, 150, 1500);
            worldLight2.DiffuseColour = ColourValue.White;
            worldLight2.SpecularColour = ColourValue.White;
        }


        public void CreatePointLight(string name, Vector3 position, ColourValue color)
        {
            Light pointLight = mSceneManager.CreateLight(name);
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = position;
            pointLight.DiffuseColour = color;
            pointLight.SpecularColour = color;
            PointLights.Add(name,pointLight);
        }

        public void CreateSpotLight(string name, Vector3 position, Vector3 direction, ColourValue color, Degree innerAngle, Degree outerAngle)
        {
            Light spotLight = mSceneManager.CreateLight(name);
            spotLight.Type = Light.LightTypes.LT_SPOTLIGHT;
            spotLight.Position = position;
            spotLight.Direction = direction;
            spotLight.SetSpotlightRange(innerAngle, outerAngle);
            spotLight.DiffuseColour = color;
            spotLight.SpecularColour = color;
        }
    }
}
