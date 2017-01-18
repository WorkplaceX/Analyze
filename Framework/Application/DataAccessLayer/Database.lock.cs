namespace Database.dbo
{
    using Application.DataAccessLayer;

    [SqlName("Airport")]
    public partial class Airport : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Text")]
        public string Text { get; set; }

        [SqlName("Code")]
        public string Code { get; set; }

        [SqlName("CountryId")]
        public int? CountryId { get; set; }
    }

    public partial class Airport_Id : Cell<Airport> { }

    public partial class Airport_Text : Cell<Airport> { }

    public partial class Airport_Code : Cell<Airport> { }

    public partial class Airport_CountryId : Cell<Airport> { }

    [SqlName("AirportDisplay")]
    public partial class AirportDisplay : Row
    {
        [SqlName("AirportId")]
        public int AirportId { get; set; }

        [SqlName("AirportText")]
        public string AirportText { get; set; }

        [SqlName("AirportCode")]
        public string AirportCode { get; set; }

        [SqlName("CountryId")]
        public int? CountryId { get; set; }

        [SqlName("CountryText")]
        public string CountryText { get; set; }

        [SqlName("CountryContinent")]
        public string CountryContinent { get; set; }
    }

    public partial class AirportDisplay_AirportId : Cell<AirportDisplay> { }

    public partial class AirportDisplay_AirportText : Cell<AirportDisplay> { }

    public partial class AirportDisplay_AirportCode : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryId : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryText : Cell<AirportDisplay> { }

    public partial class AirportDisplay_CountryContinent : Cell<AirportDisplay> { }

    [SqlName("Country")]
    public partial class Country : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Text")]
        public string Text { get; set; }

        [SqlName("TextShort")]
        public string TextShort { get; set; }

        [SqlName("Continent")]
        public string Continent { get; set; }
    }

    public partial class Country_Id : Cell<Country> { }

    public partial class Country_Text : Cell<Country> { }

    public partial class Country_TextShort : Cell<Country> { }

    public partial class Country_Continent : Cell<Country> { }

    [SqlName("ImportAirport")]
    public partial class ImportAirport : Row
    {
        [SqlName("Text")]
        public string Text { get; set; }

        [SqlName("Code")]
        public string Code { get; set; }

        [SqlName("CountryTextShort")]
        public string CountryTextShort { get; set; }
    }

    public partial class ImportAirport_Text : Cell<ImportAirport> { }

    public partial class ImportAirport_Code : Cell<ImportAirport> { }

    public partial class ImportAirport_CountryTextShort : Cell<ImportAirport> { }

    [SqlName("ImportCountry")]
    public partial class ImportCountry : Row
    {
        [SqlName("Text")]
        public string Text { get; set; }

        [SqlName("TextShort")]
        public string TextShort { get; set; }

        [SqlName("Continent")]
        public string Continent { get; set; }
    }

    public partial class ImportCountry_Text : Cell<ImportCountry> { }

    public partial class ImportCountry_TextShort : Cell<ImportCountry> { }

    public partial class ImportCountry_Continent : Cell<ImportCountry> { }

    [SqlName("ImportExcel")]
    public partial class ImportExcel : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("FileNameId")]
        public int? FileNameId { get; set; }

        [SqlName("SheetNameId")]
        public int? SheetNameId { get; set; }

        [SqlName("Row")]
        public int? Row { get; set; }

        [SqlName("ColumnNameId")]
        public int? ColumnNameId { get; set; }

        [SqlName("ValueNumber")]
        public double? ValueNumber { get; set; }

        [SqlName("ValueText")]
        public string ValueText { get; set; }
    }

    public partial class ImportExcel_Id : Cell<ImportExcel> { }

    public partial class ImportExcel_FileNameId : Cell<ImportExcel> { }

    public partial class ImportExcel_SheetNameId : Cell<ImportExcel> { }

    public partial class ImportExcel_Row : Cell<ImportExcel> { }

    public partial class ImportExcel_ColumnNameId : Cell<ImportExcel> { }

    public partial class ImportExcel_ValueNumber : Cell<ImportExcel> { }

    public partial class ImportExcel_ValueText : Cell<ImportExcel> { }

    [SqlName("ImportExcelDisplay")]
    public partial class ImportExcelDisplay : Row
    {
        [SqlName("ExcelId")]
        public int ExcelId { get; set; }

        [SqlName("FileNameId")]
        public int? FileNameId { get; set; }

        [SqlName("FileName")]
        public string FileName { get; set; }

        [SqlName("SheetNameId")]
        public int? SheetNameId { get; set; }

        [SqlName("SheetName")]
        public string SheetName { get; set; }

        [SqlName("Row")]
        public int? Row { get; set; }

        [SqlName("ColumnNameId")]
        public int? ColumnNameId { get; set; }

        [SqlName("ColumnName")]
        public string ColumnName { get; set; }

        [SqlName("ValueNumber")]
        public double? ValueNumber { get; set; }

        [SqlName("ValueText")]
        public string ValueText { get; set; }
    }

    public partial class ImportExcelDisplay_ExcelId : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_FileNameId : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_FileName : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_SheetNameId : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_SheetName : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_Row : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_ColumnNameId : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_ColumnName : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_ValueNumber : Cell<ImportExcelDisplay> { }

    public partial class ImportExcelDisplay_ValueText : Cell<ImportExcelDisplay> { }

    [SqlName("ImportName")]
    public partial class ImportName : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }
    }

    public partial class ImportName_Id : Cell<ImportName> { }

    public partial class ImportName_Name : Cell<ImportName> { }

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

    [SqlName("TableName")]
    public partial class TableName : Row
    {
        [SqlName("TableName2")]
        public string TableName2 { get; set; }

        [SqlName("IsView")]
        public bool? IsView { get; set; }
    }

    public partial class TableName_TableName2 : Cell<TableName> { }

    public partial class TableName_IsView : Cell<TableName> { }

    [SqlName("TempExcel")]
    public partial class TempExcel : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("FileNameId")]
        public int? FileNameId { get; set; }

        [SqlName("SheetNameId")]
        public int? SheetNameId { get; set; }

        [SqlName("Row")]
        public int? Row { get; set; }

        [SqlName("ColumnNameId")]
        public int? ColumnNameId { get; set; }

        [SqlName("ValueNumber")]
        public double? ValueNumber { get; set; }

        [SqlName("ValueText")]
        public string ValueText { get; set; }
    }

    public partial class TempExcel_Id : Cell<TempExcel> { }

    public partial class TempExcel_FileNameId : Cell<TempExcel> { }

    public partial class TempExcel_SheetNameId : Cell<TempExcel> { }

    public partial class TempExcel_Row : Cell<TempExcel> { }

    public partial class TempExcel_ColumnNameId : Cell<TempExcel> { }

    public partial class TempExcel_ValueNumber : Cell<TempExcel> { }

    public partial class TempExcel_ValueText : Cell<TempExcel> { }

    [SqlName("TempExcelDisplay")]
    public partial class TempExcelDisplay : Row
    {
        [SqlName("ExcelId")]
        public int ExcelId { get; set; }

        [SqlName("FileNameId")]
        public int? FileNameId { get; set; }

        [SqlName("FileName")]
        public string FileName { get; set; }

        [SqlName("SheetNameId")]
        public int? SheetNameId { get; set; }

        [SqlName("SheetName")]
        public string SheetName { get; set; }

        [SqlName("Row")]
        public int? Row { get; set; }

        [SqlName("ColumnNameId")]
        public int? ColumnNameId { get; set; }

        [SqlName("ColumnName")]
        public string ColumnName { get; set; }

        [SqlName("ValueNumber")]
        public double? ValueNumber { get; set; }

        [SqlName("ValueText")]
        public string ValueText { get; set; }
    }

    public partial class TempExcelDisplay_ExcelId : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_FileNameId : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_FileName : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_SheetNameId : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_SheetName : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_Row : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_ColumnNameId : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_ColumnName : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_ValueNumber : Cell<TempExcelDisplay> { }

    public partial class TempExcelDisplay_ValueText : Cell<TempExcelDisplay> { }

    [SqlName("TempName")]
    public partial class TempName : Row
    {
        [SqlName("Id")]
        public int Id { get; set; }

        [SqlName("Name")]
        public string Name { get; set; }
    }

    public partial class TempName_Id : Cell<TempName> { }

    public partial class TempName_Name : Cell<TempName> { }
}
