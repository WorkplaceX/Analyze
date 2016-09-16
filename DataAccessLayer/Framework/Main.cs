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
        private static string NameCsharp(string name)
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

        public static string NameCsharp(string name, List<string> nameExceptList)
        {
            nameExceptList = new List<string>(nameExceptList); // Do not modify list passed as parameter.
            for (int i = 0; i < nameExceptList.Count; i++)
            {
                nameExceptList[i] = NameCsharp(nameExceptList[i]).ToUpper();
            }
            //
            name = NameCsharp(name);
            string result = name;
            int count = 1;
            while (nameExceptList.Contains(result.ToUpper()))
            {
                count += 1;
                result = name + count;
            }
            return result;
        }
    }
}
