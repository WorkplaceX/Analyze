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
        public static object[] Select(Type typeRow, int id)
        {
            DbContextMain dbContext = new DbContextMain(typeof(SyUser));
            IQueryable query = (IQueryable)(dbContext.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(typeRow).Invoke(dbContext, null));
            return query.Where("Id = @0", id).ToDynamicArray();
        }

        public static T[] Select<T>(int id)
        {
            return Select(typeof(T), id).Cast<T>().ToArray();
        }
    }
}
