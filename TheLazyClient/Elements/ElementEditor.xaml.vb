Imports TheLazyClientMVVM
Public Class ElementEditor
    Private _IsNew As Boolean
    Public Property IsNew() As Boolean
        Get
            Return _IsNew
        End Get
        Set(ByVal value As Boolean)
            _IsNew = value           
        End Set
    End Property
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
        End Set
    End Property

    Sub LoadValues()
        If Element Is Nothing Then Return
        With Element
            txtTitle.Text = Element.name
            txtDescription.Text = .description
            txtPrice.Text = .price
        End With
        If IsNew Then
            btnPublish.Content = "Publica"
        Else
            btnPublish.Content = "Aplica"
        End If
    End Sub
    Sub SaveAction()
        If CanSave() Then
            If IsNew Then
                InsertNewElement()
            Else
                UpdateElement()
            End If
        End If
       
    End Sub
    Sub InsertNewElement()
        DbClient.DbElementClient.insertElement(c.localUser.id, SelectedSubject.id, txtTitle.Text, txtDescription.Text, txtPrice.Text)
    End Sub
    Sub UpdateElement()
        DbClient.DbElementClient.updateElement(Element.id, c.localUser.id, SelectedSubject.id, txtTitle.Text, txtDescription.Text, txtPrice.Text)
    End Sub
    Function CanSave() As Boolean 'Mostar missatge
        'Comprova errors
        Dim err As New List(Of String)
        If txtTitle.Text.Length < 5 Then err.Add("Introdueix un títol. El títol ha de contenir com a mínim 5 caràcters.")
        If txtTitle.Text.Length > 32 Then err.Add("El títol pot contenir com a màxim 32 caràcters.")
        If cmbAcademicLevel.SelectedIndex = -1 Then err.Add("Selecciona un nivell acadèmic.")
        If cmbSubjects.SelectedIndex = -1 Then err.Add("Selecciona una assignatura.")

        If err.Count > 0 Then
            Dim s As String = "No es pot completar l'operació, detalls: "
            For Each err_m As String In err
                s = s & vbCrLf & "  + " & err_m
            Next
            MsgBox(s)
            Return False
            Exit Function
        End If
        Return True
    End Function
    Public Sub LoadElement(id As Integer) 'Per editar
        Element = DbClient.DbElementClient.getElementInfo(id)
        LoadValues()
    End Sub
    Private Sub ElementEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If IsNew = False Then
            LoadValues()
        End If
        FillAcademicLevelCmb()
    End Sub
    Sub FillAcademicLevelCmb()
        With cmbAcademicLevel.Items
            .Clear()
            For Each e As Entities.AcademicLevelEntity In c.filter._AcademicLevels
                .Add(e)
            Next
        End With
        cmbAcademicLevel.SelectedItem = FindItemContaining(cmbAcademicLevel.Items, c.localUser.academic_level)
        FillSubjectCmb()

    End Sub
    Sub FillSubjectCmb()
        If SelectedAcademicLevel Is Nothing Then Exit Sub
        With cmbSubjects.Items
            .Clear()
            For Each e As Entities.SubjectEntity In DbClient.DbSubjectEditorClient.getSubjectList(SelectedAcademicLevel.id)
                .Add(e)
            Next
        End With
    End Sub
    ReadOnly Property SelectedAcademicLevel As Entities.AcademicLevelEntity
        Get
            Return cmbAcademicLevel.SelectedItem
        End Get
    End Property
    ReadOnly Property SelectedSubject As Entities.SubjectEntity
        Get
            Return cmbSubjects.SelectedItem
        End Get
    End Property
    Private Sub cmbAcademicLevel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAcademicLevel.SelectionChanged
        FillSubjectCmb()

    End Sub

    Private Sub btnPublish_Click(sender As Object, e As RoutedEventArgs) Handles btnPublish.Click
        SaveAction()
        Me.Close()
    End Sub

    Private Sub txtTitle_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtTitle.GotFocus
        If txtTitle.Text = "Escriu un títol" Then txtTitle.Text = ""
        txtTitle.Foreground = Brushes.Black
    End Sub

    Private Sub txtDescription_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtDescription.GotFocus
        If txtDescription.Text = "Fes una petita descripció del teu treball aquí" Then txtDescription.Text = ""
        txtDescription.Foreground = Brushes.Black
    End Sub

    Private Sub txtPrice_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtPrice.GotFocus
        txtPrice.Text = ""
        txtPrice.Foreground = Brushes.Black
    End Sub

    Private Sub txtTitle_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtTitle.LostFocus
        If txtTitle.Text = "" Then
            txtTitle.Foreground = Brushes.Gray
            txtTitle.Text = "Escriu un títol"
        End If
    End Sub

    Private Sub txtDescription_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtDescription.LostFocus
        If txtDescription.Text = "" Then
            txtDescription.Foreground = Brushes.Gray
            txtDescription.Text = "Fes una petita descripció del teu treball aquí"
        End If
    End Sub

    Private Sub txtPrice_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtPrice.LostFocus
        If txtPrice.Text = "" Then
            txtPrice.Foreground = Brushes.Gray
            txtPrice.Text = "0"
        End If
    End Sub
End Class
