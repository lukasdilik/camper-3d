using System;
using System.Collections.Generic;

namespace ApplicationLogic.Scene.Seriaziable
{
    [Serializable]
    public class SceneEntity
    {
        public  int ModelCounterValue;
        public  int CameraCounterValue;
        public  int LightCounterValue;
        public  List<ModelEntity> Models;

        public SceneEntity()
        {
            Models = new List<ModelEntity>();
        }

        public void AddModel(ModelEntity model)
        {
            Models.Add(model);
        }
    }
}
