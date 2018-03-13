# Autoincrement
Select with calculated autoincrement:
```Sql
SELECT 
    ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) AS Id 
FROM 
    Table
```

# Pivot Dynamic
Following sql turns any table or view into key value pair structure.

[PivotDynamic.sql](PivotDynamic.sql)

![PivotDynamic](Doc/PivotDynamic.png)
