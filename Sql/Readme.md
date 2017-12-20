# Autoincrement
Select with calculated autoincrement:
```Sql
SELECT 
    ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) AS Id 
FROM 
    Table
```
