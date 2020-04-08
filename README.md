# AncestryToLocalDB
## Convert Ancestry gedcom to LocalDB Database

This program will load a gedcom file exported from Ancestry.com into a .NET Class and optionally save it to a MSSQLLOCALDB database.

### Usage steps.

1. Create a LocalDB Instance: This is an optional step, you can either create a dedicated instance or use the default one. It is recommended to create a dedicated instance.  You can do so with the sqllocaldb utility at a commande promtp.

        sqllocaldb create AncestryToLocalDB

2. Create a database in the new LocalDB instance.  You can do it using the sqlcmd utility.  In this example our database is called AncestryGedCom.

        sqlcmd -S "(LocalDB)\AncestryToLocalDB" -Q "create database AncestryGedCom;"


3. Use sqlcmd to execute the create_tables.sql and create_procedures.sql scripts.

        sqlcmd -S "(LocalDB)\AncestryToLocalDB" -d AncestryGedCom -i create_tables.sql  
        sqlcmd -S "(LocalDB)\AncestryToLocalDB" -d AncestryGedCom -i create_procedures.sql

4. Open the AncestryToLocalDB Visual Studio solution and run it to import a Ancestry gedcom into your new LocalDB Database.

Enter the connection string and click "Connect".
![Image 1](https://github.com/groschialeux/AncestryToLocalDB/blob/master/img/Image1.JPG)
        
Select your gedcom file.
![Image 2](https://github.com/groschialeux/AncestryToLocalDB/blob/master/img/Image2.JPG)
        
Click "Import".  When the import is completed click "Ok" then "Disconnect".
![Image 3](https://github.com/groschialeux/AncestryToLocalDB/blob/master/img/Image2.JPG)

###### Note:  At this point this program has only been tested with my own personal gedcom file exported from the ancestry.ca web site. The "Ancestry", "Family" and "Person" classes have been constructed from that gedcom, not from official gedcom specifications.  Many gedcom tags are currently not covered by this.

###### Support for currently missing tags will be added as needed from the developer and as the community steps in to contribute in making this a complete supporting the entire set of gedcom tags.


