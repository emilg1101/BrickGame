using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrickGameEmulator
{
    public class BGDataStorage
    {
        private readonly string _path;

        private Dictionary<string, int> _data;
        
        public BGDataStorage(string name)
        {
            _path = name;
            _init();
        }

        private void _init()
        {
            _checkFile();
            _data = new Dictionary<string, int>();
            var lines = File.ReadAllLines(_path);

            foreach (var line in lines)
            {
                var splitLine = line.Split();
                _data[splitLine[0]] = int.Parse(splitLine[1]);
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
            return _data.ContainsKey(key) ? _data[key] : def;
        }

        public void PutInt(string key, int value)
        {
            _data[key] = value;
        }

        public void Remove(string key)
        {
            _data.Remove(key);
        }

        public void Commit()
        {
            File.Delete(_path);

            var keyList = _data.Select(x => x.Key).ToArray();

            File.WriteAllLines(_path, keyList.Select(key => key + " " + _data[key]).ToArray());
        }
    }
}