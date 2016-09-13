namespace ContinuousIntegration
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public static class Build
    {
        public static void Run()
        {
            string sql = Util.FileLoad(ConnectionManager.SchemaFileName);
            DbContextBuild dbContext = new DbContextBuild();
            StringBuilder result = new StringBuilder();
            Schema[] schemaList = dbContext.Schema.FromSql(sql).OrderBy(item => item.SchemaName).ThenBy(item => item.TableName).ToArray();
            string[] schemaNameList = schemaList.GroupBy(item => item.SchemaName, (key, group) => key).ToArray();
            bool isFirstSchema = true;
            foreach (string schemaName in schemaNameList)
            {
                if (isFirstSchema)
                {
                    isFirstSchema = false;
                }
                else
                {
                    result.AppendLine();
                }
                result.AppendLine($"namespace Database.{schemaName}");
                result.AppendLine("{");
                string[] tableNameList = schemaList.Where(item => item.SchemaName == schemaName).GroupBy(item => item.TableName, (key, group) => key).ToArray();
                bool isFirstTableName = true;
                foreach (string tableName in tableNameList)
                {
                    if (isFirstTableName)
                    {
                        isFirstTableName = false;
                    }
                    else
                    {
                        result.AppendLine();
                    }
                    result.AppendLine($"    public class {tableName}");
                    result.AppendLine("    {");
                    Schema[] fieldList = schemaList.Where(item => item.SchemaName == schemaName && item.TableName == tableName).ToArray();
                    bool isFirstField = true;
                    foreach (var field in fieldList)
                    {
                        if (isFirstField)
                        {
                            isFirstField = false;
                        }
                        else
                        {
                            result.AppendLine();
                        }
                        result.AppendLine($"        public string {field.FieldName} {{ get; set; }}");
                    }
                    result.AppendLine("    }");
                }
                result.AppendLine("}");
            }
            string csharp = result.ToString();
            Util.FileSave(ConnectionManager.DatabaseFileName, csharp);
        }
    }

    public class DbContextBuild : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionManager.ConnectionString);
        }

        public DbSet<Schema> Schema { get; set; }
    }

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
