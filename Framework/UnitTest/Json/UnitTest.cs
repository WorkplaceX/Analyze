using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UnitTest.Json
{
    public class Data
    {
        public string Name;

        public int NumberInt;

        public double NumberDouble;

        public Guid Guid;
    }

    public class DataNullable
    {
        public string Name;

        public int? NumberInt;

        public double? NumberDouble;

        public Guid? Guid;

        public string Type;
    }

    public class DataWithList
    {
        public string Name;

        public List<DataWithListItem> List;
    }

    public class DataWithListItem
    {
        public string Name;
    }

    public class DataWithListItem2 : DataWithListItem
    {
        public string Type;
    }

    public class DataWithDictionary
    {
        public Dictionary<string, int> List;
    }

    public class DataWithDictionary2
    {
        public Dictionary<string, DataWithListItem> List;
    }

    public class DataWithListNested
    {
        public List<Dictionary<string, int>> List;

        public List<List<int>> List2;
    }

    public class UnitTest : UnitTestBase
    {
        public void Test01()
        {
            Data data = new Json.Data();
            data.Name = "Name";
            data.NumberInt = 88;
            data.NumberDouble = 34.223;
            Guid guid = Guid.NewGuid();
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<Data>(json);
            Util.Assert(data.Name == data2.Name);
            Util.Assert(data.NumberInt == data2.NumberInt);
            Util.Assert(data.NumberDouble == data2.NumberDouble);
            Util.Assert(data.Guid == data2.Guid);
        }

        public void Test02()
        {
            DataNullable data = new Json.DataNullable();
            data.Name = "Name";
            data.NumberInt = 88;
            data.NumberDouble = 34.223;
            Guid guid = Guid.NewGuid();
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataNullable>(json);
            Util.Assert(data.Name == data2.Name);
            Util.Assert(data.NumberInt == data2.NumberInt);
            Util.Assert(data.NumberDouble == data2.NumberDouble);
            Util.Assert(data.Guid == data2.Guid);
        }

        public void Test03()
        {
            DataWithList data = new Json.DataWithList();
            data.Name = "L";
            data.List = null;
            // string json = Server.Json.Util.Serialize(data); // TODO throws error
            // DataWithList data2 = Server.Json.Util.Deserialize<DataWithList>(json); // TODO throws error
        }

        public void Test04()
        {
            DataWithList data = new Json.DataWithList();
            data.Name = "L";
            data.List = new List<Json.DataWithListItem>();
            data.List.Add(new Json.DataWithListItem() { Name = "X1" });
            data.List.Add(new Json.DataWithListItem2() { Name = "X2" });
            data.List.Add(null);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithList>(json);
            Util.Assert(data.List[0].Name == data2.List[0].Name);
            Util.Assert(data.List[1].GetType() == typeof(DataWithListItem2));
        }

        public void Test05()
        {
            DataWithDictionary data = new Json.DataWithDictionary();
            data.List = new Dictionary<string, int>();
            data.List["F"] = 33;
            data.List["G"] = 44;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithDictionary>(json);
            Util.Assert(data.List["F"] == 33);
            Util.Assert(data.List["G"] == 44);
        }

        public void Test06()
        {
            var data = new Json.DataWithDictionary2();
            data.List = new Dictionary<string, Json.DataWithListItem>();
            data.List["F"] = new DataWithListItem() { Name = "FF" };
            data.List["G"] = new DataWithListItem2() { Name = "GG" };
            data.List["H"] = null;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithDictionary2>(json);
            Util.Assert(data.List["F"].Name == "FF");
            Util.Assert(data.List["G"].Name == "GG");
            Util.Assert(data.List["G"].GetType() == typeof(DataWithListItem2));
            Util.Assert(data.List["H"] == null);
        }

        public void Test07()
        {
            var data = new Json.DataWithListNested();
            data.List = new List<Dictionary<string, int>>();
            data.List.Add(new Dictionary<string, int>());
            data.List[0]["X"] = 99;
            data.List2 = new List<List<int>>();
            data.List2.Add(new List<int>());
            data.List2[0].Add(88);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithListNested>(json);
        }
    }
}
