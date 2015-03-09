using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApplicationLogic
{
    [Serializable]
    public class ModelData 
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Materials { get; set; }
        public List<string> Textures { get; set; }

        public ModelData()
        {
            Name = "";
            Path = "";
            Materials = new List<string>();
            Textures = new List<string>();
        }

        public ModelData(string name, string path)
        {
            Name = name;
            Path = path;
            Materials = new List<string>();
            Textures = new List<string>();
        }

        public void AddTexture(string fileName)
        {
            Textures.Add(fileName);
        }

        public void AddMaterial(string fileName)
        {
            Materials.Add(fileName);
        }
    }
}
