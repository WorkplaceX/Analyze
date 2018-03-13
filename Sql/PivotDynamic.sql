DECLARE 
    @table NVARCHAR(257) = N'[Application].[dbo].[Country]';
DECLARE 
    @sql NVARCHAR(MAX) = N'',
    @columns NVARCHAR(MAX) = N'',
    @columnsConvert NVARCHAR(MAX) = N'';
SELECT 
    @columns += ', ' + QUOTENAME(name),
    @columnsConvert += ', CONVERT(NVARCHAR(256), ' + QUOTENAME(name) + ') AS ' + QUOTENAME(name)
FROM 
    sys.columns ColumnX
WHERE 
    ColumnX.OBJECT_ID = OBJECT_ID(@table)
SELECT @sql = N'
SELECT 
    N''' + @table + ''' AS TableName,
    Field.FieldName, 
    Field.Value,
    TYPE_NAME(ColumnX.system_type_id) AS ValueType
FROM 
(
    SELECT ' + STUFF(@columnsConvert, 1, 1, '') + ' FROM ' + @table + '
) AS Data
UNPIVOT
(
    Value FOR FieldName IN (' + STUFF(@columns, 1, 2, '') + ')
) AS Field
LEFT JOIN sys.columns AS ColumnX ON ColumnX.OBJECT_ID = OBJECT_ID(N''' + @table + ''') AND ColumnX.name = Field.FieldName
';
--PRINT @sql;
EXEC sp_executesql @sql;