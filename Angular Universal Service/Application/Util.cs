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
            string json = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string VersionServer
        {
            get
            {
                return "v0.1 Server";
            }
        }
    }
}
