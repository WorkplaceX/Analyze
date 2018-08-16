using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Select
            var query = Query<My>().Where(item => item.Id == 1);
            My my = query.ToList().First();
            Select(query);

            // Update
            var dbContext = DbContext();
            My myUpdate = new My() { Id = my.Id, Text = my.Text };
            myUpdate.Text += ".";
            dbContext.Attach(my).CurrentValues.SetValues(myUpdate);
            dbContext.SaveChanges();

            // Insert
            My myInsert = new My() { Text = "New" };
            dbContext.Add(myInsert);
            dbContext.SaveChanges();

            // Select View
            List<MyView> myViewList = Query<MyView>().ToList();
            var myView = myViewList.First();

            // Update View
            myUpdate = new My() { Id = myView.Id, Text = myView.Text };
            myUpdate.Text += "V";
            dbContext.Attach(myView).CurrentValues.SetValues(myUpdate);
            dbContext.SaveChanges(); // Throws SQL Error MyView is not updatable, as expected.
        }

        public static DbContext DbContext()
        {
            var typeMappingSource = new SqlServerTypeMappingSource(new TypeMappingSourceDependencies(new ValueConverterSelector(new ValueConverterSelectorDependencies())), new RelationalTypeMappingSourceDependencies()); // EF Core 2.1

            // From SQL type to C# type.
            var type = typeMappingSource.FindMapping("nvarchar(max)").ClrType;

            var conventionSet = SqlServerConventionSetBuilder.Build();
            var builder = new ModelBuilder(conventionSet);

            // My
            var entity = builder.Entity(typeof(My));
            entity.ToTable("My");
            entity.Property("Id");
            entity.Property("Id").HasAnnotation(CoreAnnotationNames.TypeMapping, typeMappingSource.FindMapping(typeof(int)));
            entity.Property("Text").HasAnnotation(CoreAnnotationNames.TypeMapping, typeMappingSource.FindMapping(typeof(string)));

            // MyView
            entity = builder.Entity(typeof(MyView));
            entity.ToTable("MyView");
            entity.Property("Id");
            entity.Property("Id").HasAnnotation(CoreAnnotationNames.TypeMapping, typeMappingSource.FindMapping(typeof(int)));
            entity.Property("Text").HasAnnotation(CoreAnnotationNames.TypeMapping, typeMappingSource.FindMapping(typeof(string)));

            var model = builder.Model;
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer("Data Source=localhost; Initial Catalog=AdventureWorks2016; Integrated Security=True;").UseModel(model);

            var dbContext = new DbContext(options.Options);
            return dbContext;
        }

        public static IQueryable<T> Query<T>()
        {
            var dbContext = DbContext();
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeof(T)).Invoke(dbContext, new object[] { }));
            return (IQueryable<T>)query;
        }

        public static List<object> Select(IQueryable query)
        {
            var list = query.ToDynamicList();
            //List<Row> result = list.Cast<Row>().ToList();
            return list;

        }
    }
    public class My
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }

    public class MyView
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
