using System;

namespace Application
{
    public class Data
    {
        public string Name { get; set; }

        public Guid Session;
    }

    public static class Main
    {
        public static Data Request(Data data)
        {
            Data result = null;
            if (data == null || data.Session == Guid.Empty)
            {
                result = new Data();
                result.Session = Guid.NewGuid();
            }
            else
            {
                result = data;
            }
            result.Name += ".NET Core=" + DateTime.Now.ToString();
            return result;
        }
    }
}
