namespace ContinuousIntegration
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// Generate CSharp code for database tables.
    /// </summary>
    public static class GenerateCSharp
    {
        /// <summary>
        /// Generate CSharp code for each database schema.
        /// </summary>
        private static void SchemaName(Schema[] dataList, StringBuilder result)
        {
            string[] schemaNameList = dataList.GroupBy(item => item.SchemaName, (key, group) => key).ToArray();
            List<string> nameExceptList = new List<string>();
            bool isFirst = true;
            foreach (string schemaName in schemaNameList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                Framework.Util.NameCSharp("namespace Database.{0}", schemaName, nameExceptList, result);
                result.AppendLine("{");
                TableName(dataList, schemaName, result);
                result.AppendLine("}");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database table.
        /// </summary>
        private static void TableName(Schema[] dataList, string schemaName, StringBuilder result)
        {
            string[] tableNameList = dataList.Where(item => item.SchemaName == schemaName).GroupBy(item => item.TableName, (key, group) => key).ToArray();
            List<string> nameExceptList = new List<string>();
            bool isFirst = true;
            foreach (string tableName in tableNameList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                Framework.Util.NameCSharp("    public class {0}", tableName, nameExceptList, result);
                result.AppendLine("    {");
                FieldName(dataList, schemaName, tableName, result);
                result.AppendLine("    }");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database field.
        /// </summary>
        private static void FieldName(Schema[] dataList, string schemaName, string tableName, StringBuilder result)
        {
            Schema[] fieldList = dataList.Where(item => item.SchemaName == schemaName && item.TableName == tableName).ToArray();
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add(tableName); // CSharp propery can not have same name like class.
            bool isFirst = true;
            foreach (var field in fieldList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    result.AppendLine();
                }
                Framework.Util.NameCSharp("        public string {0} {{ get; set; }}", field.FieldName, nameExceptList, result);
            }
        }

        private static void Meta(Schema[] dataList, StringBuilder result)
        {
            result.AppendLine();
            result.AppendLine("namespace Database");
            result.AppendLine("{");
            result.AppendLine("}");

        }

        /// <summary>
        /// Script to generate CSharp code.
        /// </summary>
        public static void Run()
        {
            string sql = Util.FileLoad(ConnectionManager.SchemaFileName);
            DbContextBuild dbContext = new DbContextBuild();
            StringBuilder result = new StringBuilder();
            Schema[] dataList = dbContext.Schema.FromSql(sql).OrderBy(item => item.SchemaName).ThenBy(item => item.TableName).ToArray();
            SchemaName(dataList, result);
            Meta(dataList, result);
            string cSharp = result.ToString();
            Util.FileSave(ConnectionManager.DatabaseFileName, cSharp);
        }
    }

    /// <summary>
    /// DbContext used to query database schema.
    /// </summary>
    public class DbContextBuild : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionManager.ConnectionString);
        }

        public DbSet<Schema> Schema { get; set; }
    }

    /// <summary>
    /// See also file Sql\Schema.sql
    /// </summary>
    public class Schema
    {
        [Key]
        public Guid IdView { get; set; }

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public int FieldNameOrderBy { get; set; }

        public bool IsView { get; set; }

        public byte SqlType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }
    }
}
