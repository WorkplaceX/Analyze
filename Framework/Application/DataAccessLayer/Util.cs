namespace Application.DataAccessLayer
{
    using Database.dbo;
    using System.Linq;

    public class Util
    {
        public static SyUser Select()
        {
            DbContextMain db = new DataAccessLayer.DbContextMain();
            return db.SyUser.First();
        }
    }
}
