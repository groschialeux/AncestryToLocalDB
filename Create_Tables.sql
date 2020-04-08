use AncestryGedCom
go

Create Table Family
(

	gedGuid UniqueIdentifier,
	FamilyID int,
	HusbandUID UniqueIdentifier,
	WifeUID UniqueIdentifier,
	MarriageDate nvarchar(30),
	MarriagePlace nvarchar(250),
	DivorceDate nvarchar(30),
	DivorcePlace nvarchar(250)
	Constraint [PK_Family] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Marriage
(
	gedGuid UniqueIdentifier,
	FamilyUID UniqueIdentifier,
	MarriageDate nvarchar(30),
	MarriagePlace nvarchar(250)
	Constraint [PK_Marriage] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Divorce
(
	gedGuid UniqueIdentifier,
	FamilyUID UniqueIdentifier,
	DivorceDate nvarchar(30),
	DivorcePlace nvarchar(250)
	Constraint [PK_Divorce] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Children
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	FamilyUID UniqueIdentifier,
	FatherRelation nvarchar(30),
	MotherRelation nvarchar(30)
	Constraint [PK_Children] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Source
(
	gedguid UniqueIdentifier,
	SourceType nvarchar(10),
	SourceID nvarchar(20),
	SourceAPID nvarchar(30),
	SourceData nvarchar(250),
	foreingUID UniqueIdentifier
	Constraint [PK_Source] Primary Key Clustered
	(
		gedGuid
	)
)


Create Table Person
(
	gedGuid UniqueIdentifier,
	PersonID int,
	Gender nvarchar(5),
	ChildOfFamily int
	Constraint [PK_Person] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Names
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	PersonName nvarchar(100),
	BirthUID UniqueIdentifier
	Constraint [PK_Names] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Birth
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	BirthDate nvarchar(30),
	BirthPlace nvarchar(250)
	Constraint [PK_Birth] Primary Key Clustered
	(
		gedGuid
	)
)


Create Table Baptism
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	BaptismDate nvarchar(30),
	BaptismPlace nvarchar(250)
	Constraint [PK_Baptism] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Death
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	DeathDate nvarchar(30),
	DeathPlace nvarchar(250),
	DeathDesc nvarchar(250)
	Constraint [PK_Death] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Burial
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	BurialDate nvarchar(30),
	BurialPlace nvarchar(250)
	Constraint [PK_Burial] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Residence
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	ResidenceDesc nvarchar(250),
	ResidenceDate nvarchar(30),
	ResidencePlace nvarchar(250)
	Constraint [PK_Residence] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Note
(
	gedGuid UniqueIdentifier,
	NoteType nvarchar(10),
	NoteText nvarchar(Max),
	foreignUID UniqueIdentifier
	Constraint [PK_Note] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Event
(
	gedGuid UniqueIdentifier,
	EventType nvarchar(10),
	EventDesc nvarchar(250),
	EventPlace nvarchar(30),
	foreignUID UniqueIdentifier
	Constraint [PK_Event] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Occupation
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	OccupationDesc nvarchar(250),
	OccupationDate nvarchar(30),
	OccupationPlace nvarchar(250),
	Constraint [PK_Occupation] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Document
(
	gedGuid UniqueIdentifier,
	DocName nvarchar(250),
	Form nvarchar(10),
	Title nvarchar(100),
	foreignUID UniqueIdentifier
	Constraint [PK_Document] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Employment
(
	gedGuid UniqueIdentifier,
	EmploymentDesc nvarchar(100),
	EmploymentDate nvarchar(30),
	EmploymentPlace nvarchar(250),
	PersonUID UniqueIdentifier
	Constraint [PK_Employment] Primary Key Clustered
	(
		gedGuid
	)
)

Create Table Spouse
(
	gedGuid UniqueIdentifier,
	PersonUID UniqueIdentifier,
	SpouseID int
	constraint [PK_Spouse] Primary Key Clustered
	(
		gedGuid
	)	
)

