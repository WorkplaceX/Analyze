/* SQL Pivot */
SELECT User_SK, FieldName, Value
FROM [Database].[dboDatabase].[User]
UNPIVOT (Value FOR FieldName in (email, firstname)) AS Contact
