using System;
using System.Collections.Generic;

namespace Application
{
    public class Data : Component
    {
        public Data() 
            : base(null, "Data")
        {

        }

        public string Name;

        public Guid Session;

        public bool IsBrowser;

        public string VersionServer;

        public string VersionClient;
    }

    public class Component
    {
        public Component() : this(null, null) { }

        public Component(Component owner, string text)
        {
            Constructor(owner, text);
        }

        private void Constructor(Component owner, string text)
        {
            this.Type = GetType().Name;
            this.Text = text;
            if (owner != null)
            {
                if (owner.List == null)
                {
                    owner.List = new List<Component>();
                }
                int count = 0;
                foreach (var item in owner.List)
                {
                    if (item.Key.StartsWith(this.Type + "-"))
                    {
                        count += 1;
                    }
                }
                this.Key = this.Type + "-" + count.ToString();
                owner.List.Add(this);
            }
        }

        public string Key;

        public string Type;

        public string Text;

        public List<Component> List = new List<Component>();
    }

    public class LayoutContainer : Component
    {
        public LayoutContainer() : this(null, null) { }

        public LayoutContainer(Component owner, string text) 
            : base(owner, text)
        {

        }
    }

    public class LayoutRow : Component
    {
        public LayoutRow() : this(null, null) { }

        public LayoutRow(LayoutContainer owner, string text) 
            : base(owner, text)
        {

        }
    }

    public class LayoutCell : Component
    {
        public LayoutCell() : this(null, null) { }

        public LayoutCell(LayoutRow owner, string text) 
            : base(owner, text)
        {

        }
    }

    public class Button : Component
    {
        public Button() : this(null, null) { }

        public Button(Component owner, string text)
            : base(owner, text)
        {
            if (IsClick)
            {
                Text += "."; // TODO
            }
        }

        public bool IsClick;
    }

    public class Input : Component
    {
        public Input() : this(null, null) { }

        public Input(Component owner, string text)
            : base(owner, text)
        {

        }
    }

    public class Label : Component
    {
        public Label() : this(null, null) { }

        public Label(Component owner, string text)
            : base(owner, text)
        {

        }
    }

    public static class Main
    {
        public static Data Process(Data dataIn)
        {
            Data dataOut = Util.JsonObjectClone<Data>(dataIn);
            if (dataOut == null || dataOut.Session == Guid.Empty)
            {
                dataOut = DataCreate();
            }
            dataOut.Name = ".NET Core=" + DateTime.Now.ToString("HH:mm:ss.fff");
            dataOut.VersionServer = Util.VersionServer;
            return dataOut;
        }

        public static Data DataCreate()
        {
            Data result = new Data();
            result.Session = Guid.NewGuid();
            //
            var container = new LayoutContainer(result, "Container");
            var rowHeader = new LayoutRow(container, "Header");
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1");
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1");
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2");
            new Label(cellContent2, "Enter text");
            new Input(cellContent2, "MyTest");
            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter1 = new LayoutCell(rowFooter, "FooterCell1");
            var button = new Button(cellFooter1, "Hello");
            //
            return result;
        }
    }
}
