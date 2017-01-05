namespace Database.dbo
{
    using Application.DataAccessLayer;

    [SqlName("LoLoation")]
    public partial class LoLoation : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }
    }

    public partial class LoLoation_Id : Cell<LoLoation> { }

    public partial class LoLoation_Name : Cell<LoLoation> { }

    [SqlName("LoRole")]
    public partial class LoRole : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }

        [SqlName("IsAdmin")]
        public bool IsAdmin { get; set; }
    }

    public partial class LoRole_Id : Cell<LoRole> { }

    public partial class LoRole_Name : Cell<LoRole> { }

    public partial class LoRole_IsAdmin : Cell<LoRole> { }

    [SqlName("LoRoleAccess")]
    public partial class LoRoleAccess : Row
    {
        [SqlName("UserId")]
        public int UserId { get; set; }

        [SqlName("UserName")]
        public string UserName { get; set; }

        [SqlName("LoationId")]
        public int? LoationId { get; set; }

        [SqlName("LoationName")]
        public string LoationName { get; set; }
    }

    public partial class LoRoleAccess_UserId : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_UserName : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_LoationId : Cell<LoRoleAccess> { }

    public partial class LoRoleAccess_LoationName : Cell<LoRoleAccess> { }

    [SqlName("LoRoleLoation")]
    public partial class LoRoleLoation : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("RoleId")]
        public int? RoleId { get; set; }

        [SqlName("UserId")]
        public int? UserId { get; set; }

        [SqlName("LoationId")]
        public int LoationId { get; set; }

        [SqlName("IsActive")]
        public bool IsActive { get; set; }
    }

    public partial class LoRoleLoation_Id : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_RoleId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_UserId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_LoationId : Cell<LoRoleLoation> { }

    public partial class LoRoleLoation_IsActive : Cell<LoRoleLoation> { }

    [SqlName("LoRoleMatrix")]
    public partial class LoRoleMatrix : Row
    {
        [SqlName("UserId")]
        public int UserId { get; set; }

        [SqlName("UserName")]
        public string UserName { get; set; }

        [SqlName("LoationId")]
        public int LoationId { get; set; }

        [SqlName("LoationName")]
        public string LoationName { get; set; }

        [SqlName("RoleId")]
        public int RoleId { get; set; }

        [SqlName("RoleName")]
        public string RoleName { get; set; }

        [SqlName("IsRole")]
        public bool? IsRole { get; set; }

        [SqlName("IsDirect")]
        public bool? IsDirect { get; set; }

        [SqlName("IsAdmin")]
        public bool? IsAdmin { get; set; }

        [SqlName("IsAdminModule")]
        public bool? IsAdminModule { get; set; }

        [SqlName("IsAccess")]
        public int? IsAccess { get; set; }
    }

    public partial class LoRoleMatrix_UserId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_UserName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_LoationId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_LoationName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_RoleId : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_RoleName : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsRole : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsDirect : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAdmin : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAdminModule : Cell<LoRoleMatrix> { }

    public partial class LoRoleMatrix_IsAccess : Cell<LoRoleMatrix> { }

    [SqlName("LoRoleUser")]
    public partial class LoRoleUser : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("UserId")]
        public int UserId { get; set; }

        [SqlName("RoleId")]
        public int RoleId { get; set; }

        [SqlName("IsActive")]
        public bool IsActive { get; set; }
    }

    public partial class LoRoleUser_Id : Cell<LoRoleUser> { }

    public partial class LoRoleUser_UserId : Cell<LoRoleUser> { }

    public partial class LoRoleUser_RoleId : Cell<LoRoleUser> { }

    public partial class LoRoleUser_IsActive : Cell<LoRoleUser> { }

    [SqlName("SyRole")]
    public partial class SyRole : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }

        [SqlName("IsAdmin")]
        public bool? IsAdmin { get; set; }
    }

    public partial class SyRole_Id : Cell<SyRole> { }

    public partial class SyRole_Name : Cell<SyRole> { }

    public partial class SyRole_IsAdmin : Cell<SyRole> { }

    [SqlName("SyRoleAccess")]
    public partial class SyRoleAccess : Row
    {
        [SqlName("UserId")]
        public int UserId { get; set; }

        [SqlName("UserName")]
        public string UserName { get; set; }

        [SqlName("RoleId")]
        public int? RoleId { get; set; }

        [SqlName("RoleName")]
        public string RoleName { get; set; }

        [SqlName("IsAdmin")]
        public bool? IsAdmin { get; set; }
    }

    public partial class SyRoleAccess_UserId : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_UserName : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_RoleId : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_RoleName : Cell<SyRoleAccess> { }

    public partial class SyRoleAccess_IsAdmin : Cell<SyRoleAccess> { }

    [SqlName("SyRoleUser")]
    public partial class SyRoleUser : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("UserId")]
        public int UserId { get; set; }

        [SqlName("RoleId")]
        public int RoleId { get; set; }

        [SqlName("IsActive")]
        public bool IsActive { get; set; }
    }

    public partial class SyRoleUser_Id : Cell<SyRoleUser> { }

    public partial class SyRoleUser_UserId : Cell<SyRoleUser> { }

    public partial class SyRoleUser_RoleId : Cell<SyRoleUser> { }

    public partial class SyRoleUser_IsActive : Cell<SyRoleUser> { }

    [SqlName("SyUser")]
    public partial class SyUser : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }
    }

    public partial class SyUser_Id : Cell<SyUser> { }

    public partial class SyUser_Name : Cell<SyUser> { }
}
