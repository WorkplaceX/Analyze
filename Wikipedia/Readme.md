# Country List
Following article shows how to read country list from Wikipedia and create a (*.sql) script. Program reads Wikipedia country list and  from https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2 and converts country table into a (*.csv) file to load into a database. Resolves also theWwikipedia country links.

Columns:
* Code
* Country
* CountryLink
* Year
* CcTLD
* CcTLDLink
* Iso
* IsoLink
* Notes

# Import (*.csv) into sql server
Bulk import a (*.csv) file like this:

![](Doc/SqlFlatFileImport.png)

![](Doc/SqlFlatFileImportDialog.png)

Straight forward bulk import for staging area without data type conversion:
![](Doc/SqlFlatFileImportColumn.png)

# Export data from sql server to (*.sql) script
Export data table to (*.sql) script like this:
![](Doc/SqlExport.png)

![](Doc/SqlExportDialog.png)

Remove GO statements with Notepas++

![](Doc/NotepadReplaceGO.png)
