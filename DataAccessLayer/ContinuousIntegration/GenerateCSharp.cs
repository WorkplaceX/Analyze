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
        private static void SchemaName(Meta meta, StringBuilder result)
        {
            var schemaNameList = meta.List.GroupBy(item => new { item.Schema.SchemaName, item.SchemaNameCSharp } , (key, group) => key).ToArray();
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
                TableName(meta, item.SchemaName, result);
                result.AppendLine("}");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database table.
        /// </summary>
        private static void TableName(Meta meta, string schemaName, StringBuilder result)
        {
            var tableNameList = meta.List.Where(item => item.Schema.SchemaName == schemaName).GroupBy(item => new { item.Schema.TableName, item.TableNameCSharp } , (key, group) => key).ToArray();
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
                FieldName(meta, schemaName, item.TableName, result);
                result.AppendLine("    }");
            }
        }

        /// <summary>
        /// Generate CSharp code for each database field.
        /// </summary>
        private static void FieldName(Meta meta, string schemaName, string tableName, StringBuilder result)
        {
            var  fieldNameList = meta.List.Where(item => item.Schema.SchemaName == schemaName && item.Schema.TableName == tableName).ToArray();
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

        private static void Meta(Schema[] schemaSql, StringBuilder result)
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
            Schema[] schemaSql = dbContext.Schema.FromSql(sql).ToArray();
            Meta meta = new Meta(schemaSql);
            SchemaName(meta, result);
            Meta(schemaSql, result);
            string cSharp = result.ToString();
            Util.FileSave(ConnectionManager.DatabaseLockFileName, cSharp);
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

    public class Meta
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Meta(Schema[] schemaSql)
        {
            SchemaName(schemaSql);
        }

        public class SchemaCSharp
        {
            public Schema Schema { get; set; }

            public string SchemaNameCSharp { get; set; }

            public string TableNameCSharp { get; set; }

            public string fieldNameCSharp { get; set; }
        }

        private void SchemaName(Schema[] dataList)
        {
            string[] schemaNameList = dataList.GroupBy(item => item.SchemaName, (key, group) => key).ToArray();
            List<string> nameExceptList = new List<string>();
            foreach (string schemaName in schemaNameList)
            {
                string schemaNameCSharp = Framework.Util.NameCSharp(schemaName, nameExceptList);
                TableName(dataList, schemaName, schemaNameCSharp);
            }
        }

        private void TableName(Schema[] dataList, string schemaName, string schemaNameCSharp)
        {
            string[] tableNameList = dataList.Where(item => item.SchemaName == schemaName).GroupBy(item => item.TableName, (key, group) => key).ToArray();
            List<string> nameExceptList = new List<string>();
            foreach (string tableName in tableNameList)
            {
                string tableNameCSharp = Framework.Util.NameCSharp(tableName, nameExceptList);
                FieldName(dataList, schemaName, schemaNameCSharp, tableName, tableNameCSharp);
            }
        }

        private void FieldName(Schema[] dataList, string schemaName, string schemaNameCSharp, string tableName, string tableNameCSharp)
        {
            Schema[] fieldList = dataList.Where(item => item.SchemaName == schemaName && item.TableName == tableName).ToArray();
            List<string> nameExceptList = new List<string>();
            nameExceptList.Add(tableName); // CSharp propery can not have same name like class.
            foreach (Schema field in fieldList)
            {
                string fieldNameCSharp = Framework.Util.NameCSharp(field.FieldName, nameExceptList);
                List.Add(new SchemaCSharp()
                {
                    Schema = field,
                    SchemaNameCSharp = schemaNameCSharp,
                    TableNameCSharp = tableNameCSharp,
                    fieldNameCSharp = fieldNameCSharp,
                });
            }
        }

        public readonly List<SchemaCSharp> List = new List<SchemaCSharp>();
    }
}
