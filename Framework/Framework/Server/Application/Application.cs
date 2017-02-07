namespace Framework.Server.Application
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class Grid : Component
    {
        public Grid() : this(null, null) { }

        public Grid(Component owner, string text)
            : base(owner, text)
        {

        }

        private List<GridColumn> LoadColumnList(Type typeRow)
        {
            var result = new List<GridColumn>();
            //
            var cellList = Framework.Server.DataAccessLayer.Util.ColumnList(typeRow);
            double widthPercentTotal = 0;
            bool isLast = false;
            for (int i = 0; i < cellList.Count; i++)
            {
                // Text
                string text = cellList[i].FieldName;
                cellList[i].ColumnText(ref text);
                // WidthPercent
                isLast = i == cellList.Count;
                double widthPercentAvg = Math.Round(((double)100 - widthPercentTotal) / ((double)cellList.Count - (double)i), 2);
                double widthPercent = widthPercentAvg;
                cellList[i].ColumnWidthPercent(ref widthPercent);
                widthPercent = Math.Round(widthPercent, 2);
                if (isLast)
                {
                    widthPercent = 100 - widthPercentTotal;
                }
                else
                {
                    if (widthPercentTotal + widthPercent > 100)
                    {
                        widthPercent = widthPercentAvg;
                    }
                }
                widthPercentTotal = widthPercentTotal + widthPercent;
                result.Add(new GridColumn() { FieldName = cellList[i].FieldName, Text = text, WidthPercent = widthPercent });
            }
            return result;
        }

        public void Load(Type typeRow)
        {
            RowList = new List<GridRow>();
            ColumnList = LoadColumnList(typeRow);
            CellList = new Dictionary<string, Dictionary<string, GridCell>>();
            //
            object[] rowList = Framework.Server.DataAccessLayer.Util.Select(typeRow, 0, 20);
            var propertyInfoList = typeRow.GetTypeInfo().GetProperties();
            for (int index = 0; index < rowList.Length; index++)
            {
                object row = rowList[index];
                RowList.Add(new GridRow() { Index = index.ToString() });
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    string fieldName = propertyInfo.Name;
                    object value = propertyInfo.GetValue(row);
                    object valueJson = Framework.Server.DataAccessLayer.Util.ValueToJson(value);
                    if (!CellList.ContainsKey(fieldName))
                    {
                        CellList[fieldName] = new Dictionary<string, GridCell>();
                    }
                    CellList[fieldName][index.ToString()] = new GridCell() { V = valueJson };
                }
            }
        }

        public List<GridRow> RowList;

        public List<GridColumn> ColumnList;

        /// <summary>
        /// (FieldName, Index, GridCell)
        /// </summary>
        public Dictionary<string, Dictionary<string, GridCell>> CellList;
    }

    public class GridData
    {
        private List<GridColumn> LoadColumnList(Type typeRow)
        {
            var result = new List<GridColumn>();
            //
            var cellList = Framework.Server.DataAccessLayer.Util.ColumnList(typeRow);
            double widthPercentTotal = 0;
            bool isLast = false;
            for (int i = 0; i < cellList.Count; i++)
            {
                // Text
                string text = cellList[i].FieldName;
                cellList[i].ColumnText(ref text);
                // WidthPercent
                isLast = i == cellList.Count;
                double widthPercentAvg = Math.Round(((double)100 - widthPercentTotal) / ((double)cellList.Count - (double)i), 2);
                double widthPercent = widthPercentAvg;
                cellList[i].ColumnWidthPercent(ref widthPercent);
                widthPercent = Math.Round(widthPercent, 2);
                if (isLast)
                {
                    widthPercent = 100 - widthPercentTotal;
                }
                else
                {
                    if (widthPercentTotal + widthPercent > 100)
                    {
                        widthPercent = widthPercentAvg;
                    }
                }
                widthPercentTotal = widthPercentTotal + widthPercent;
                result.Add(new GridColumn() { FieldName = cellList[i].FieldName, Text = text, WidthPercent = widthPercent });
            }
            return result;
        }

        public void Load(Type typeRow)
        {
            string tableName = DataAccessLayer.Util.TableName(typeRow);
            // Row
            if (RowList == null)
            {
                RowList = new Dictionary<string, List<Application.GridRow>>();
            }
            RowList[tableName] = new List<GridRow>();
            // Column
            if (ColumnList == null)
            {
                ColumnList = new Dictionary<string, List<Application.GridColumn>>();
            }
            ColumnList[tableName] = LoadColumnList(typeRow);
            // Cell
            if (CellList == null)
            {
                CellList = new Dictionary<string, Dictionary<string, Dictionary<string, Application.GridCell>>>();
            }
            CellList[tableName] = new Dictionary<string, Dictionary<string, Application.GridCell>>();
            //
            object[] rowList = Framework.Server.DataAccessLayer.Util.Select(typeRow, 0, 20);
            var propertyInfoList = typeRow.GetTypeInfo().GetProperties();
            for (int index = 0; index < rowList.Length; index++)
            {
                object row = rowList[index];
                RowList[tableName].Add(new GridRow() { Index = index.ToString() });
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    string fieldName = propertyInfo.Name;
                    object value = propertyInfo.GetValue(row);
                    object valueJson = Framework.Server.DataAccessLayer.Util.ValueToJson(value);
                    if (!CellList[tableName].ContainsKey(fieldName))
                    {
                        CellList[tableName][fieldName] = new Dictionary<string, GridCell>();
                    }
                    CellList[tableName][fieldName][index.ToString()] = new GridCell() { V = valueJson };
                }
            }
        }

        /// <summary>
        /// (TableName, GridRow)
        /// </summary>
        public Dictionary<string, List<GridRow>> RowList;

        /// <summary>
        /// (TableName, GridColumn)
        /// </summary>
        public Dictionary<string, List<GridColumn>> ColumnList;

        /// <summary>
        /// (TableName, FieldName, Index(Filter, 0..99, Total), GridCell)
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, GridCell>>> CellList;
    }

    public class GridCell
    {
        /// <summary>
        /// Value.
        /// </summary>
        public object V;
    }

    public class GridColumn
    {
        public string FieldName;

        public string Text;

        public double WidthPercent;
    }

    public class GridRow
    {
        public string Index;
    }

    public class Json : Component
    {
        public Json()
            : base(null, "Json")
        {

        }

        /// <summary>
        /// GET not POST json when debugging client. See also file json.json.
        /// </summary>
        public bool IsJsonGet;

        public string Name;

        public Guid Session;

        public bool IsBrowser;

        public string VersionServer;

        public string VersionClient;

        public int ResponseCount;

        public GridData GridData;
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

    public class ApplicationBase
    {
        public Json Process(Json jsonIn)
        {
            Json jsonOut = Framework.Server.DataAccessLayer.Util.JsonObjectClone<Json>(jsonIn);
            if (jsonOut == null || jsonOut.Session == Guid.Empty)
            {
                jsonOut = JsonCreate();
            }
            else
            {
                jsonOut.ResponseCount += 1;
            }
            jsonOut.Name = ".NET Core=" + DateTime.Now.ToString("HH:mm:ss.fff");
            jsonOut.VersionServer = Framework.Util.VersionServer;
            Input input = (Input)jsonOut.List[0].List[1].List[1].List[1];
            input.AutoComplete = input.TextNew?.ToUpper();
            return jsonOut;
        }

        protected virtual Json JsonCreate()
        {
            Json result = new Json();
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
