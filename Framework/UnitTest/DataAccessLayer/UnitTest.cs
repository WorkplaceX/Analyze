﻿namespace UnitTest.DataAccessLayer
{
    using System.Collections.Generic;

    public class UnitTest : UnitTestBase
    {
        public void Name01()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("Word");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("Word", nameExceptList);
            Util.Assert(nameCSharp == "Word2");
        }

        public void Name02()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WOrd");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("Word", nameExceptList);
            Util.Assert(nameCSharp == "Word2");
        }

        public void Name03()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WO_rd?");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("Word", nameExceptList);
            Util.Assert(nameCSharp == "Word2");
        }

        public void Name04()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("WO_rd?");
            nameExceptList.Add("Sun");
            nameExceptList.Add("Word2");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("Word", nameExceptList);
            Util.Assert(nameCSharp == "Word3");
        }

        public void Name05()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("World", nameExceptList);
            Util.Assert(nameCSharp == "World");
        }

        public void Name06()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("World1");
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("World", nameExceptList);
            Util.Assert(nameCSharp == "World4");
        }

        public void Name07()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("World2");
            nameExceptList.Add("World3");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("World", nameExceptList);
            Util.Assert(nameCSharp == "World4");
        }

        public void Name08()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("WorlD");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("WorLD", nameExceptList);
            Util.Assert(nameCSharp == "WorLD2");
        }

        public void Name09()
        {
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add("World");
            nameExceptList.Add("WorlD");
            string nameCSharp = Build.DataAccessLayer.Util.NameCSharp("WorLD", nameExceptList);
            Util.Assert(nameCSharp == "WorLD2");
        }
    }
}