using Application;
using Application.DataAccessLayer;
using Database.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application
{
    public class Grid : Component
    {
        public Grid() : this(null, null) { }

        public Grid(Component owner, string text) 
            : base(owner, text)
        {

        }

        public void Load(Type typeRow)
        {
            object[] rowList = DataAccessLayer.Util.Select(typeRow);
            GridCellList = new List<List<Application.GridCell>>();
            var propertyInfoList = typeRow.GetTypeInfo().GetProperties();
            foreach (object row in rowList)
            {
                var gridCellList = new List<GridCell>();
                GridCellList.Add(gridCellList);
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    object value = propertyInfo.GetValue(row);
                    gridCellList.Add(new Application.GridCell() { FieldName = propertyInfo.Name, Value = value });
                }
            }
        }

        /// <summary>
        /// (Row, FieldName, GridCell)
        /// </summary>
        public List<List<GridCell>> GridCellList;
    }

    public class GridCell
    {
        public string FieldName;

        public object Value;
    }

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

        public int ResponseCount;
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

        public bool IsFocus;

        public string TextNew;

        public string AutoComplete;
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
            else
            {
                dataOut.ResponseCount += 1;
            }
            dataOut.Name = ".NET Core=" + DateTime.Now.ToString("HH:mm:ss.fff");
            dataOut.Name += " - " + DataAccessLayer.Util.Select(typeof(SyUser), 1).Cast<SyUser>().First().Name;
            dataOut.VersionServer = Util.VersionServer;
            Input input = (Input)dataOut.List[0].List[1].List[1].List[1];
            input.AutoComplete = input.TextNew?.ToUpper();
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
            var grid = new Grid(cellFooter1, "MyGrid");
            // grid.Load(typeof(SyUser)); 
            //
            return result;
        }
    }
}
