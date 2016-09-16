namespace Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Util
    {
        /// <summary>
        /// Filter out special characters. Allow only characters and numbers.
        /// </summary>
        private static string NameCSharp(string name)
        {
            StringBuilder result = new StringBuilder();
            foreach (char item in name)
            {
                if (item >= '0' && item <= '9')
                {
                    result.Append(item);
                }
                char itemToUpper = char.ToUpper(item);
                if (itemToUpper >= 'A' && itemToUpper <= 'Z')
                {
                    result.Append(item);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// (Name, NameCSharp). For example ("World*", "WorldNew")
        /// </summary>
        public static Dictionary<string, string> NameCSharpCustomizeList = new Dictionary<string, string>();

        /// <summary>
        /// Return CSharp code compliant name.
        /// </summary>
        private static string NameCSharp(string name, List<string> nameExceptList)
        {
            string nameCSharpCustomize;
            if (NameCSharpCustomizeList.TryGetValue(name, out nameCSharpCustomize))
            {
                return nameCSharpCustomize;
            }
            //
            nameExceptList = new List<string>(nameExceptList); // Do not modify list passed as parameter.
            for (int i = 0; i < nameExceptList.Count; i++)
            {
                nameExceptList[i] = NameCSharp(nameExceptList[i]).ToUpper();
            }
            //
            name = NameCSharp(name);
            string result = name;
            int count = 1;
            while (nameExceptList.Contains(result.ToUpper()))
            {
                count += 1;
                result = name + count;
            }
            return result;
        }

        /// <summary>
        /// Generate CSharp compliant line of code.
        /// </summary>
        /// <param name="cSharp">For example: "public class {0}"</param>
        /// <param name="name">For example: "My_Class"</param>
        /// <param name="nameExceptList">Name is automatically added.</param>
        /// <param name="result">Returns for example: "public class MyClass"</param>
        public static void NameCSharp(string cSharp, string name, List<string> nameExceptList, StringBuilder result)
        {
            string nameCSharp = Util.NameCSharp(name, nameExceptList);
            result.AppendLine(string.Format(cSharp, nameCSharp));
            nameExceptList.Add(name);
        }
    }
}
