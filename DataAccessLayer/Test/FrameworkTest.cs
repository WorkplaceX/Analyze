namespace Test
{
    using System.Collections.Generic;

    public class FrameworkTest : TestBase
    {
        public void Name01()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("Word");
            string nameCsharp = Framework.Util.NameCsharp("Word", nameExceptList);
            Util.Assert(nameCsharp == "Word2");
        }

        public void Name02()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WOrd");
            string nameCsharp = Framework.Util.NameCsharp("Word", nameExceptList);
            Util.Assert(nameCsharp == "Word2");
        }

        public void Name03()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WO_rd?");
            string nameCsharp = Framework.Util.NameCsharp("Word", nameExceptList);
            Util.Assert(nameCsharp == "Word2");
        }

        public void Name04()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WO_rd?");
            nameExceptList.Add("Sun");
            nameExceptList.Add("Word2");
            string nameCsharp = Framework.Util.NameCsharp("Word", nameExceptList);
            Util.Assert(nameCsharp == "Word3");
        }

        public void Name05()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCsharp = Framework.Util.NameCsharp("World", nameExceptList);
            Util.Assert(nameCsharp == "World");
        }

        public void Name06()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("World1");
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCsharp = Framework.Util.NameCsharp("World", nameExceptList);
            Util.Assert(nameCsharp == "World4");
        }

        public void Name07()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCsharp = Framework.Util.NameCsharp("World", nameExceptList);
            Util.Assert(nameCsharp == "World4");
        }

        public void Name08()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("WorlD");
            string nameCsharp = Framework.Util.NameCsharp("WorLD", nameExceptList);
            Util.Assert(nameCsharp == "WorLD2");
        }

        public void Name09()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("WorlD");
            string nameCsharp = Framework.Util.NameCsharp("WorLD", nameExceptList);
            Util.Assert(nameCsharp == "WorLD2");
        }
    }
}
