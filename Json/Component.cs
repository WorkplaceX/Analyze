using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsoleApp
{
    public class ComponentJson
    {
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
            Id = Root.RootIdCount.ToString(CultureInfo.InvariantCulture);
        }

        internal ComponentJson Root { get; private set; }

        internal int RootIdCount = 0;

        public string? Type { get; private set; }

        public string Id { get; private set; }

        private List<ComponentJson>? list;

        public List<ComponentJson> List
        {
            get
            {
                if (list == null)
                {
                    list = new List<ComponentJson>();
                }
                return list;
            }
        }
    }

    public class Button : ComponentJson
    {
        public Button(ComponentJson? owner) 
            : base(owner)
        {

        }

        public string? TextHtml { get; set; }

        public bool IsClick { get; set; }
    }

    public class Page : ComponentJson
    {
        public Page(ComponentJson owner) 
            : base(owner)
        {

        }
    }
}
