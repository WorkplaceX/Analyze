using System;
using System.Collections.Generic;

namespace Application
{
    public class Data 
    {
        public string Name { get; set; }

        public Guid Session;

        public bool IsBrowser { get; set; }

        public string VersionServer { get; set; }

        public string VersionClient { get; set; }

        public Component Component { get; set; }
    }

    public class Component
    {
        public Component(Component owner, string text)
        {
            this.List = new Dictionary<string, Component>();
            this.Type = GetType().Name;
            this.Text = text;
            if (owner != null)
            {
                int count = 0;
                foreach (var item in owner.List)
                {
                    if (item.Key.StartsWith(this.Type + "-"))
                    {
                        count += 1;
                    }
                }
                owner.List.Add(this.Type + "-" + count.ToString(), this);
            }
        }

        public string Type { get; set; }

        public string Text { get; set; }

        public Dictionary<string, Component> List { get; set; }
    }

    public class LayoutContainer : Component
    {
        public LayoutContainer(Component owner, string text) 
            : base(owner, text)
        {

        }
    }

    public class LayoutRow : Component
    {
        public LayoutRow(LayoutContainer owner, string text) 
            : base(owner, text)
        {

        }
    }

    public class LayoutCell : Component
    {
        public LayoutCell(LayoutRow owner, string text) 
            : base(owner, text)
        {

        }
    }

    public static class Main
    {
        public static Data Request(Data dataIn)
        {
            Data dataOut = Util.JsonObjectClone<Data>(dataIn);
            if (dataOut == null || dataOut.Session == Guid.Empty)
            {
                dataOut = DataCreate();
            }
            dataOut.Name = ".NET Core=" + DateTime.Now.ToString();
            dataOut.VersionServer = Util.VersionServer;
            return dataOut;
        }

        public static Data DataCreate()
        {
            Data result = new Data();
            result.Session = Guid.NewGuid();
            //
            var container = new LayoutContainer(null, "Container");
            var rowHeader = new LayoutRow(container, "Header");
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1");
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1");
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2");
            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter1 = new LayoutCell(rowFooter, "FooterCell1");
            //
            result.Component = container;
            return result;
        }
    }
}
