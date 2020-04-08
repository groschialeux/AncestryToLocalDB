use AncestryGedCom
Go

Create Procedure AddFamily(
	@gedGuid UniqueIdentifier,
	@FamilyID int,
	@HusbandID int,
	@WifeID Int,
	@MarriageDate nvarchar(30),
	@MarriagePlace nvarchar(250),
	@DivorceDate nvarchar(30),
	@DivorcePlace nvarchar(250)
)
as
Declare @HusbandUID UniqueIdentifier, @WifeUID UniqueIdentifier
Select	@HusbandUID = gedGuid from Person where PersonID = @HusbandID
Select	@WifeUID = gedGuid from Person where PersonID = @WifeID
Insert into Family(gedGuid, FamilyID, HusbandUID, WifeUID, MarriageDate, MarriagePlace, DivorceDate, DivorcePlace) Values (@gedGuid, @FamilyID, @HusbandUID, @WifeUID, @MarriageDate, @MarriagePlace, @DivorceDate, @DivorcePlace)
Go

Create Procedure AddPerson(
	@gedGuid UniqueIdentifier,
	@PersonID int,
	@Gender nvarchar(5),
	@ChildOfFamily int,
	@PersonName nvarchar(100),
	@NameGuid UniqueIdentifier
)
as
Insert into Person(gedGuid, PersonID, Gender, ChildOfFamily) Values(@gedGuid, @PersonID, @Gender, @ChildOfFamily)
Insert into Names(gedGuid, PersonUID, PersonName) Values (@NameGuid, @gedGuid, @PersonName)
go

Create Procedure AddBirth(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@BirthDate nvarchar(30),
	@BirthPlace nvarchar(250)
)
as
Insert into Birth(gedGuid, PersonUID, BirthDate, BirthPlace) Values(@gedGuid, @PersonUID, @BirthDate, @BirthPlace)
Go

Create Procedure AddBaptism(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@BaptismDate nvarchar(30),
	@BaptismPlace nvarchar(250)
)
as
Insert into Baptism(gedguid, PersonUID, BaptismDate, BaptismPlace) Values(@gedGuid, @PersonUID, @BaptismDate, @BaptismPlace)
Go

Create Procedure AddDeath(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@DeathDate nvarchar(30),
	@DeathPlace nvarchar(250),
	@DeathDesc nvarchar(250)
)
as
Insert into Death(gedguid, PersonUID, DeathDate, DeathPlace,DeathDesc) Values(@gedGuid, @PersonUID, @DeathDate, @DeathPlace,@DeathDesc)
Go


Create Procedure AddBurial(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@BurialDate nvarchar(30),
	@BurialPlace nvarchar(250)
)
as
Insert into Burial(gedguid, PersonUID, BurialDate, BurialPlace) Values(@gedGuid, @PersonUID, @BurialDate, @BurialPlace)
Go

Create Procedure AddResidence(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@ResidenceDate nvarchar(30),
	@ResidencePlace nvarchar(250),
	@ResidenceDesc nvarchar(250)
)
as
Insert into Residence(gedguid, PersonUID, ResidenceDate, ResidencePlace,ResidenceDesc) Values(@gedGuid, @PersonUID, @ResidenceDate, @ResidencePlace,@ResidenceDesc)
Go

Create Procedure AddSource(
	@gedGuid UniqueIdentifier,
	@SourceType nVarchar(10),
	@SourceID nvarchar(20),
	@SourceAPID nvarchar(30),
	@SourceData nVarchar(250),
	@foreingUID UniqueIdentifier
)
as
Insert into Source(gedGuid, SourceType, SourceID, SourceAPID, SourceData, foreingUID) Values(@gedGuid, @SourceType, @SourceID, @SourceAPID, @SourceData, @foreingUID)
Go

Create Procedure AddEvent(
	@gedGuid UniqueIdentifier,
	@EventType nvarchar(10),
	@EventDesc nvarchar(250),
	@EventPlace nvarchar(30),
	@foreignUID UniqueIdentifier
)
as
Insert into Event(gedGuid, EventType, EventDesc, EventPlace, foreignUID) values(@gedGuid, @EventType, @EventDesc, @EventPlace, @foreignUID)
Go

Create Procedure AddOccupation(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@Desc nvarchar(250),
	@Date nvarchar(30),
	@Place nvarchar(250)
)
as 
Insert into Occupation(gedGuid, PersonUID, OccupationDesc,OccupationDate,OccupationPlace) Values(@gedGuid, @PersonUID, @Desc, @Date, @Place)
Go

Create Procedure AddDocument(
	@gedGuid UniqueIdentifier,
	@DocName nvarchar(100),
	@Form nvarchar(10),
	@Title nvarchar(100),
	@foreignUID UniqueIdentifier
)
as
Insert into Document(gedGuid, DocName, Form, Title, foreignUID) Values (@gedGuid,@DocName, @Form, @Title, @foreignUID)
Go

Create Procedure AddSpouse(
	@gedGuid UniqueIdentifier,
	@PersonUID UniqueIdentifier,
	@SpouseID Int
)
as
Insert into Spouse(gedGuid, PersonUID, SpouseID) Values(@gedGuid, @PersonUID, @SpouseID)
Go

Create Procedure AddEmployment(
	@gedGuid UniqueIdentifier,
	@Desc nvarchar(100),
	@Date nvarchar(30),
	@Place nvarchar(250),
	@PersonUID UniqueIdentifier
)
as
Insert into Employment(gedGuid, EmploymentDesc, EmploymentDate, EmploymentPlace, PersonUID) Values (@gedGuid, @Desc, @Date, @Place, @PersonUID)
Go

Create Procedure TruncateAllTables
as
Truncate Table Baptism
Truncate Table Birth
Truncate Table Burial
Truncate Table Children
Truncate Table Death
Truncate Table Divorce
Truncate Table Document
Truncate Table Employment
Truncate Table Event
Truncate Table Family
Truncate Table Marriage
Truncate Table Names
Truncate Table Note
Truncate Table Occupation
Truncate Table Person
Truncate Table Residence
Truncate Table Source
Truncate Table Spouse
Go

