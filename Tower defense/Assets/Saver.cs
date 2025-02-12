using System;
using System.IO;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    internal class Saver<T>
    {
        private static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }
        public static void TryLoad(string filename, ref T data)
        {
            var path = Path(filename);
            if (File.Exists(path))
            {                
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;
            }            
        }

        public static void Save(string filename, T data)
        {            
            var wrapper = new Saver<T> { data = data};
            var dataString = JsonUtility.ToJson(wrapper);
            
            File.WriteAllText(Path(filename), dataString);
        }

        public T data;
        
                
    }
}