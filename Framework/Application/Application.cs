namespace Application
{
    using Framework.Server.Application;
    using System;

    public class ApplicationX : ApplicationBase
    {
        protected override JsonApplication JsonApplicationCreate()
        {
            JsonApplication result = new JsonApplication();
            result.Session = Guid.NewGuid();
            //
            new GridKeyboard(result, "GridKeyboard");
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
            new GridField(cellFooter1, "Field");
            var grid = new Grid(cellFooter1, "Grid", "Master");
            //
            var gridData = new GridData();
            gridData.Load("Master", typeof(Database.dbo.AirportDisplay));
            if (gridData.FocusGridName == null)
            {
                //gridData.FocusGridName = "Master";
                //gridData.FocusFieldName = "AirportCode";
                //gridData.FocusIndex = "2";
            }
            result.GridData = gridData;
            //
            return result;
        }
    }
}
