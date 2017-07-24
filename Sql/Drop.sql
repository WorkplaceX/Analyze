/* Drop all table and views starting with 'Framework' */

/* Drop all Framework table constraints */
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + ' DROP CONSTRAINT ' + QUOTENAME(name)  + ';' + CHAR(10) 
FROM sys.foreign_keys
WHERE 
	QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) LIKE '\[dbo\].\[Framework%' ESCAPE '\'
EXEC sys.sp_executesql @sql;
GO
-- Drop all Framework table
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'DROP TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(object_id)) + '.' + QUOTENAME(OBJECT_NAME(object_id)) + CHAR(13)
FROM sys.tables
WHERE 
	QUOTENAME(OBJECT_SCHEMA_NAME(object_id)) + '.' + QUOTENAME(OBJECT_NAME(object_id)) LIKE '\[dbo\].\[Framework%' ESCAPE '\' AND NOT 
	QUOTENAME(OBJECT_SCHEMA_NAME(object_id)) + '.' + QUOTENAME(OBJECT_NAME(object_id)) = '[dbo].[FrameworkVersion]'
EXEC sys.sp_executesql @sql;
GO
-- Drop all Framework views
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'DROP VIEW ' + QUOTENAME(OBJECT_SCHEMA_NAME(object_id)) + '.' + QUOTENAME(OBJECT_NAME(object_id)) + CHAR(13)
FROM sys.views
WHERE 
	QUOTENAME(OBJECT_SCHEMA_NAME(object_id)) + '.' + QUOTENAME(OBJECT_NAME(object_id)) LIKE '\[dbo\].\[Framework%' ESCAPE '\'
EXEC sys.sp_executesql @sql;
