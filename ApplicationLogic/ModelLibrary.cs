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

        public List<string> AllowedTexturesExtensions = new List<string> { ".jpg", ".png" };

        public List<ModelData> AvailableModels;
        public ModelLibrary()
        {
            AvailableModels = new List<ModelData>();
        }

        public void ImportModel(string modelFolderPath)
        {
            var modelFiles = Directory.GetFiles(modelFolderPath,"*.mesh");
            var materialFiles = Directory.GetFiles(modelFolderPath,"*.material");
            var textureFiles = Directory.GetFiles(modelFolderPath);

            if (modelFiles.Length == 0)
            {
                throw new Exception("No .mesh file found in folder: " + modelFolderPath);
            }

            if (modelFiles.Length > 1)
            {
                throw new Exception("More than one .mesh file in folder: " + modelFolderPath);
            }
            
            var modelFileName = Path.GetFileName(modelFiles[0]);
           
            if (string.IsNullOrEmpty(modelFileName))
            {
                throw new NullReferenceException("Model file name is NULL");
            }

            var newModel = new ModelData(modelFileName, Path.Combine(@StoredModelsPath, modelFileName));
            File.Copy(Path.GetFullPath(modelFiles[0]), Path.Combine(@StoredModelsPath, modelFileName), true);

            if (materialFiles.Length < 1)
            {
                throw new Exception("No .material file found in folder: " + modelFolderPath);
                
            }

            foreach (var material in materialFiles)
            {
                var materialPath = Path.GetFullPath(material);
                if (string.IsNullOrEmpty(material))
                {
                    throw new NullReferenceException("Material file name is NULL");
                }
                
                File.Copy(materialPath, Path.Combine(@StoredMaterialsPath, Path.GetFileName(material)), true);
               
                newModel.AddMaterial( Path.Combine(@StoredMaterialsPath, Path.GetFileName(material)));
            }

            foreach (var texture in textureFiles)
            {
                var texturePath = Path.GetFullPath(texture);
                if (string.IsNullOrEmpty(Path.GetFileName(texture)))
                {
                    throw new NullReferenceException("Texture file name is NULL");
                }
                if (!AllowedTexturesExtensions.Contains(Path.GetExtension(texturePath))) continue;

                File.Copy(texturePath, Path.Combine(@StoredTexturesPath, Path.GetFileName(texture)), true);
                
                newModel.AddTexture(Path.Combine(@StoredTexturesPath, Path.GetFileName(texture)));
            }
            
            

            if (AvailableModels.Contains(newModel))
            {
                throw new Exception("Model with name: " + newModel+ " is already in the library");  
            }
            AvailableModels.Add(newModel);    
        }

        public void RemoveModel(string modelName)
        {
            if (AvailableModels.Find(x => x.Name == modelName) != null)
            {
                ModelData toRemove = AvailableModels.Find(x => x.Name == modelName);
                File.Delete(toRemove.Path);
                foreach (var material in toRemove.Materials)
                {
                    File.Delete(material);
                }
                foreach (var texture in toRemove.Textures)
                {
                    File.Delete(texture);
                }
                AvailableModels.Remove(toRemove);
            }
        }

        public List<string> GetAvailableModelsName()
        {
            var modelNames = AvailableModels.Select(availableModel => availableModel.Name).ToList();
            return modelNames;
        }

        public ModelData GetModel(string name)
        {
            if (String.IsNullOrEmpty(name)) return null;

            if (AvailableModels.Find(x => x.Name == name) == null)
                throw new KeyNotFoundException(name + "NOT found");
            return AvailableModels.Find(x => x.Name == name);
        }
    }
}
