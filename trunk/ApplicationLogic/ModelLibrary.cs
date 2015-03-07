using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApplicationLogic
{
    [Serializable]
    public class ModelLibrary
    {
        public readonly string StoredModelsPath = ApplicationLogicResources.StoredModelsPath;
        public readonly string StoredMaterialsPath = ApplicationLogicResources.StoredMaterialPath;
        public readonly string StoredTexturesPath = ApplicationLogicResources.StoredTexturesPath;

        public readonly List<string> AllowedTexturesExtensions = new List<string> { "jpg", "png" }; 

        private readonly Dictionary<string, ModelData> AvailableModels;
        public ModelLibrary()
        {
            AvailableModels = new Dictionary<string, ModelData>();
        }

        public void ImportModel(string modelFolderPath)
        {
            var files = Directory.GetFiles(modelFolderPath);
            ModelData newModel = null;
            var materials = new List<string>();
            var textures = new List<string>();
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file);

                if (ext == "mesh")
                {
                    newModel = new ModelData(Path.GetFileName(file), Path.GetFullPath(file));
                    File.Copy(Path.GetFullPath(file), @StoredModelsPath,true);
                }
                if (ext == "material")
                {
                    var path = Path.GetFullPath(file);
                    materials.Add(path);
                    File.Copy(path, @StoredMaterialsPath, true);
                }
                if (AllowedTexturesExtensions.Contains(ext))
                {
                    var path = Path.GetFullPath(file); 
                    textures.Add(path);
                    File.Copy(path, @StoredTexturesPath, true);
                }
            }
            
            if (newModel == null) return;
            materials.ForEach(x => newModel.AddMaterial(x));
            textures.ForEach(x => newModel.AddTexture(x));
            AvailableModels.Add(newModel.Name,newModel);
        }

        public void RemoveModel(string modelName)
        {
            if (AvailableModels.ContainsKey(modelName))
            {
                ModelData toRemove = AvailableModels[modelName];
                File.Delete(toRemove.Path);
                foreach (var material in toRemove.Materials)
                {
                    File.Delete(material);
                }
                foreach (var texture in toRemove.Textures)
                {
                    File.Delete(texture);
                }
                AvailableModels.Remove(modelName);
            }
        }

        public List<string> GetAvailableModelsName()
        {
            var modelNames = AvailableModels.Select(availableModel => availableModel.Key).ToList();
            return modelNames;
        }

        public ModelData GetModel(string name)
        {
            if(AvailableModels.ContainsKey(name))
                throw new KeyNotFoundException(name + "NOT found");
            return AvailableModels[name];
        }
    }
}
