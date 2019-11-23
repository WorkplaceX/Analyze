using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsoleApp
{
    public class ComponentJson
    {
        public ComponentJson()
        {

        }

        public ComponentJson(ComponentJson? owner)
        {
            Constructor(owner);
        }

        internal void Constructor(ComponentJson? owner)
        {
            this.Type = GetType().Name;
            if (owner == null)
            {
                Root = this;
            }
            else
            {
                Root = owner.Root;
                owner.List.Add(this);
            }
            Root.RootIdCount += 1;
            Id = Root.RootIdCount;
        }

        internal ComponentJson Root { get; private set; }

        internal int RootIdCount = 0;

        public string? Type { get; set; }

        public int Id { get; set; }

        private List<ComponentJson>? list;

        public List<ComponentJson> List { get; set; } = new List<ComponentJson>();
        //{
        //    get
        //    {
        //        if (list == null)
        //        {
        //            list = new List<ComponentJson>();
        //        }
        //        return list;
        //    }
        //}
    }

    public class Button : ComponentJson
    {
        public Button() : base(null)
        {

        }

        public Button(ComponentJson? owner) 
            : base(owner)
        {

        }

        public string? TextHtml { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsClick { get; set; }

        public My My { get; set; }
    }

    public class Page : ComponentJson
    {
        public Page(ComponentJson owner) 
            : base(owner)
        {

        }
    }

    public class My : ComponentJson
    {
        public  My() : base(null)
        {

        }
        public My(ComponentJson? owner) : base(owner)
        {

        }

        public string X { get; set; }
    }

    public class My2 : My
    {
        public My2() : base(null)
        {

        }

        public My2(ComponentJson? owner) : base(owner)
        {

        }

        public string Y { get; set; }

        public Row Row { get; set; }
    }

    public class Row
    {
        public string Hello { get; set; }
    }

    public class Person : Row
    {
        public string Name { get; set; }

        public decimal? Value1 { get; set; }

        public decimal Value2 { get; set; }

        public double Value3 { get; set; }

        public List<Row> RowList { get; set; } = new List<Row>();

        public Dictionary<string, Row> RowList2 { get; set; } = new Dictionary<string, Row>();
    }
}
