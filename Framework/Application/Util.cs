﻿namespace Application
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public static class Util
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

        public static string FileRead(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static object ValueToJson(object value)
        {
            object result = value;
            if (value !=  null)
            {
                if (value.GetType() == typeof(int))
                {
                    result = Convert.ChangeType(value, typeof(double));
                }
            }
            return result;
        }
    }
}
