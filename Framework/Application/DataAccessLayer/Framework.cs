namespace Application.DataAccessLayer
{
    using Database.dbo;
    using Microsoft.EntityFrameworkCore;

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

    public class DbContextMain : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Application.ConnectionManager.ConnectionString);
        }

        public DbSet<SyUser> SyUser { get; set; }
    }
}
