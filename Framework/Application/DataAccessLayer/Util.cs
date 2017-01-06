namespace Application.DataAccessLayer
{
    using Database.dbo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Reflection;

    public class Util
    {
        private static IQueryable SelectQuery(Type typeRow)
        {
            DbContextMain dbContext = new DbContextMain(typeRow);
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeRow).Invoke(dbContext, null));
            return query;
        }

        public static object[] Select(Type typeRow, int id)
        {
            DbContextMain dbContext = new DbContextMain(typeof(SyUser));
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeRow).Invoke(dbContext, null));
            return query.Where("Id = @0", id).ToDynamicArray();
        }

        public static object[] Select(Type typeRow)
        {
            return SelectQuery(typeRow).ToDynamicArray();
        }

        public static TRow[] Select<TRow>() where TRow : Row
        {
            return Select(typeof(TRow)).Cast<TRow>().ToArray();
        }
    }
}
