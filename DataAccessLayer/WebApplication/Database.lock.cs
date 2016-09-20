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

    public partial class Data_Id : Cell<Data> { }

    public partial class Data_Name : Cell<Data> { }

    public partial class Data_Value : Cell<Data> { }

    public partial class Data_LogId : Cell<Data> { }

    public partial class Data2 : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public byte[] Value { get; set; }

        public int LogId { get; set; }
    }

    public partial class Data2_Id : Cell<Data2> { }

    public partial class Data2_Name : Cell<Data2> { }

    public partial class Data2_Value : Cell<Data2> { }

    public partial class Data2_LogId : Cell<Data2> { }

    public partial class FrameworkSession : Row
    {
        public int Id { get; set; }

        public Guid Name { get; set; }

        public int? UserId { get; set; }
    }

    public partial class FrameworkSession_Id : Cell<FrameworkSession> { }

    public partial class FrameworkSession_Name : Cell<FrameworkSession> { }

    public partial class FrameworkSession_UserId : Cell<FrameworkSession> { }

    public partial class FrameworkUser : Row
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public partial class FrameworkUser_Id : Cell<FrameworkUser> { }

    public partial class FrameworkUser_Email : Cell<FrameworkUser> { }

    public partial class FrameworkUser_Password : Cell<FrameworkUser> { }

    public partial class Log : Row
    {
        public int Id { get; set; }

        public DateTime? Time { get; set; }

        public string Ip { get; set; }

        public Guid? ExternalName { get; set; }

        public Guid? SessionName { get; set; }
    }

    public partial class Log_Id : Cell<Log> { }

    public partial class Log_Time : Cell<Log> { }

    public partial class Log_Ip : Cell<Log> { }

    public partial class Log_ExternalName : Cell<Log> { }

    public partial class Log_SessionName : Cell<Log> { }

    public partial class LoginSession : Row
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public partial class LoginSession_Id : Cell<LoginSession> { }

    public partial class LoginSession_LoginUserId : Cell<LoginSession> { }

    public partial class LoginSession_Token : Cell<LoginSession> { }

    public partial class LoginSession_DateTime : Cell<LoginSession> { }

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

    public partial class LoginUser_Id : Cell<LoginUser> { }

    public partial class LoginUser_Email : Cell<LoginUser> { }

    public partial class LoginUser_Password : Cell<LoginUser> { }

    public partial class LoginUser_IsAdmin : Cell<LoginUser> { }

    public partial class LoginUser_IsReseller : Cell<LoginUser> { }

    public partial class LoginUser_ResellerId : Cell<LoginUser> { }

    public partial class LoginUser_Company : Cell<LoginUser> { }

    public partial class Product : Row
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public string Image { get; set; }
    }

    public partial class Product_Id : Cell<Product> { }

    public partial class Product_Title : Cell<Product> { }

    public partial class Product_Price : Cell<Product> { }

    public partial class Product_IsActive : Cell<Product> { }

    public partial class Product_IsDelete : Cell<Product> { }

    public partial class Product_Image : Cell<Product> { }

    public partial class TableLoginSession : Row
    {
        public int Id { get; set; }

        public int? LoginUserId { get; set; }

        public Guid? Token { get; set; }

        public DateTime? DateTime { get; set; }
    }

    public partial class TableLoginSession_Id : Cell<TableLoginSession> { }

    public partial class TableLoginSession_LoginUserId : Cell<TableLoginSession> { }

    public partial class TableLoginSession_Token : Cell<TableLoginSession> { }

    public partial class TableLoginSession_DateTime : Cell<TableLoginSession> { }

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

    public partial class TableLoginUser_Id : Cell<TableLoginUser> { }

    public partial class TableLoginUser_Email : Cell<TableLoginUser> { }

    public partial class TableLoginUser_Password : Cell<TableLoginUser> { }

    public partial class TableLoginUser_IsAdmin : Cell<TableLoginUser> { }

    public partial class TableLoginUser_IsReseller : Cell<TableLoginUser> { }

    public partial class TableLoginUser_ResellerId : Cell<TableLoginUser> { }

    public partial class TableLoginUser_Company : Cell<TableLoginUser> { }

    public partial class TableProduct : Row
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }

    public partial class TableProduct_Id : Cell<TableProduct> { }

    public partial class TableProduct_Title : Cell<TableProduct> { }

    public partial class TableProduct_Price : Cell<TableProduct> { }

    public partial class TableProduct_IsActive : Cell<TableProduct> { }

    public partial class TableProduct_IsDelete : Cell<TableProduct> { }

    public partial class Version : Row
    {
        public int Id { get; set; }

        public string Version2 { get; set; }
    }

    public partial class Version_Id : Cell<Version> { }

    public partial class Version_Version2 : Cell<Version> { }
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

    public partial class Data_Id : Cell<Data> { }

    public partial class Data_Name : Cell<Data> { }

    public partial class Data_Value : Cell<Data> { }

    public partial class Data_LogId : Cell<Data> { }
}
