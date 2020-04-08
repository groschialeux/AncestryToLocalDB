Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    Dim myConnection As SqlConnection = New SqlConnection
    Dim Ancestry As New AncestryGed

    Private Sub bConnect_Click(sender As Object, e As EventArgs) Handles bConnect.Click
        Try
            myConnection.Open()
            Me.Label1.ForeColor = Color.Green
            bConnect.Enabled = False
            bDisconnect.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub bDisconnect_Click(sender As Object, e As EventArgs) Handles bDisconnect.Click
        If myConnection.State = ConnectionState.Open Then
            myConnection.Close()
            Me.Label1.ForeColor = Color.Maroon
            bConnect.Enabled = True
            bDisconnect.Enabled = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myConnection.ConnectionString = Me.connectString.Text
    End Sub

    Private Sub connectString_TextChanged(sender As Object, e As EventArgs) Handles connectString.TextChanged
        myConnection.ConnectionString = Me.connectString.Text
    End Sub

    Private Sub bSelect_Click(sender As Object, e As EventArgs) Handles bSelect.Click
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            GedcomFile.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub bImport_Click(sender As Object, e As EventArgs) Handles bImport.Click
        Me.Cursor = Cursors.WaitCursor
        If IO.File.Exists(GedcomFile.Text) Then
            Dim rType As Int16 = 0, gdata As String = "", CurrentLineNo As Int16 = 0

            Dim ged As String() = IO.File.ReadAllLines(GedcomFile.Text)

            Do Until CurrentLineNo = ged.Count - 1
                ReadGedLine(ged, CurrentLineNo, rType, gdata, True)
                If rType = 0 And gdata.Contains("INDI") Then
                    ProcessNewPerson(CurrentLineNo, ged, gdata)
                End If

                If rType = "0" And gdata.Contains("@ FAM") Then
                    ProcessNewFamily(CurrentLineNo, ged, gdata)
                End If

            Loop
            Me.Cursor = Cursors.Arrow
            MessageBox.Show("Import process complete !")
        End If
    End Sub

    Private Sub ProcessNewFamily(ByRef LineNo As Int16, ByRef ged As String(), ByVal gdata As String)
        Dim rType As Int16 = 0
        Dim myFamily As New Family(gdata.Replace("@ FAM", "").Replace("@F", ""))

        While True
            ReadGedLine(ged, LineNo, rType, gdata, True)

            If rType = 0 Then
                Exit While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "HUSB" Then
                myFamily.Husband = gdata.Replace("HUSB @P", "").Replace("@", "").Trim
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "WIFE" Then
                myFamily.Wife = gdata.Replace("WIFE @P", "").Replace("@", "").Trim
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "CHIL" Then
                Dim myChild As New Child(gdata.Replace("CHIL @P", "").Replace("@", ""))

                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType < 2 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 5) = "_FREL" Then
                        myChild.FatherRelation = gdata.Substring(5)
                        Continue While
                    End If

                    If Strings.Left(gdata, 5) = "_FREL" Then
                        myChild.FatherRelation = gdata.Substring(5, gdata.Length - 5).Trim.Replace("@", "")
                        Continue While
                    End If

                    If Strings.Left(gdata, 5) = "_MREL" Then
                        myChild.MotherRelation = gdata.Substring(5, gdata.Length - 5).Trim.Replace("@", "")
                        Continue While
                    End If

                End While
                myFamily.Children.Add(myChild)
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "MARR" Then
                Dim myMarriage As New Marriage
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType < 2 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        myMarriage.MarriageDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        myMarriage.MarriagePlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim MarriageSource As New Source("MARR", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(MarriageSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        myMarriage.MarriageSources.Add(MarriageSource)
                    End If

                End While
                myFamily.Marriage = myMarriage
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 3) = "DIV" Then
                Dim myDivorce As New DIvorce
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 0 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        myDivorce.DivorceDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        myDivorce.DivorcePlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim DivorceSource As New Source("DIV", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(DivorceSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        myDivorce.DivorceSources.Add(DivorceSource)
                    End If

                End While
                myFamily.Divorce = myDivorce
                LineNo = LineNo - 1
                Continue While
            End If

        End While
        Ancestry.Families.Add(myFamily)
        LineNo = LineNo - 1
        If myConnection.State = ConnectionState.Open Then
            AddDBFamily(myFamily)
        End If
    End Sub

    Private Sub ProcessNewPerson(ByRef LineNo As Int16, ByRef ged As String(), ByVal gdata As String)
        Dim rType As Int16 = 0
        Dim myPerson As New Person(gdata.Replace("@ INDI", "").Replace("@P", ""))

        While True
            ReadGedLine(ged, LineNo, rType, gdata, True)

            If rType = 0 Then
                Exit While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "NAME" Then
                Dim myName As New Name(gdata.Substring(4, gdata.Length - 4).Trim)
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim NameSource As New Source("NAME", gdata.Substring(4, gdata.Length - 4).Trim)
                        ProcessSource(NameSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        myName.Sources.Add(NameSource)
                    End If
                End While
                myPerson.Name = myName
                LineNo = LineNo - 1
                Continue While

            End If

            If rType = 1 And Strings.Left(gdata, 3) = "SEX" Then
                myPerson.Gender = gdata.Substring(3, gdata.Length - 3).Trim
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "BIRT" Then
                Dim Birth As New BirthInfo
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        Birth.BirthDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        Birth.BirthPlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim BirthSource As New Source("BIRT", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(BirthSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        Birth.Sources.Add(BirthSource)
                    End If

                End While
                myPerson.Birth.Add(Birth)
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "BAPM" Then
                Dim Baptism As New BaptismInfo
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        Baptism.BaptismDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        Baptism.BaptismPlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim BaptismSource As New Source("BAPM", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(BaptismSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        Baptism.Sources.Add(BaptismSource)
                    End If
                End While
                myPerson.Baptism.Add(Baptism)
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "DEAT" Then
                Dim Death As New DeathInfo(gdata.Substring(4, gdata.Length - 4).Trim)
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        Death.DeathDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        Death.DeathPlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim DeathSource As New Source("DEAT", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(DeathSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        Death.Sources.Add(DeathSource)
                    End If

                End While
                myPerson.Death.Add(Death)
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "BURI" Then
                Dim Burial As New BurialInfo
                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        Burial.BurialDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        Burial.BurialPlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim BurialSource As New Source("BURI", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(BurialSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        Burial.Sources.Add(BurialSource)
                    End If

                End While
                myPerson.Burial.Add(Burial)
                LineNo = LineNo - 1
                Continue While
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "FAMC" Then
                myPerson.ChildOfFamily = gdata.Substring(4, gdata.Length - 4).Trim.Replace("@F", "").Replace("@", "")
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "FAMS" Then
                Dim mySpouse As New Spouse(gdata.Substring(4, gdata.Length - 4).Trim.Replace("@F", "").Replace("@", ""))
                myPerson.Spouses.Add(mySpouse)
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "RESI" Then
                Dim myResidence As New Residence(gdata.Substring(4, gdata.Length - 4).Trim)

                While True
                    ReadGedLine(ged, LineNo, rType, gdata, True)

                    If rType = 1 Then
                        Exit While
                    End If

                    If Strings.Left(gdata, 4) = "DATE" Then
                        myResidence.ResidenceDate = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If

                    If Strings.Left(gdata, 4) = "PLAC" Then
                        myResidence.ResidencePlace = gdata.Substring(4, gdata.Length - 4).Trim
                        Continue While
                    End If
                    If Strings.Left(gdata, 4) = "SOUR" Then
                        Dim ResidenceSource As New Source("RESI", gdata.Substring(4, gdata.Length - 4).Trim.Replace("@", ""))
                        ProcessSource(ResidenceSource, ged, LineNo, rType, gdata)
                        LineNo = LineNo - 1
                        myResidence.Sources.Add(ResidenceSource)
                    End If
                End While
                myPerson.Residences.Add(myResidence)
                LineNo = LineNo - 1
                Continue While

            End If

            If rType = 1 And Strings.Left(gdata, 4) = "NOTE" Then
                Dim myNote As New Note(gdata.Substring(4, gdata.Length - 4).Trim)
                ProcessNote(myNote, ged, LineNo, rType, gdata)
                LineNo = LineNo - 1
                myPerson.Notes.Add(myNote)
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "EVEN" Then
                Dim myEvent As New LifeEvent(gdata.Substring(4, gdata.Length - 4).Trim)
                ProcessEvent(myEvent, ged, LineNo, rType, gdata)
                LineNo = LineNo - 1
                myPerson.Events.Add(myEvent)
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "OCCU" Then
                Dim myOccupation As New Occupation(gdata.Substring(4, gdata.Length - 4).Trim.Replace("@F", "").Replace("@", ""))
                ProcessOccupation(myOccupation, ged, LineNo, rType, gdata)
                LineNo = LineNo - 1
                myPerson.Occupations.Add(myOccupation)
            End If

            If rType = 1 And Strings.Left(gdata, 4) = "OBJE" Then
                Dim myObject As New AncestryToLocalDB.Object
                ProcessObject(myObject, ged, LineNo, rType, gdata)
                LineNo = LineNo - 1
                myPerson.Objects.Add(myObject)
            End If

            If rType = 1 And Strings.Left(gdata, 7) = "_EMPLOY" Then
                Dim myEmployment As New Employment(gdata.Substring(7, gdata.Length - 7).Trim)
                ProcessEmployment(myEmployment, ged, LineNo, rType, gdata)
                LineNo = LineNo - 1
                myPerson.Employments.Add(myEmployment)
            End If

        End While
        Ancestry.People.Add(myPerson)
        LineNo = LineNo - 1
        If myConnection.State = ConnectionState.Open Then
            AddDBPerson(myPerson)
        End If
    End Sub

    Private Sub ReadGedLine(ByRef ged As String(), ByRef Lineno As Int16, ByRef rType As Int16, ByRef gdata As String, ByVal increment As Boolean)
        rType = Strings.Left(ged(Lineno), 1)
        gdata = Mid(ged(Lineno), 3, 1000)
        If increment = True Then
            Lineno += 1
        End If
    End Sub

    Private Sub AddDBPerson(ByVal myPerson As Person)
        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@gedGuid", myPerson.guid))
        cmd.Parameters.Add(New SqlParameter("@PersonID", myPerson.PersonID))
        cmd.Parameters.Add(New SqlParameter("@Gender", myPerson.Gender))
        cmd.Parameters.Add(New SqlParameter("@ChildOfFamily", myPerson.ChildOfFamily))
        cmd.Parameters.Add(New SqlParameter("@PersonName", myPerson.Name.Name))
        cmd.Parameters.Add(New SqlParameter("@NameGuid", myPerson.Name.guid))


        cmd.CommandText = "AddPerson"
        cmd.ExecuteNonQuery()

        AddDBSources(myPerson.Name.Sources, myPerson.guid)

        For Each birth As BirthInfo In myPerson.Birth
            cmd.CommandText = "AddBirth"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", birth.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@BirthDate", birth.BirthDate))
            cmd.Parameters.Add(New SqlParameter("@BirthPlace", birth.BirthPlace))
            cmd.ExecuteNonQuery()
            AddDBSources(birth.Sources, birth.guid)
        Next

        For Each baptism As BaptismInfo In myPerson.Baptism
            cmd.CommandText = "AddBaptism"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", baptism.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@BaptismDate", baptism.BaptismDate))
            cmd.Parameters.Add(New SqlParameter("@BaptismPlace", baptism.BaptismPlace))
            cmd.ExecuteNonQuery()
            AddDBSources(baptism.Sources, baptism.guid)
        Next

        For Each death As DeathInfo In myPerson.Death
            cmd.CommandText = "AddDeath"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", death.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@DeathDate", death.DeathDate))
            cmd.Parameters.Add(New SqlParameter("@DeathPlace", death.DeathPlace))
            cmd.Parameters.Add(New SqlParameter("@DeathDesc", death.DeathDesc))
            cmd.ExecuteNonQuery()
            AddDBSources(death.Sources, death.guid)
        Next

        For Each burial As BurialInfo In myPerson.Burial
            cmd.CommandText = "Addburial"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", burial.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@BurialDate", burial.BurialDate))
            cmd.Parameters.Add(New SqlParameter("@BurialPlace", burial.BurialPlace))
            cmd.ExecuteNonQuery()
            AddDBSources(burial.Sources, burial.guid)
        Next

        For Each residence As Residence In myPerson.Residences
            cmd.CommandText = "AddResidence"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", residence.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@ResidenceDate", residence.ResidenceDate))
            cmd.Parameters.Add(New SqlParameter("@ResidencePlace", residence.ResidencePlace))
            cmd.Parameters.Add(New SqlParameter("@ResidenceDesc", residence.ResidenceDesc))
            cmd.ExecuteNonQuery()
            AddDBSources(residence.Sources, residence.guid)
        Next

        For Each xevent As LifeEvent In myPerson.Events
            cmd.CommandText = "AddEvent"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", xevent.guid))
            cmd.Parameters.Add(New SqlParameter("@EventType", xevent.EventType))
            cmd.Parameters.Add(New SqlParameter("@EventDesc", xevent.Description))
            cmd.Parameters.Add(New SqlParameter("@EventPlace", xevent.EventPlace))
            cmd.Parameters.Add(New SqlParameter("@foreignUID", myPerson.guid))
            cmd.ExecuteNonQuery()
        Next

        For Each occupation As Occupation In myPerson.Occupations
            cmd.CommandText = "AddOccupation"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", occupation.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@Date", occupation.Date))
            cmd.Parameters.Add(New SqlParameter("@Place", occupation.Place))
            cmd.Parameters.Add(New SqlParameter("@Desc", occupation.Description))
            cmd.ExecuteNonQuery()
            AddDBSources(occupation.Sources, occupation.guid)
        Next

        For Each document As AncestryToLocalDB.Object In myPerson.Objects
            cmd.CommandText = "AddDocument"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", document.guid))
            cmd.Parameters.Add(New SqlParameter("@DocName", document.File))
            cmd.Parameters.Add(New SqlParameter("@Form", document.Form))
            cmd.Parameters.Add(New SqlParameter("@Title", document.Title))
            cmd.Parameters.Add(New SqlParameter("@foreignUID", myPerson.guid))
            cmd.ExecuteNonQuery()
        Next


        For Each spouse As Spouse In myPerson.Spouses
            cmd.CommandText = "AddSpouse"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", spouse.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@SpouseID", spouse.SpouseID))
            cmd.ExecuteNonQuery()
        Next


        For Each employment As Employment In myPerson.Employments
            cmd.CommandText = "Addemployment"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", employment.guid))
            cmd.Parameters.Add(New SqlParameter("@PersonUID", myPerson.guid))
            cmd.Parameters.Add(New SqlParameter("@Desc", employment.Description))
            cmd.Parameters.Add(New SqlParameter("@Date", employment.Date))
            cmd.Parameters.Add(New SqlParameter("@Place", employment.Place))
            cmd.ExecuteNonQuery()
        Next

    End Sub

    Private Sub AddDBFamily(ByVal myFamily As Family)
        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandType = CommandType.StoredProcedure

        cmd.CommandText = "AddFamily"
        cmd.Parameters.Add(New SqlParameter("@gedGuid", myFamily.guid))
        cmd.Parameters.Add(New SqlParameter("@FamilyID", myFamily.FamilyID))
        cmd.Parameters.Add(New SqlParameter("@HusbandID", myFamily.Husband))
        cmd.Parameters.Add(New SqlParameter("@WifeID", myFamily.Wife))
        cmd.Parameters.Add(New SqlParameter("@MarriageDate", myFamily.Marriage.MarriageDate))
        cmd.Parameters.Add(New SqlParameter("@MarriagePlace", myFamily.Marriage.MarriagePlace))
        cmd.Parameters.Add(New SqlParameter("@DivorceDate", myFamily.Divorce.DivorceDate))
        cmd.Parameters.Add(New SqlParameter("@DivorcePlace", myFamily.Divorce.DivorcePlace))
        cmd.ExecuteNonQuery()
        AddDBSources(myFamily.Marriage.MarriageSources, myFamily.Marriage.guid)
        AddDBSources(myFamily.Divorce.DivorceSources, myFamily.Divorce.guid)

    End Sub

    Private Sub AddDBSources(ByVal sources As List(Of Source), ByVal foreingUID As Guid)
        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandType = CommandType.StoredProcedure

        For Each source As Source In sources
            cmd.CommandText = "AddSource"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(New SqlParameter("@gedGuid", source.guid))
            cmd.Parameters.Add(New SqlParameter("@SourceType", source.SourceType))
            cmd.Parameters.Add(New SqlParameter("@SourceID", source.SourceID))
            cmd.Parameters.Add(New SqlParameter("@SourceAPID", source.SourceAPID))
            cmd.Parameters.Add(New SqlParameter("@foreingUID", foreingUID))
            cmd.Parameters.Add(New SqlParameter("@SourceData", source.SourceData))
            cmd.ExecuteNonQuery()
        Next

    End Sub

    Private Sub ProcessSource(ByVal _source As Source, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)

        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype <> 3 Then
                Exit While
            End If

            If Strings.Left(_gdata, 5) = "_APID" Then
                _source.SourceAPID = Mid(_gdata, 7, 30)
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "PAGE" Then
                _source.SourceData = Mid(_gdata, 6, 1000)
                Continue While
            End If
        End While
    End Sub

    Private Sub ProcessEvent(ByVal _event As LifeEvent, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)
        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype = 1 Then
                Exit While
            End If

            If Strings.Left(_gdata, 4) = "TYPE" Then
                _event.EventType = Mid(_gdata, 6, 30)
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "PLAC" Then
                _event.EventPlace = Mid(_gdata, 6, 1000)
                Continue While
            End If
        End While
    End Sub

    Private Sub ProcessNote(ByVal _note As Note, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)
        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype = 1 Then
                Exit While
            End If

            If Strings.Left(_gdata, 4) = "CONC" Then
                _note.NoteText = _note.NoteText.Trim + Mid(_gdata, 6, 30)
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "CONT" Then
                _note.NoteText = _note.NoteText.Trim + vbCrLf + Mid(_gdata, 7, 30)
                Continue While
            End If
        End While
    End Sub

    Private Sub ProcessOccupation(ByVal _occupation As Occupation, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)

        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype = 1 Then
                Exit While
            End If

            If Strings.Left(_gdata, 4) = "DATE" Then
                _occupation.Date = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "PLAC" Then
                _occupation.Place = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "SOUR" Then
                Dim OccupationSource As New Source("OCCU", _gdata.Substring(4, _gdata.Length - 4).Trim.Replace("@", ""))
                ProcessSource(OccupationSource, _ged, _lineno, _rtype, _gdata)
                _lineno = _lineno - 1
                _occupation.Sources.Add(OccupationSource)
            End If
        End While
    End Sub

    Private Sub ProcessObject(ByVal _object As AncestryToLocalDB.Object, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)

        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype = 1 Then
                Exit While
            End If

            If Strings.Left(_gdata, 4) = "FILE" Then
                _object.File = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "FORM" Then
                _object.Form = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "TITL" Then
                _object.Title = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

        End While
    End Sub

    Private Sub ProcessEmployment(ByVal _employment As Employment, ByVal _ged As String(), ByRef _lineno As Int16, ByVal _rtype As Int16, ByVal _gdata As String)
        While True
            ReadGedLine(_ged, _lineno, _rtype, _gdata, True)
            If _rtype = 1 Then
                Exit While
            End If

            If Strings.Left(_gdata, 4) = "DATE" Then
                _employment.Date = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If

            If Strings.Left(_gdata, 4) = "PLAC" Then
                _employment.Place = _gdata.Substring(4, _gdata.Length - 4).Trim
                Continue While
            End If
        End While

    End Sub

End Class
