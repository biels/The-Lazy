Imports TheLazyClientMVVM
Public Class ProfileEditor
    'Private _SelectedGroup As TheLazyClientMVVM.Entities.GroupEntity
    'Public Property SelectedGroup() As TheLazyClientMVVM.Entities.GroupEntity
    '    Get
    '        Return _SelectedGroup
    '    End Get
    '    Set(ByVal value As TheLazyClientMVVM.Entities.GroupEntity)
    '        _SelectedGroup = value
    '        'If value IsNot Nothing Then lblGroupPreview.Text = value.ToString
    '    End Set
    'End Property

    'Private Sub btnSelectGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnSelectGroup.Click
    '    Dim frm As New GroupEditor
    '    frm.Init()
    '    frm.UpdateUI()
    '    frm.ShowDialog()
    '    If frm.SelectedGroup IsNot Nothing Then
    '        'Establir el grup seleccionat
    '        SelectedGroup = frm.SelectedGroup
    '    End If
    'End Sub
    Dim _academic_levels As List(Of Entities.AcademicLevelEntity)
    Dim _education_centers As List(Of Entities.EducationCenterEntity)
    Dim _group_codes() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K"} ', "L", "M", "N", "O", "P", "Q", "R", "S", "T", "X", "Y", "Z"}
    Sub FillDataFromServer()
        _academic_levels = DbClient.DbGroupEditor.getAcademicLevelList
        _education_centers = DbClient.DbGroupEditor.getEducationCenterList
    End Sub
    Sub FillComboboxes()
        With cmbAcademicLevel.Items
            .Clear()
            For Each e As Entities.AcademicLevelEntity In _academic_levels
                .Add(e)
            Next
        End With
        With cmbEducationCenter.Items
            .Clear()
            For Each e As Entities.EducationCenterEntity In _education_centers
                .Add(e)
            Next
        End With
        With cmbGroupCode.Items
            .Clear()
            For Each s As String In _group_codes
                .Add(s)
            Next
        End With
        With cmbGender.Items
            .Clear()
            .Add("Masculí")
            .Add("Femení")
        End With
    End Sub
    Sub LoadLocalValues()
        With c.localUser
            txtFullName.Text = .real_name
            txtEmail.Text = .email
            cmbAcademicLevel.SelectedItem = FindItemContaining(cmbAcademicLevel.Items, .academic_level)
            cmbEducationCenter.SelectedItem = FindItemContaining(cmbEducationCenter.Items, .education_center)
            cmbGender.SelectedItem = FindItemContaining(cmbGender.Items, .gender)
            cmbGroupCode.SelectedValue = .group_code
        End With
        
        'SelectedGroup = c.localUser.group
    End Sub
    Private Function FindItemContaining(items As IEnumerable, target As Object) As Object
        If items Is Nothing Then Return Nothing
        If target Is Nothing Then Return Nothing
        For Each item As Object In items
            If item.ToString().Contains(target.ToString) Then
                Return item
            End If
        Next
        ' Return null;
        Return Nothing
    End Function

    Sub SaveChanges()
        Dim academic_level As Entities.AcademicLevelEntity = cmbAcademicLevel.SelectedItem
        Dim education_center As Entities.EducationCenterEntity = cmbEducationCenter.SelectedItem
        Dim gender As Entities.UserEntity.GenderEnum = cmbGender.SelectedIndex
        TheLazyClientMVVM.DbClient.DbUserClient.updateUser(c.localUser.username, txtEmail.Text, txtFullName.Text, gender.ToString, cmbGroupCode.Text, academic_level.id, education_center.id)
    End Sub
    Public Function CanSave()
        Dim err As New List(Of String)

        If txtFullName.Text = "" Then err.Add("Introdueix el teu nom real")
        If txtFullName.Text.Length < 8 Then err.Add("El nom real ha de contenir com a mínim 8 caràcters, Nom i Cognoms")
        If txtFullName.Text.Length > 40 Then err.Add("El nom real pot contenir com a màxim 40 caràcters")

        If txtFullName.Text = "" Then err.Add("Introdueix una aderça de correu electrònic")
        If txtEmail.Text.Length < 8 And txtEmail.Text <> "test" Then err.Add("El correu ha de contenir com a mínim 8 caràcters")
        If txtEmail.Text.Length > 32 Then err.Add("El correu pot contenir com a màxim 16 caràcters")
        If Not (txtEmail.Text = "test" Or txtEmail.Text = "" Or (txtEmail.Text.Contains("@") And txtEmail.Text.EndsWith(".com") And (txtEmail.Text.Contains("gmail") Or txtEmail.Text.Contains("outlook") Or txtEmail.Text.Contains("msn") Or txtEmail.Text.Contains("google") Or txtEmail.Text.Contains("hotmail")))) Then err.Add("L'adreça de correu electrònic no és vàlida.")

        If cmbAcademicLevel.SelectedIndex = -1 Then err.Add("Selecciona un nivell acadèmic")

        If cmbEducationCenter.SelectedIndex = -1 Then err.Add("Selecciona un centre educatiu")

        If cmbGroupCode.SelectedIndex = -1 Then err.Add("Selecciona un codi de grup (A, B, C, ..)")

        If cmbGroupCode.SelectedIndex = -1 Then err.Add("Selecciona el gènere")

        If err.Count > 0 Then
            Dim s As String = "No es pot completar l'operació, detalls: "
            For Each err_m As String In err
                s = s & vbCrLf & "  + " & err_m
            Next
            MsgBox(s)
            Return False
        End If
        Return True
    End Function
    Private Sub btnSaveChanges_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChanges.Click
        If CanSave() Then
            SaveChanges()
            'Tancar finalment
            Me.Close()
        Else

        End If

    End Sub

    Private Sub ProfileEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Title = "Carregant..."
        FillDataFromServer()
        FillComboboxes()
        LoadLocalValues()
        Me.Title = "Edita el teu perfil"
    End Sub
    Private Sub GroupEditor_Activated(sender As Object, e As EventArgs) Handles Me.Activated
       

        'MsgBox("Activated")
    End Sub
End Class
