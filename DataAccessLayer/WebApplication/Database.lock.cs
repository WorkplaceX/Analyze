namespace Database.dbo
{
    using System;
    using Framework;

    public class Data
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public class Data2
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public class FrameworkSession
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public int? UserId { get; set; }
    }

    public class FrameworkUser
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }

        public DateTime? Time { get; set; }

        public string Ip { get; set; }

        public Guid? ExternalName { get; set; }

        public Guid? SessionName { get; set; }
    }

    public class LoginSession
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public class LoginUser
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsReseller { get; set; }

        public int? ResellerId { get; set; }

        public string Company { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public string Image { get; set; }
    }

    public class TableLoginSession
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public class TableLoginUser
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsReseller { get; set; }

        public int? ResellerId { get; set; }

        public string Company { get; set; }
    }

    public class TableProduct
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }

    public class Version
    {
        public int Id { get; set; }

        public string Version2 { get; set; }
    }

    public partial class Data_ : Row<Data> { }

    public partial class Data2_ : Row<Data2> { }

    public partial class FrameworkSession_ : Row<FrameworkSession> { }

    public partial class FrameworkUser_ : Row<FrameworkUser> { }

    public partial class Log_ : Row<Log> { }

    public partial class LoginSession_ : Row<LoginSession> { }

    public partial class LoginUser_ : Row<LoginUser> { }

    public partial class Product_ : Row<Product> { }

    public partial class TableLoginSession_ : Row<TableLoginSession> { }

    public partial class TableLoginUser_ : Row<TableLoginUser> { }

    public partial class TableProduct_ : Row<TableProduct> { }

    public partial class Version_ : Row<Version> { }
}

namespace Database.lp
{
    using System;
    using Framework;

    public class Data
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public partial class Data_ : Row<Data> { }
}

namespace Database
{
}
