﻿namespace Framework.Server.DataAccessLayer
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Reflection;

    public static class Util
    {
        public static string TableName(Type typeRow)
        {
            SqlNameAttribute attributeRow = (SqlNameAttribute)typeRow.GetTypeInfo().GetCustomAttribute(typeof(SqlNameAttribute));
            return attributeRow.SqlName;
        }

        public static List<Cell> ColumnList(Type typeRow)
        {
            List<Cell> result = new List<Cell>();
            SqlNameAttribute attributeRow = (SqlNameAttribute)typeRow.GetTypeInfo().GetCustomAttribute(typeof(SqlNameAttribute));
            foreach (PropertyInfo propertyInfo in typeRow.GetTypeInfo().GetProperties())
            {
                SqlNameAttribute attributePropertySql = (SqlNameAttribute)propertyInfo.GetCustomAttribute(typeof(SqlNameAttribute));
                TypeCellAttribute attributePropertyCell = (TypeCellAttribute)propertyInfo.GetCustomAttribute(typeof(TypeCellAttribute));
                Cell cell = (Cell)Activator.CreateInstance(attributePropertyCell.TypeCell);
                cell.Constructor(attributeRow.SqlName, attributePropertySql.SqlName);
                result.Add(cell);
            }
            return result;
        }

        public static List<Cell> CellList(object row)
        {
            List<Cell> result = new List<Cell>();
            result = ColumnList(row.GetType());
            foreach (Cell cell in result)
            {
                cell.Constructor(row);
            }
            return result;
        }

        private static IQueryable SelectQuery(Type typeRow)
        {
            var conventionBuilder = new CoreConventionSetBuilder();
            var conventionSet = conventionBuilder.CreateConventionSet();
            var builder = new ModelBuilder(conventionSet);
            {
                var entity = builder.Entity(typeRow);
                SqlNameAttribute attributeRow = (SqlNameAttribute)typeRow.GetTypeInfo().GetCustomAttribute(typeof(SqlNameAttribute));
                entity.ToTable(attributeRow.SqlName);
                foreach (PropertyInfo propertyInfo in typeRow.GetTypeInfo().GetProperties())
                {
                    SqlNameAttribute attributeProperty = (SqlNameAttribute)propertyInfo.GetCustomAttribute(typeof(SqlNameAttribute));
                    entity.Property(propertyInfo.PropertyType, propertyInfo.Name).HasColumnName(attributeProperty.SqlName);
                }
            }
            var options = new DbContextOptionsBuilder<DbContext>();
            options.UseSqlServer(Framework.Server.ConnectionManager.ConnectionString);
            options.UseModel(builder.Model);
            DbContext dbContext = new DbContext(options.Options);
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // For SQL views. No primary key.
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeRow).Invoke(dbContext, null));
            return query;
        }

        public static object[] Select(Type typeRow)
        {
            return SelectQuery(typeRow).ToDynamicArray();
        }

        public static TRow[] Select<TRow>() where TRow : Row
        {
            return Select(typeof(TRow)).Cast<TRow>().ToArray();
        }

        public static object[] Select(Type typeRow, int id)
        {
            IQueryable query = SelectQuery(typeRow);
            return query.Where("Id = @0", id).ToDynamicArray();
        }

        public static object[] Select(Type typeRow, int pageIndex, int pageRowCount)
        {

            var query = SelectQuery(typeRow).Skip(pageIndex * pageRowCount).Take(pageRowCount);
            object[] result = query.ToDynamicArray().ToArray();
            return result;
        }

        public static T JsonObjectClone<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }

        public static object ValueToJson(object value)
        {
            object result = value;
            if (value != null)
            {
                if (value.GetType() == typeof(int))
                {
                    result = Convert.ChangeType(value, typeof(double));
                }
            }
            return result;
        }
    }
}
