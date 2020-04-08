Public Class Family

    Public guid As Guid
    Public FamilyID As Int16
    Public Husband As Int16
    Public Wife As Int16
    Public Marriage As Marriage
    Public Divorce As Divorce
    Public Children As List(Of Child)


    Public Sub New()
        guid = Guid.NewGuid
        FamilyID = 0
        Husband = 0
        Wife = 0
        Marriage = New Marriage
        Divorce = New Divorce
        Children = New List(Of Child)
    End Sub

    Public Sub New(ByVal _familyid As Int16)
        guid = Guid.NewGuid
        FamilyID = _familyid
        Husband = 0
        Wife = 0
        Marriage = New Marriage
        Divorce = New Divorce
        Children = New List(Of Child)
    End Sub

End Class

Public Class Marriage
    Public guid As Guid
    Public MarriageDate As String
    Public MarriagePlace As String
    Public MarriageSources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid
        MarriageDate = ""
        MarriagePlace = ""
        MarriageSources = New List(Of Source)
    End Sub

End Class

Public Class Divorce
    Public guid As Guid
    Public DivorceDate As String
    Public DivorcePlace As String
    Public DivorceSources As List(Of Source)

    Public Sub New()
        guid = Guid.NewGuid
        DivorceDate = ""
        DivorcePlace = ""
        DivorceSources = New List(Of Source)
    End Sub

End Class

Public Class Child
    Public guid As Guid
    Public PersonID As Int16
    Public FatherRelation As String
    Public MotherRelation As String

    Public Sub New()
        guid = Guid.NewGuid
        PersonID = 0
        FatherRelation = ""
        MotherRelation = ""
    End Sub

    Public Sub New(ByVal _personID As Int16)
        guid = Guid.NewGuid
        PersonID = _personID
        FatherRelation = ""
        MotherRelation = ""
    End Sub
End Class
