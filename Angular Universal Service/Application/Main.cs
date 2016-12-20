using System;

namespace Application
{
    public class Data
    {
        public string Name { get; set; }

        public Guid Session;

        public bool IsBrowser { get; set; }

        public string VersionServer { get; set; }

        public string VersionClient { get; set; }
    }

    public static class Main
    {
        public static Data Request(Data dataIn)
        {
            Data dataOut = Util.JsonObjectClone<Data>(dataIn);
            if (dataOut == null || dataOut.Session == Guid.Empty)
            {
                dataOut = new Data();
                dataOut.Session = Guid.NewGuid();
            }
            dataOut.Name = ".NET Core=" + DateTime.Now.ToString();
            dataOut.VersionServer = Util.VersionServer;
            return dataOut;
        }
    }
}
