using Newtonsoft.Json;

namespace Server
{
    public class ConnectionString
    {
        public string Local { get; set; }

        public string Remote { get; set; }
    }

    public static class ConnectionManager
    {
        public static ConnectionString ConnectionString
        {
            get
            {
                string json = Util.FileRead("ConnectionString.json"); // See also .gitignore
                ConnectionString result = JsonConvert.DeserializeObject<ConnectionString>(json);
                return result;
            }
        }
    }
}
