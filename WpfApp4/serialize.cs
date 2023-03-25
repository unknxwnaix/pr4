using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    internal class serialize
    {
        public static void Serialize<T>(T list) 
        {
            string json = JsonConvert.SerializeObject(list);
            string path = "C:\\заметки\\result.json";
            File.WriteAllText(path,json);
        }
        public static T Deserialize<T>()
        {
            string path = "C:\\заметки\\result.json";
            string json = File.ReadAllText(path); ;
            T list = JsonConvert.DeserializeObject<T>(json);
            return list;
        }
    }
}
