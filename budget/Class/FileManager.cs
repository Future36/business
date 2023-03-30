using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.json
{
    static class FileManager
    {

        public static void MySerialize<T>(string path, T notes)
        {
            if (!File.Exists(path))
            {
                FileStream filestream = File.Create(path);
                filestream.Dispose();
            }
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(path, json);
        }

        static public List<T> ReadFromFile<T>(string path)
        {
            List<T> result = new List<T>();
            string resultInfo = File.ReadAllText(path);
            result = JsonConvert.DeserializeObject<List<T>>(resultInfo);
            return result;
        }
    
    }
}



