namespace Framework.Server.Application
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;

    public class Grid : JsonComponent
    {
        public Grid() { }

        public Grid(JsonComponent owner, string text, string gridName)
            : base(owner, text)
        {
            this.GridName = gridName;
        }

        public string GridName;
    }

    /// <summary>
    /// Grid keyboard handler (Singleton).
    /// </summary>
    public class GridKeyboard : JsonComponent
    {
        public GridKeyboard() { }

        public GridKeyboard(JsonComponent owner, string text)
            : base(owner, text)
        {

        }
    }

    public class GridField : JsonComponent
    {
        public GridField() { }

        public GridField(JsonComponent owner, string text, string gridName, string fieldName, string gridIndex)
            : base(owner, text)
        {
            this.GridName = gridName;
            this.FieldName = fieldName;
            this.GridIndex = gridIndex;
        }

        public string GridName;

        public string FieldName;

        public string GridIndex;
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

        public void Load(string gridName, Type typeRow)
        {
            if (GridLoadList == null)
            {
                GridLoadList = new Dictionary<string, Application.GridLoad>();
            }
            GridLoadList[gridName] = new Application.GridLoad() { GridName = gridName };
            // Row
            if (RowList == null)
            {
                RowList = new Dictionary<string, List<Application.GridRow>>();
            }
            RowList[gridName] = new List<GridRow>();
            // Column
            if (ColumnList == null)
            {
                ColumnList = new Dictionary<string, List<Application.GridColumn>>();
            }
            ColumnList[gridName] = LoadColumnList(typeRow);
            // Cell
            if (CellList == null)
            {
                CellList = new Dictionary<string, Dictionary<string, Dictionary<string, Application.GridCell>>>();
            }
            CellList[gridName] = new Dictionary<string, Dictionary<string, Application.GridCell>>();
            //
            object[] rowList = Framework.Server.DataAccessLayer.Util.Select(typeRow, 0, 15);
            var propertyInfoList = typeRow.GetTypeInfo().GetProperties();
            for (int index = 0; index < rowList.Length; index++)
            {
                object row = rowList[index];
                RowList[gridName].Add(new GridRow() { Index = index.ToString() });
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    string fieldName = propertyInfo.Name;
                    object value = propertyInfo.GetValue(row);
                    object valueJson = Framework.Server.DataAccessLayer.Util.ValueToJson(value);
                    if (!CellList[gridName].ContainsKey(fieldName))
                    {
                        CellList[gridName][fieldName] = new Dictionary<string, GridCell>();
                    }
                    CellList[gridName][fieldName][index.ToString()] = new GridCell() { V = valueJson };
                }
            }
        }

        /// <summary>
        /// (GridName, GridLoad) List of all loaded data grids.
        /// </summary>
        public Dictionary<string, GridLoad> GridLoadList;

        /// <summary>
        /// (GridName, GridRow)
        /// </summary>
        public Dictionary<string, List<GridRow>> RowList;

        /// <summary>
        /// (GridName, GridColumn)
        /// </summary>
        public Dictionary<string, List<GridColumn>> ColumnList;

        /// <summary>
        /// (GridName, FieldName, Index(Filter, 0..99, Total), GridCell)
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, GridCell>>> CellList;

        /// <summary>
        /// Focused grid cell.
        /// </summary>
        public string FocusGridName;

        /// <summary>
        /// Focused grid cell.
        /// </summary>
        public string FocusIndex;

        /// <summary>
        /// Focused grid cell.
        /// </summary>
        public string FocusFieldName;
    }

    public class GridCell
    {
        /// <summary>
        /// Value.
        /// </summary>
        public object V;

        public bool IsSelect;

        public bool IsClick;
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

        public bool IsClick;

        /// <summary>
        /// Bitwise (01=Select; 10=MouseOver; 11=Select and MouseOver).
        /// </summary>
        public int IsSelect;

        public bool IsSelectGet()
        {
            return (IsSelect & 1) == 1;
        }

        public void IsSelectSet(bool value)
        {
            if (value)
            {
                IsSelect = IsSelect | 1;
            }
            else
            {
                IsSelect = IsSelect & 2;
            }
        }
    }

    public class GridLoad
    {
        public string GridName;
    }

    public class JsonApplication : JsonComponent
    {
        public JsonApplication()
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

        public int RequestCount; // Set by client.

        public int ResponseCount;

        public GridData GridData;
    }

    public class JsonComponent
    {
        public JsonComponent() : this(null, null) { }

        public JsonComponent(JsonComponent owner, string text)
        {
            Constructor(owner, text);
        }

        private void Constructor(JsonComponent owner, string text)
        {
            this.Type = GetType().Name;
            this.Text = text;
            if (owner != null)
            {
                if (owner.List == null)
                {
                    owner.List = new List<JsonComponent>();
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

        public List<JsonComponent> List = new List<JsonComponent>();


        private void ListAll(List<JsonComponent> result)
        {
            result.AddRange(List);
            foreach (var item in List)
            {
                item.ListAll(result);
            }
        }

        public List<JsonComponent> ListAll()
        {
            List<JsonComponent> result = new List<JsonComponent>();
            ListAll(result);
            return result;
        }

        public List<T> ListAll<T>() where T : JsonComponent
        {
            List<JsonComponent> result = ListAll();
            return result.OfType<T>().ToList();
        }
    }

    public class LayoutContainer : JsonComponent
    {
        public LayoutContainer() : this(null, null) { }

        public LayoutContainer(JsonComponent owner, string text)
            : base(owner, text)
        {

        }
    }

    public class LayoutRow : JsonComponent
    {
        public LayoutRow() : this(null, null) { }

        public LayoutRow(LayoutContainer owner, string text)
            : base(owner, text)
        {

        }
    }

    public class LayoutCell : JsonComponent
    {
        public LayoutCell() : this(null, null) { }

        public LayoutCell(LayoutRow owner, string text)
            : base(owner, text)
        {

        }
    }

    public class Button : JsonComponent
    {
        public Button() : this(null, null) { }

        public Button(JsonComponent owner, string text)
            : base(owner, text)
        {
            if (IsClick)
            {
                Text += "."; // TODO
            }
        }

        public bool IsClick;
    }

    public class Input : JsonComponent
    {
        public Input() : this(null, null) { }

        public Input(JsonComponent owner, string text)
            : base(owner, text)
        {

        }

        public bool IsFocus;

        public string TextNew;

        public string AutoComplete;
    }

    public class Label : JsonComponent
    {
        public Label() : this(null, null) { }

        public Label(JsonComponent owner, string text)
            : base(owner, text)
        {

        }
    }

    public class ApplicationBase
    {
        private void ProcessGridSelectRowClear(JsonApplication jsonApplicationOut)
        {
            foreach (string gridName in jsonApplicationOut.GridData.RowList.Keys)
            {
                foreach (GridRow gridRow in jsonApplicationOut.GridData.RowList[gridName])
                {
                    gridRow.IsSelectSet(false);
                }
            }
        }

        private void ProcessGridSelectCellClear(JsonApplication jsonApplicationOut)
        {
            GridData gridData = jsonApplicationOut.GridData;
            foreach (string gridName in gridData.RowList.Keys)
            {
                foreach (GridRow gridRow in gridData.RowList[gridName])
                {
                    foreach (var gridColumn in gridData.ColumnList[gridName])
                    {
                        GridCell gridCell = gridData.CellList[gridName][gridColumn.FieldName][gridRow.Index];
                        gridCell.IsSelect = false;
                    }
                }
            }
        }

        private void ProcessGridSelectCell(JsonApplication jsonApplicationOut, string gridName, string index, string fieldName)
        {
            GridData gridData = jsonApplicationOut.GridData;
            gridData.FocusGridName = gridName;
            gridData.FocusIndex = index;
            gridData.FocusFieldName = fieldName;
            ProcessGridSelectCellClear(jsonApplicationOut);
            gridData.CellList[gridName][fieldName][index].IsSelect = true;
        }

        private void ProcessGridSelect(JsonApplication jsonApplicationOut)
        {
            GridData gridData = jsonApplicationOut.GridData;
            foreach (string gridName in gridData.RowList.Keys)
            {
                foreach (GridRow gridRow in gridData.RowList[gridName])
                {
                    if (gridRow.IsClick)
                    {
                        gridRow.IsClick = false;
                        ProcessGridSelectRowClear(jsonApplicationOut);
                        gridRow.IsSelectSet(true);
                    }
                    foreach (var gridColumn in gridData.ColumnList[gridName])
                    {
                        GridCell gridCell = gridData.CellList[gridName][gridColumn.FieldName][gridRow.Index];
                        if (gridCell.IsClick == true)
                        {
                            gridCell.IsClick = false;
                            ProcessGridSelectCell(jsonApplicationOut, gridName, gridRow.Index, gridColumn.FieldName);
                        }
                    }
                }
            }
        }

        public JsonApplication Process(JsonApplication jsonApplicationIn)
        {
            JsonApplication jsonApplicationOut = Framework.Server.DataAccessLayer.Util.JsonObjectClone<JsonApplication>(jsonApplicationIn);
            if (jsonApplicationOut == null || jsonApplicationOut.Session == Guid.Empty)
            {
                jsonApplicationOut = JsonApplicationCreate();
            }
            else
            {
                jsonApplicationOut.ResponseCount += 1;
            }
            ProcessGridSelect(jsonApplicationOut);
            jsonApplicationOut.Name = ".NET Core=" + DateTime.Now.ToString("HH:mm:ss.fff");
            jsonApplicationOut.VersionServer = Framework.Util.VersionServer;
            return jsonApplicationOut;
        }

        protected virtual JsonApplication JsonApplicationCreate()
        {
            JsonApplication result = new JsonApplication();
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
