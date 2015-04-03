Imports TheLazyClientMVVM.Entities
Public Class GroupEditor

    Private _EducationCenters As List(Of EducationCenterEntity)
    Public ReadOnly Property EducationCenters() As List(Of EducationCenterEntity)
        Get
            Return _EducationCenters
        End Get
    End Property
    Private _AcademicLevels As List(Of AcademicLevelEntity)
    Public ReadOnly Property AcademicLevels() As List(Of AcademicLevelEntity)
        Get
            Return c.cache.academic_level_cache.getAcademicLevelFullList()
        End Get

    End Property
    Public Sub Init()
        _EducationCenters = c.cache.education_center_cache.getEducationCenterFullList()
        _AcademicLevels = c.cache.academic_level_cache.getAcademicLevelFullList()
    End Sub
    'Private _Groups As List(Of GroupEntity)
    'Public ReadOnly Property Groups As List(Of GroupEntity)
    '    Get
    '        _Groups = TheLazyClientMVVM.DbClient.DbGroupEditor.getGroupList(SelectedEducationCenter, SelectedAcademicLevel)
    '        Return _Groups
    '    End Get
    'End Property
    Public ReadOnly Property SelectedEducationCenter() As EducationCenterEntity
        Get
            If lstEduactrionCenters.SelectedIndex < 0 Then Return Nothing
            Return EducationCenters(lstEduactrionCenters.SelectedIndex)
        End Get
    End Property
    Public ReadOnly Property SelectedAcademicLevel() As AcademicLevelEntity
        Get
            If lstAcademicLevels.SelectedIndex < 0 Then Return Nothing
            Return AcademicLevels(lstAcademicLevels.SelectedIndex)
        End Get
    End Property
    'Private _SelectedGroup As GroupEntity
    'Public ReadOnly Property SelectedGroup() As GroupEntity
    '    Get
    '        If lstGoupCodes.SelectedIndex < 0 Then Return Nothing
    '        _SelectedGroup = _Groups(lstGoupCodes.SelectedIndex) 'En funció dels altres 2
    '        'UpdateSelectionDesc()
    '        Return _SelectedGroup
    '    End Get
    'End Property
    Public Sub UpdateUI()
        'EducationCenters
        lstEduactrionCenters.Items.Clear()
        For Each e As EducationCenterEntity In EducationCenters
            lstEduactrionCenters.Items.Add(e.name)
        Next
        'AcademicLevels
        lstAcademicLevels.Items.Clear()
        For Each e As AcademicLevelEntity In AcademicLevels
            lstAcademicLevels.Items.Add(e.name)
        Next
       

    End Sub
    'Sub UpdateGroups()
    '    'Groups
    '    lstGoupCodes.Items.Clear()
    '    If SelectedAcademicLevel IsNot Nothing And SelectedEducationCenter IsNot Nothing Then
    '        For Each e As GroupEntity In Groups
    '            lstGoupCodes.Items.Add(e.group_code)
    '        Next
    '    End If

    'End Sub
    'Sub UpdateSelectionDesc()

    '    lblSelectionString.Content = If(SelectedGroup() Is Nothing, "[Selecciona un grup]", _SelectedGroup)
    '    btnSelect.IsEnabled = SelectedGroup() IsNot Nothing
    'End Sub
    Private Sub lstEduactrionCenters_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstEduactrionCenters.SelectionChanged
        'UpdateUI()
        'UpdateGroups()
    End Sub

    Private Sub lstAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstAcademicLevels.SelectionChanged
        'UpdateUI()
        'UpdateGroups()
    End Sub

  

    Private Sub lstGoupCodes_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstGoupCodes.SelectionChanged
        ' UpdateSelectionDesc()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Sub AfegirCentre()
        If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.SuperModerator) Then Exit Sub
        Dim name, loaction As String
        Dim r1 As String = InputBox("Introdueix el nom del centre", "Afegeix un centre...", "IES [Nom]")
        If r1 <> "" Then
            name = r1
            Dim r2 As String = InputBox("Introdueix la localitat. Ex: Santpedor", "Afegeix un centre...", "[Localitat]")
            If r1 <> "" Then
                loaction = r2
                TheLazyClientMVVM.DbClient.DbGroupEditor.insertEducationCenter(name, loaction)
            End If
        End If
        UpdateUI()
    End Sub
    Sub AfegirNivellAcademic()
        If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.SuperModerator) Then Exit Sub
        Dim name As String
        Dim r1 As String = InputBox("Introdueix el nom del nivell. Ex: 1r BTX", "Afegeix un nivell...")
        If r1 <> "" Then
            name = r1
            TheLazyClientMVVM.DbClient.DbGroupEditor.insertAcademicLevel(name)
        End If
        UpdateUI()
    End Sub
    'Sub AfegirGrup()
    '    If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.Trusted) Then Exit Sub
    '    Dim name As String
    '    Dim commoncodes() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "X", "Y", "Z"}
    '    Dim r1 As String = InputBox("Introdueix el codi del grup. Aquest ha de ser un sol caràcter Ex: A", "Afegeix un grup...", commoncodes(_Groups.Count))
    '    If r1 <> "" And r1.Length = 1 Then
    '        name = r1.ToUpper
    '        For Each g As GroupEntity In _Groups
    '            If g.group_code = name Then
    '                MsgBox("Ja existeix un grup amb aquest codi de grup")
    '                Exit Sub
    '            End If
    '        Next
    '        TheLazyClientMVVM.DbClient.DbGroupEditor.insertGroup(name, SelectedAcademicLevel, SelectedEducationCenter)
    '    End If
    '    UpdateUI()
    'End Sub
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        AfegirCentre()
    End Sub

    Private Sub btnAddAcademicLevel_Click(sender As Object, e As RoutedEventArgs) Handles btnAddAcademicLevel.Click
        AfegirNivellAcademic()
    End Sub

    Private Sub btnAddGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnAddGroup.Click
        'AfegirGrup()
    End Sub

    
End Class
