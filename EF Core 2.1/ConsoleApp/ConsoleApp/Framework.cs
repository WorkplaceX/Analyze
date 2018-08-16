using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace ConsoleApp.Framework
{
    public static class Framework
    {
        private static PropertyInfo[] TypeRowToPropertyList(Type typeRow)
        {
            return typeRow.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private static IMutableModel DbContextModel(Type typeRow)
        {
            // EF Core 2.1
            var typeMappingSource = new SqlServerTypeMappingSource(new TypeMappingSourceDependencies(new ValueConverterSelector(new ValueConverterSelectorDependencies())), new RelationalTypeMappingSourceDependencies());

            var conventionSet = SqlServerConventionSetBuilder.Build();
            var builder = new ModelBuilder(conventionSet);

            // Build model
            var entity = builder.Entity(typeRow);
            SqlTableAttribute tableAttribute = (SqlTableAttribute)typeRow.GetTypeInfo().GetCustomAttribute(typeof(SqlTableAttribute));
            entity.ToTable(tableAttribute.SqlTableName, tableAttribute.SqlSchemaName); // By default EF maps sql table name to class name.
            PropertyInfo[] propertyInfoList = TypeRowToPropertyList(typeRow);
            bool isPrimaryKey = false; // Sql view 
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                SqlFieldAttribute columnAttribute = (SqlFieldAttribute)propertyInfo.GetCustomAttribute(typeof(SqlFieldAttribute));
                if (columnAttribute == null || columnAttribute.SqlFieldName == null) // Calculated column. Do not include it in sql select.
                {
                    entity.Ignore(propertyInfo.Name);
                }
                else
                {
                    if (columnAttribute.IsPrimaryKey)
                    {
                        isPrimaryKey = true;
                    }
                    entity.Property(propertyInfo.PropertyType, propertyInfo.Name).HasColumnName(columnAttribute.SqlFieldName);
                    CoreTypeMapping coreTypeMapping = typeMappingSource.FindMapping(propertyInfo.PropertyType);
                    Debug.Assert(coreTypeMapping != null);
                    entity.Property(propertyInfo.PropertyType, propertyInfo.Name).HasAnnotation(CoreAnnotationNames.TypeMapping, coreTypeMapping);
                }
            }

            if (isPrimaryKey == false)
            {
                entity.HasKey(propertyInfoList.First().Name); // Prevent null exception if first field of view does not start with name "Id".
            }

            var model = builder.Model;
            return model;
        }

        private static DbContext DbContext(Type typeRow)
        {
            var options = new DbContextOptionsBuilder<DbContext>();
            string connectionString = "Data Source=localhost; Initial Catalog=AdventureWorks2016; Integrated Security=True;";
            if (connectionString == null)
            {
                throw new Exception("ConnectionString is null! (See also file: ConfigFramework.json)");
            }
            options.UseSqlServer(connectionString); // See also: ConfigFramework.json // (Data Source=localhost; Initial Catalog=Application; Integrated Security=True;)
            options.UseModel(DbContextModel(typeRow));
            DbContext result = new DbContext(options.Options);

            return result;
        }

        public static IQueryable Query(Type typeRow)
        {
            DbContext dbContext = DbContext(typeRow);
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeRow).Invoke(dbContext, null));
            return query;
        }

        public static IQueryable<TRow> Query<TRow>() where TRow : Row
        {
            return (IQueryable<TRow>)Query(typeof(TRow));
        }

        public static List<Row> Select(IQueryable query)
        {
            var list = query.ToDynamicList();
            List<Row> result = list.Cast<Row>().ToList();
            return result;
        }
    }

    public class Row
    {

    }

    public class SqlFieldAttribute : Attribute
    {
        public SqlFieldAttribute(string sqlFieldName, bool isPrimaryKey)
        {
            this.SqlFieldName = sqlFieldName;
            this.IsPrimaryKey = isPrimaryKey;
        }

        public SqlFieldAttribute(string sqlFieldName)
            : this(sqlFieldName, false)
        {

        }

        /// <summary>
        /// Gets or sets SqlFieldName. If null, it's a calculated field.
        /// </summary>
        public readonly string SqlFieldName;

        public readonly bool IsPrimaryKey;

        public readonly Type TypeCell;
    }


    public class SqlTableAttribute : Attribute
    {
        public SqlTableAttribute(string sqlSchemaName, string sqlTableName)
        {
            this.SqlSchemaName = sqlSchemaName;
            this.SqlTableName = sqlTableName;
        }

        public readonly string SqlSchemaName;

        public readonly string SqlTableName;
    }


    [SqlTable("Person", "vAdditionalContactInfo")]
    public class vAdditionalContactInfo : Row
    {
        [SqlField("BusinessEntityID")]
        public int BusinessEntityID { get; set; }

        [SqlField("FirstName")]
        public string FirstName { get; set; }

        [SqlField("MiddleName")]
        public string MiddleName { get; set; }

        [SqlField("LastName")]
        public string LastName { get; set; }

        [SqlField("TelephoneNumber")]
        public string TelephoneNumber { get; set; }

        [SqlField("TelephoneSpecialInstructions")]
        public string TelephoneSpecialInstructions { get; set; }

        [SqlField("Street")]
        public string Street { get; set; }

        [SqlField("City")]
        public string City { get; set; }

        [SqlField("StateProvince")]
        public string StateProvince { get; set; }

        [SqlField("PostalCode")]
        public string PostalCode { get; set; }

        [SqlField("CountryRegion")]
        public string CountryRegion { get; set; }

        [SqlField("HomeAddressSpecialInstructions")]
        public string HomeAddressSpecialInstructions { get; set; }

        [SqlField("EMailAddress")]
        public string EMailAddress { get; set; }

        [SqlField("EMailSpecialInstructions")]
        public string EMailSpecialInstructions { get; set; }

        [SqlField("EMailTelephoneNumber")]
        public string EMailTelephoneNumber { get; set; }

        [SqlField("rowguid")]
        public Guid rowguid { get; set; }

        [SqlField("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }

    [SqlTable("dbo", "My")]
    public class My : Row
    {
        [SqlField("Id")]
        public int Id { get; set; }

        [SqlField("Text")]
        public string Text { get; set; }
    }
}
