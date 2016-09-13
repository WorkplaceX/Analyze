namespace ContinuousIntegration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public static class Build
    {
        public static void Run()
        {
            DbContextBuild dbContext = new ContinuousIntegration.DbContextBuild();
            foreach (var schema in dbContext.Schema)
            {

            }
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

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public int FieldNameOrderBy { get; set; }

        public bool IsView { get; set; }

        public int SqlType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }
    }
}
