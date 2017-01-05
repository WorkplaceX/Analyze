namespace Application.DataAccessLayer
{
    public class ConnectionManager
    {
        public static string ConnectionString
        {
            get
            {
                return Application.ConnectionManager.ConnectionString;
            }
        }
    }
}
