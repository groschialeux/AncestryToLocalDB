Public Class Person
    Public guid As Guid
    Public PersonID As Int16
    Public Name As Name
    Public Gender As String
    Public ChildOfFamily As Int16
    Public Birth As List(Of BirthInfo)
    Public Baptism As List(Of BaptismInfo)
    Public Death As List(Of DeathInfo)
    Public Burial As List(Of BurialInfo)
    Public Residences As List(Of Residence)
    Public Notes As List(Of Note)
    Public Events As List(Of LifeEvent)
    Public Occupations As List(Of Occupation)
    Public Objects As List(Of Object)
    Public Spouses As List(Of Spouse)
    Public Employments As List(Of Employment)

    Public Sub New()
        guid = Guid.NewGuid()
        PersonID = 0
        Name = New Name
        Gender = ""
        ChildOfFamily = 0
        Birth = New List(Of BirthInfo)
        Baptism = New List(Of BaptismInfo)
        Death = New List(Of DeathInfo)
        Burial = New List(Of BurialInfo)
        Residences = New List(Of Residence)
        Notes = New List(Of Note)
        Events = New List(Of LifeEvent)
        Occupations = New List(Of Occupation)
        Objects = New List(Of Object)
        Spouses = New List(Of Spouse)
        Employments = New List(Of Employment)
    End Sub

    Public Sub New(ByVal _PersonID As Int16)
        guid = Guid.NewGuid()
        PersonID = _PersonID
        Name = New Name
        Gender = ""
        ChildOfFamily = 0
        Birth = New List(Of BirthInfo)
        Baptism = New List(Of BaptismInfo)
        Death = New List(Of DeathInfo)
        Burial = New List(Of BurialInfo)
        Residences = New List(Of Residence)
        Notes = New List(Of Note)
        Events = New List(Of LifeEvent)
        Occupations = New List(Of Occupation)
        Objects = New List(Of Object)
        Spouses = New List(Of Spouse)
        Employments = New List(Of Employment)
    End Sub

    Public Sub New(ByVal _guid As Guid, ByVal _PersonID As Int16)
        guid = _guid
        PersonID = _PersonID
        Name = New Name
        Gender = ""
        ChildOfFamily = 0
        Birth = New List(Of BirthInfo)
        Baptism = New List(Of BaptismInfo)
        Death = New List(Of DeathInfo)
        Burial = New List(Of BurialInfo)
        Residences = New List(Of Residence)
        Notes = New List(Of Note)
        Events = New List(Of LifeEvent)
        Occupations = New List(Of Occupation)
        Objects = New List(Of Object)
        Spouses = New List(Of Spouse)
        Employments = New List(Of Employment)
    End Sub

End Class

Public Class Name
    Public guid As Guid
    Public Name As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid
        Name = ""
        Sources = New List(Of Source)
    End Sub

    Public Sub New(ByVal _name As String)
        guid = Guid.NewGuid
        Name = _name
        Sources = New List(Of Source)
    End Sub

End Class

Public Class BirthInfo
    Public guid As Guid
    Public BirthDate As String
    Public BirthPlace As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid()
        BirthDate = ""
        BirthPlace = ""
        Sources = New List(Of Source)
    End Sub
End Class

Public Class BaptismInfo
    Public guid As Guid
    Public BaptismDate As String
    Public BaptismPlace As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid()
        BaptismDate = ""
        BaptismPlace = ""
        Sources = New List(Of Source)
    End Sub
End Class

Public Class DeathInfo
    Public guid As Guid
    Public DeathDesc As String
    Public DeathDate As String
    Public DeathPlace As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid()
        DeathDesc = ""
        DeathDate = ""
        DeathPlace = ""
        Sources = New List(Of Source)
    End Sub

    Public Sub New(ByVal _deathdesc As String)
        guid = Guid.NewGuid()
        DeathDesc = _deathdesc
        DeathDate = ""
        DeathPlace = ""
        Sources = New List(Of Source)

    End Sub
End Class

Public Class BurialInfo
    Public guid As Guid
    Public BurialDate As String
    Public BurialPlace As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid()
        BurialDate = ""
        BurialPlace = ""
        Sources = New List(Of Source)
    End Sub
End Class

Public Class Source
    Public guid As Guid
    Public SourceType As String
    Public SourceID As String
    Public SourceAPID As String
    Public SourceData As String

    Public Sub New()
        guid = Guid.NewGuid
        SourceType = ""
        SourceID = ""
        SourceAPID = ""
        SourceData = ""
    End Sub

    Public Sub New(ByVal _sourceType As String, ByVal _sourceID As String)
        guid = Guid.NewGuid
        SourceType = _sourceType
        SourceID = _sourceID
        SourceAPID = ""
        SourceData = ""
    End Sub

End Class

Public Class Residence
    Public guid As Guid
    Public ResidenceDesc As String
    Public ResidenceDate As String
    Public ResidencePlace As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid()
        ResidenceDate = ""
        ResidenceDesc = ""
        ResidencePlace = ""
        Sources = New List(Of Source)
    End Sub

    Public Sub New(ByVal _residencedesc As String)
        guid = Guid.NewGuid()
        ResidenceDate = ""
        ResidenceDesc = _residencedesc
        ResidencePlace = ""
        Sources = New List(Of Source)
    End Sub

End Class

Public Class Note
    Public guid As Guid
    Public NoteText As String

    Public Sub New()
        guid = Guid.NewGuid
        NoteText = ""
    End Sub

    Public Sub New(ByVal _noteText As String)
        guid = Guid.NewGuid
        NoteText = _noteText
    End Sub

End Class

Public Class LifeEvent
    Public guid As Guid
    Public EventType As String
    Public Description As String
    Public EventPlace As String

    Public Sub New()
        guid = Guid.NewGuid
        EventType = ""
        Description = ""
        EventPlace = ""
    End Sub

    Public Sub New(ByVal _description As String)
        guid = Guid.NewGuid
        EventType = ""
        Description = _description
        EventPlace = ""
    End Sub

End Class

Public Class Occupation
    Public guid As Guid
    Public Description As String
    Public [Date] As String
    Public Place As String
    Public Sources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid
        Description = ""
        [Date] = ""
        Place = ""
        Sources = New List(Of Source)
    End Sub

    Public Sub New(ByVal _description As String)
        guid = Guid.NewGuid
        Description = _description
        [Date] = ""
        Place = ""
        Sources = New List(Of Source)
    End Sub
End Class

Public Class [Object]
    Public guid As Guid
    Public File As String
    Public Form As String
    Public Title As String

    Public Sub New()
        guid = Guid.NewGuid
        File = ""
        Form = ""
        Title = ""
    End Sub

End Class

Public Class Employment
    Public guid As Guid
    Public Description As String
    Public [Date] As String
    Public Place As String

    Public Sub New()
        guid = Guid.NewGuid
        Description = ""
        [Date] = ""
        Place = ""
    End Sub

    Public Sub New(ByVal _description As String)
        guid = Guid.NewGuid
        Description = _description
        [Date] = ""
        Place = ""

    End Sub
End Class

Public Class Spouse
    Public guid As Guid
    Public SpouseID As Int16

    Public Sub New()
        guid = Guid.NewGuid
        SpouseID = 0
    End Sub

    Public Sub New(_spouse As Int16)
        guid = Guid.NewGuid
        SpouseID = _spouse
    End Sub
End Class

