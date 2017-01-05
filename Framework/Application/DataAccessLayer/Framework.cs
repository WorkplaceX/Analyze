namespace Application.DataAccessLayer
{
    using Database.dbo;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Reflection;

    /// <summary>
    /// Base class for every database row.
    /// </summary>
    public class Row
    {
        protected virtual bool IsReadOnly()
        {
            return false;
        }
    }

    /// <summary>
    /// Base class for every database field.
    /// </summary>
    public class Cell
    {
        internal void Constructor(object row)
        {
            this.row = row;
        }

        private object row;

        public object Row
        {
            get
            {
                return row;
            }
        }

        protected virtual bool IsReadOnly()
        {
            return false;
        }
    }

    /// <summary>
    /// Base class for every database field.
    /// </summary>
    public class Cell<TRow> : Cell
    {
        public new TRow Row
        {
            get
            {
                return (TRow)base.Row;
            }
        }
    }

    /// <summary>
    /// Sql table name and field name.
    /// </summary>
    public class SqlNameAttribute : Attribute
    {
        public SqlNameAttribute(string sqlName)
        {
            this.SqlName = sqlName;
        }

        public readonly string SqlName;
    }

    public class DbContextMain : DbContext
    {
        public DbContextMain(Type[] rowTypeList)
        {
            this.RowTypeList = rowTypeList;
        }

        public DbContextMain(Type rowType) 
            : this(new Type[] { rowType })
        {

        }

        public readonly Type[] RowTypeList;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Application.ConnectionManager.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (Type rowType in RowTypeList)
            {
                var entity = modelBuilder.Entity(rowType);
                SqlNameAttribute attributeRow = (SqlNameAttribute)rowType.GetTypeInfo().GetCustomAttribute(typeof(SqlNameAttribute));
                entity.ToTable(attributeRow.SqlName);
                foreach (PropertyInfo propertyInfo in rowType.GetTypeInfo().GetProperties())
                {
                    SqlNameAttribute attributeProperty = (SqlNameAttribute)propertyInfo.GetCustomAttribute(typeof(SqlNameAttribute));
                    entity.Property(propertyInfo.PropertyType, propertyInfo.Name).HasColumnName(attributeProperty.SqlName);
                }
            }
            //
            base.OnModelCreating(modelBuilder);
        }
    }
}
