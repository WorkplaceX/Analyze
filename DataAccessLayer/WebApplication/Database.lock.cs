namespace Database.dbo
{
    using System;
    using Framework;

    public partial class Data : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public partial class Data2 : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public partial class FrameworkSession : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public int? UserId { get; set; }
    }

    public partial class FrameworkUser : Row
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public partial class Log : Row
    {
        public int Id { get; set; }

        public DateTime? Time { get; set; }

        public string Ip { get; set; }

        public Guid? ExternalName { get; set; }

        public Guid? SessionName { get; set; }
    }

    public partial class LoginSession : Row
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public partial class LoginUser : Row
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsReseller { get; set; }

        public int? ResellerId { get; set; }

        public string Company { get; set; }
    }

    public partial class Product : Row
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public string Image { get; set; }
    }

    public partial class TableLoginSession : Row
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public partial class TableLoginUser : Row
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsReseller { get; set; }

        public int? ResellerId { get; set; }

        public string Company { get; set; }
    }

    public partial class TableProduct : Row
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }

    public partial class Version : Row
    {
        public int Id { get; set; }

        public string Version2 { get; set; }
    }
}

namespace Database.lp
{
    using System;
    using Framework;

    public partial class Data : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }
}

namespace Database
{
}
