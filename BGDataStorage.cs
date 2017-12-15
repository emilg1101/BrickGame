using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrickGameEmulator
{
    public class BGDataStorage
    {
        private readonly string _path;

        private Dictionary<string, int> data;
        
        public BGDataStorage(string name)
        {
            _path = Environment.CurrentDirectory + @"\"  + name;
            _init();
        }

        private void _init()
        {
            _checkFile();
            data = new Dictionary<string, int>();
            var lines = File.ReadAllLines(_path);

            foreach (var line in lines)
            {
                var splitLine = line.Split();
                data[splitLine[0]] = int.Parse(splitLine[1]);
            }
        }

        private void _checkFile()
        {
            if (File.Exists(_path)) return;
            using (var fs = File.Create(_path))
            {
                fs.Close();
            }
        }
        
        public int GetInt(string key, int def)
        {
            return data.ContainsKey(key) ? data[key] : def;
        }

        public void PutInt(string key, int value)
        {
            data[key] = value;
        }

        public void Remove(string key)
        {
            data.Remove(key);
        }

        public void Commit()
        {
            File.Delete(_path);

            var keyList = data.Select(x => x.Key).ToArray();

            File.WriteAllLines(_path, keyList.Select(key => key + " " + data[key]).ToArray());
        }
    }
}