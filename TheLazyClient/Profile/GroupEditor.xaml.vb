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
            Return TheLazyClientMVVM.DbClient.DbGroupEditor.getAcademicLevelList()
        End Get

    End Property
    Public Sub Init()
        _EducationCenters = TheLazyClientMVVM.DbClient.DbGroupEditor.getEducationCenterList()
        _AcademicLevels = TheLazyClientMVVM.DbClient.DbGroupEditor.getAcademicLevelList()
    End Sub
    Public ReadOnly Property Groups As List(Of GroupEntity)
        Get
            Return TheLazyClientMVVM.DbClient.DbGroupEditor.getGroupList(SelectedEducationCenter, SelectedAcademicLevel)
        End Get
    End Property
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
    Public ReadOnly Property SelectedGroup() As GroupEntity
        Get
            If lstGoupCodes.SelectedIndex < 0 Then Return Nothing
            Return Groups(lstGoupCodes.SelectedIndex) 'En funció dels altres 2
        End Get
    End Property
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
    Sub UpdateGroups()
        'Groups
        lstGoupCodes.Items.Clear()
        If SelectedAcademicLevel IsNot Nothing And SelectedEducationCenter IsNot Nothing Then
            For Each e As GroupEntity In Groups
                lstGoupCodes.Items.Add(e.group_code)
            Next
        End If
    End Sub
    Private Sub lstEduactrionCenters_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstEduactrionCenters.SelectionChanged
        'UpdateUI()
        UpdateGroups()
    End Sub

    Private Sub lstAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstAcademicLevels.SelectionChanged
        'UpdateUI()
        UpdateGroups()
    End Sub
End Class
