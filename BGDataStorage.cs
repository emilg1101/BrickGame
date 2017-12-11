using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrickGameEmulator
{
    public class BGDataStorage
    {
        private readonly string name;

        private Dictionary<string, int> data;
        
        public BGDataStorage(string name)
        {
            this.name = name;
            _init();
        }

        private void _init()
        {
            _checkFile();
            data = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines(name + ".txt");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split();
                data[line[0]] = int.Parse(line[1]);
            }
        }

        private void _checkFile()
        {
            if (!File.Exists(Environment.CurrentDirectory + "/" + name + ".txt"))
                File.Create(name + ".txt");
        }
        
        public int GetInt(string key, int def)
        {
            if (data.ContainsKey(key)) return data[key];
            return def;
        }

        public void PutInt(string key, int value)
        {
            data[key] = value;
        }

        public void Commit()
        {
            File.Delete(name + ".txt");

            List<string> lines = new List<string>();

            Dictionary<string,int>.KeyCollection keys = data.Keys;

            var keyList = data.Select(x => x.Key).ToArray();

            for (int i = 0; i < keyList.Length; i++)
            {
                lines.Add(keyList[i] + " " + data[keyList[i]]);
            }
            
            File.WriteAllLines(name + ".txt", lines.ToArray());
        }
    }
}