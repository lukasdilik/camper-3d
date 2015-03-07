using System.Collections.Generic;

namespace ApplicationLogic
{
    public class ModelData
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public List<string> Materials { get; private set; }
        public List<string> Textures { get; private set; }

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
