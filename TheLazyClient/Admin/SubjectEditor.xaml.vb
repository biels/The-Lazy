Imports TheLazyClientMVVM
Public Class SubjectEditor
    Private _AcademicLevels As New List(Of Entities.AcademicLevelEntity)
    Public Property AcademicLevels() As List(Of Entities.AcademicLevelEntity)
        Get
            Return _AcademicLevels
        End Get
        Set(ByVal value As List(Of Entities.AcademicLevelEntity))
            _AcademicLevels = value
            UpdateMainDropdownUI()
        End Set
    End Property    
    Public ReadOnly Property SelectedAcademicLevel() As Entities.AcademicLevelEntity
        Get
            If cmbAcademicLevels.SelectedIndex = -1 Then Return Nothing
            If AcademicLevels Is Nothing Then Return Nothing
            Return AcademicLevels(cmbAcademicLevels.SelectedIndex)
        End Get

    End Property
    Dim selectedSubjectIndex As Integer = -1
    Private _CurrentSubjects As New List(Of Entities.SubjectEntity)
    Public ReadOnly Property SelectedSubject() As Entities.SubjectEntity
        Get
            If selectedSubjectIndex = -1 Then Return Nothing
            If _CurrentSubjects Is Nothing Then Return Nothing
            If (_CurrentSubjects.Count - 1) < selectedSubjectIndex Then Return Nothing
            Return _CurrentSubjects(selectedSubjectIndex)
        End Get

    End Property
    Sub FillAcademicLevels()
        AcademicLevels = c.cache.academic_level_cache.getAcademicLevelFullList()
    End Sub
    Sub FillCurrentSubjects()
        If SelectedAcademicLevel IsNot Nothing Then
            _CurrentSubjects = c.cache.subject_cache.getSubjectFullList(SelectedAcademicLevel.id, True)
        End If
        If _CurrentSubjects.Count > 0 Then
            lstSubjects.SelectedIndex = 0
        End If
    End Sub
    Sub UpdateMainDropdownUI()
        'AcademicLevles
        With cmbAcademicLevels.Items
            .Clear()
            For Each e As Entities.AcademicLevelEntity In AcademicLevels
                .Add(e)
            Next
            If .Count > 0 Then
                cmbAcademicLevels.SelectedIndex = 0
            End If
        End With       
    End Sub
    Sub UpdateAcademicLevelDependencies()
        updateListOnSelectionChanged = False
        'MainList
        FillCurrentSubjects()
        With lstSubjects.Items
            .Clear()
            For Each e As Entities.SubjectEntity In _CurrentSubjects
                .Add(New ListBoxItem() With {.Content = e, .Background = New SolidColorBrush(ColorFromInt(e.color))})
            Next
        End With
        updateListOnSelectionChanged = True
    End Sub
    Sub LoadSelectionToMainView()
        If SelectedSubject Is Nothing Then Exit Sub
        With SelectedSubject
            txtName.Text = .name
            txtShortCode.Text = .shortcode
            txtDescription.Text = .description
            btnChooseColor.Background = New SolidColorBrush(ColorFromInt(.color))
            'btnChooseColor.Background = New SolidColorBrush(Color.FromRgb(44, 66, 200))
        End With

    End Sub
    Sub SaveSelectedSubject()
        If SelectedSubject Is Nothing Or SelectedAcademicLevel Is Nothing Then
            'MsgBox("Selecciona una assignatura!")
            Exit Sub
        End If
        Dim c As System.Windows.Media.Color = DirectCast(btnChooseColor.Background, SolidColorBrush).Color
        DbClient.DbSubjectEditorClient.updateSubject(SelectedSubject.id, txtName.Text, txtShortCode.Text.ToUpper, ColorToInt(c), SelectedAcademicLevel.id, txtDescription.Text)
    End Sub
    Private Sub SubjectEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        FillAcademicLevels()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim c As New System.Windows.Forms.ColorDialog
        If c.ShowDialog() = Forms.DialogResult.OK Then
            btnChooseColor.Background = New SolidColorBrush(ConvertToMediaColor(c.Color))
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub


    Private Sub cmbAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAcademicLevels.SelectionChanged
        selectedSubjectIndex = -1
        UpdateAcademicLevelDependencies()
    End Sub
    Dim updateListOnSelectionChanged As Boolean = True
    Private Sub lstSubjects_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstSubjects.SelectionChanged
        SaveSelectedSubject()
        selectedSubjectIndex = lstSubjects.SelectedIndex
        LoadSelectionToMainView()
    End Sub
    Private Function StringFromRichTextBox(ByVal rtb As RichTextBox) As String
        ' TextPointer to the start of content in the RichTextBox.
        ' TextPointer to the end of content in the RichTextBox.
        Dim textRange As New TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd)

        ' The Text property on a TextRange object returns a string
        ' representing the plain text content of the TextRange.
        Return textRange.Text
    End Function
    Function AutoGenerateShortCode(name As String) As String
        name = name.Trim()
        If name.Length < 3 Then
            Return "XXX"
        End If
        Dim r As String = name.Substring(0, 3)
        Dim wsplit As List(Of String) = name.Split(" ").ToList
        wsplit.Remove("")
        If wsplit.Count > 3 Then
            wsplit.Remove("la")
            wsplit.Remove("el")
        End If
        If wsplit.Count > 3 Then
            wsplit.Remove("de")
            wsplit.Remove("del")
            wsplit.Remove("dels")
        End If
        If wsplit.Count > 3 Then
            wsplit.RemoveRange(3, wsplit.Count - 3 - 1)
        End If
        If wsplit.Count = 2 Then
            r = wsplit(0).Substring(0, 2) & wsplit(1).Substring(0, 1)
        End If
        If wsplit.Count = 3 Then
            r = ""
            For Each w As String In wsplit
                If w.Length = 0 Then Continue For
                Dim c As String = w.Substring(0, 1)
                If w.Split("'").Length = 2 Then
                    Dim trueword As String = w.Split("'")(1)
                    If trueword.Length > 0 Then
                        c = trueword.Substring(0, 1)
                    End If

                End If
                r &= c
            Next
        End If
        Return r.ToUpper
    End Function
    Private Sub btnAddSuubject_Click(sender As Object, e As RoutedEventArgs) Handles btnAddSuubject.Click
        If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.SuperModerator) Then Exit Sub
        If SelectedAcademicLevel Is Nothing Then Exit Sub
        Dim name As String
        Dim r1 As String = InputBox("Introdueix el nom de l'assignatura. Ex: Català. A continuació en podràs modificar els detalls", "Afegeix una assignatura...")
        If r1 <> "" Then
            name = r1
            TheLazyClientMVVM.DbClient.DbSubjectEditorClient.insertSubject(name, AutoGenerateShortCode(name), 0, SelectedAcademicLevel.id, "")
            UpdateAcademicLevelDependencies() 'Omple i mostra
            cmbAcademicLevels.SelectedItem = FindItemContaining(cmbAcademicLevels.Items, name)
            LoadSelectionToMainView()
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        Dim selectedindex As Integer = selectedSubjectIndex
        If selectedindex < 0 Then Exit Sub
        lstSubjects.SelectedIndex = selectedindex
        SaveSelectedSubject()
        UpdateAcademicLevelDependencies()
        lstSubjects.SelectedIndex = selectedindex
        LoadSelectionToMainView()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtName.TextChanged
        If chkAutogenerate Is Nothing Then Exit Sub
        If chkAutogenerate.IsChecked Then
            txtShortCode.Text = AutoGenerateShortCode(txtName.Text)
        End If
    End Sub


    Private Sub btnRemoveCurrent_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveCurrent.Click
        Dim selectedindex As Integer = selectedSubjectIndex
        If selectedindex < 0 Then Exit Sub
        If DbClient.DbSubjectEditorClient.deleteSubject(SelectedSubject.id) Then
            UpdateAcademicLevelDependencies()
        Else
            MsgBox("No s'ha pogut esborrar, possiblement hi hagin elements fent-hi referèrncia.")
        End If
    End Sub
End Class
