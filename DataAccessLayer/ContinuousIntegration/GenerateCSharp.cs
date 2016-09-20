namespace ContinuousIntegration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Generate CSharp code.
    /// </summary>
    public class CSharpGenerate
    {
        public CSharpGenerate(MetaCSharp metaCSharp)
        {
            this.MetaCSharp = metaCSharp;
        }

        public readonly MetaCSharp MetaCSharp;

        /// <summary>
        /// Generate CSharp code for each database schema.
        /// </summary>
        private static void SchemaName(MetaCSharp metaCSharp, StringBuilder result)
        {
            var schemaNameList = metaCSharp.List.GroupBy(item => new { item.Schema.SchemaName, item.SchemaNameCSharp }, (key, group) => key).ToArray();
            bool isFirst = true;
            foreach (var item in schemaNameList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                result.AppendLine(string.Format("namespace Database.{0}", item.SchemaNameCSharp));
                result.AppendLine("{");
                result.AppendLine("    using System;");
                result.AppendLine("    using Framework;");
                result.AppendLine();
                TableName(metaCSharp, item.SchemaName, result);
                result.AppendLine("}");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database table.
        /// </summary>
        private static void TableName(MetaCSharp metaCSharp, string schemaName, StringBuilder result)
        {
            var tableNameList = metaCSharp.List.Where(item => item.Schema.SchemaName == schemaName).GroupBy(item => new { item.Schema.TableName, item.TableNameCSharp }, (key, group) => key).ToArray();
            List<string> nameExceptList = new List<string>();
            bool isFirst = true;
            foreach (var item in tableNameList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                result.AppendLine(string.Format("    public partial class {0} : Row", item.TableNameCSharp));
                result.AppendLine("    {");
                FieldName(metaCSharp, schemaName, item.TableName, result);
                result.AppendLine("    }");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database field.
        /// </summary>
        private static void FieldName(MetaCSharp metaCSharp, string schemaName, string tableName, StringBuilder result)
        {
            var fieldNameList = metaCSharp.List.Where(item => item.Schema.SchemaName == schemaName && item.Schema.TableName == tableName).ToArray();
            bool isFirst = true;
            foreach (var item in fieldNameList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                string typeCSharp = Framework.Util.SqlTypeToCSharp(item.Schema.SqlType, item.Schema.IsNullable);
                result.AppendLine(string.Format("        public " + typeCSharp + " {0} {{ get; set; }}", item.fieldNameCSharp));
            }
        }

        public void Run(out string cSharp)
        {
            StringBuilder result = new StringBuilder();
            SchemaName(MetaCSharp, result);
            cSharp = result.ToString();
        }
    }
}
