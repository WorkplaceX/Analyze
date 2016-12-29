using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Application
{
    public class Util
    {
        public static T JsonObjectClone<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }

        public static string VersionServer
        {
            get
            {
                return "v0.2 Server";
            }
        }
    }
}
