namespace Database.dbo
{
    public class Data
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string LogId { get; set; }
    }

    public class FrameworkSession
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }

    public class FrameworkUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class Log
    {
        public string Id { get; set; }

        public string Time { get; set; }

        public string Ip { get; set; }

        public string ExternalName { get; set; }

        public string SessionName { get; set; }
    }

    public class LoginSession
    {
        public string Id { get; set; }

        public string LoginUserId { get; set; }

        public string Token { get; set; }

        public string DateTime { get; set; }
    }

    public class LoginUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string IsAdmin { get; set; }

        public string IsReseller { get; set; }

        public string ResellerId { get; set; }

        public string Company { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Price { get; set; }

        public string IsActive { get; set; }

        public string IsDelete { get; set; }

        public string Image { get; set; }
    }

    public class TableLoginSession
    {
        public string Id { get; set; }

        public string LoginUserId { get; set; }

        public string Token { get; set; }

        public string DateTime { get; set; }
    }

    public class TableLoginUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string IsAdmin { get; set; }

        public string IsReseller { get; set; }

        public string ResellerId { get; set; }

        public string Company { get; set; }
    }

    public class TableProduct
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Price { get; set; }

        public string IsActive { get; set; }

        public string IsDelete { get; set; }
    }

    public class Version
    {
        public string Id { get; set; }

        public string Version_ { get; set; }
    }
}

namespace Database.lp
{
    public class Data
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string LogId { get; set; }
    }
}
