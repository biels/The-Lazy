Imports TheLazyClientMVVM

''' <summary>
''' Control que mostra una llista d'elements basant-se en un filtre.
''' </summary>
''' <remarks></remarks>
Public Class ElementExplorerControl
    Private _Filter As TheLazyClientMVVM.ElementFilter
    Public Property Filter() As TheLazyClientMVVM.ElementFilter
        Get
            If _Filter Is Nothing Then Return c.filter
            Return _Filter
        End Get
        Set(ByVal value As TheLazyClientMVVM.ElementFilter)
            _Filter = value
        End Set
    End Property


    Dim _Initialized As Boolean = False
    Private Sub ElementExplorerControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Init()
    End Sub
    Public Sub Init()
        If _Initialized Then Exit Sub 'Evitar multi-handlings dels esdeveniments del filtre
        SetFilterHandlers()
        FillElementTabComboboxes()
        Filter.getFilteredElementsAsync()
        _Initialized = True
    End Sub
    Private Sub SetFilterHandlers()
        AddHandler Filter.RequestStarted, AddressOf OnRequestStarted
        AddHandler Filter.RequestDefined, AddressOf OnRequestDefined
        AddHandler Filter.ElementRecieved, AddressOf OnElementRecieved
        AddHandler Filter.RequestComplete, AddressOf OnRequestComplete
    End Sub
    Sub OnRequestStarted()
        pnlElements.Children.Clear()
        btnUpdateElements.IsEnabled = False
        progElementUpdateProgress.Visibility = Windows.Visibility.Visible
        progElementUpdateProgress.IsIndeterminate = True
        btnUpdateElements.Content = String.Format("Preaprant...")
    End Sub
    Sub OnRequestDefined(id_list As List(Of Integer))
        pnlElements.Children.Clear()
        progElementUpdateProgress.IsIndeterminate = False
        btnUpdateElements.Content = String.Format("Actualitzant...")
    End Sub
    Sub OnElementRecieved(e As Entities.ElementEntity, progress As Integer, total As Integer)
        Dim control As New ElementThumbnaiItem
        control.Element = e
        pnlElements.Children.Add(control)
        'btnUpdate.Content = String.Format("Actualitzant... ({0} de {1})", progress, total)
        progElementUpdateProgress.Value = progress
        progElementUpdateProgress.Maximum = total
    End Sub
    Sub OnRequestComplete(current_element_list As List(Of Entities.ElementEntity))
        btnUpdateElements.Content = "Actualitza"
        btnUpdateElements.IsEnabled = True
        progElementUpdateProgress.Value = 0
        progElementUpdateProgress.Visibility = Windows.Visibility.Collapsed
        'MsgBox("Completada")
    End Sub
    '------------
    Sub FillElementTabComboboxes()
        If Filter._AcademicLevels Is Nothing Then Exit Sub
        With cmbAcademicLevels.Items
            .Clear()
            For Each e As Entities.AcademicLevelEntity In Filter._AcademicLevels
                .Add(e)
            Next
        End With

    End Sub
    Sub UpdateSubjectsCombobox()
        If Filter._Subjects Is Nothing Then Exit Sub
        With cmbSubjectFilter.Items
            .Clear()
            For Each e As Entities.SubjectEntity In Filter._Subjects
                .Add(New ComboBoxItem() With {.Content = e, .Foreground = New SolidColorBrush(ColorFromInt(e.color))})
            Next
        End With
    End Sub
    '------------------

    Private Sub cmbAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAcademicLevels.SelectionChanged
        Filter.updateSubjectList(cmbAcademicLevels.SelectedItem)
        UpdateSubjectsCombobox()
    End Sub

    Private Sub cmbSubjectFilter_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSubjectFilter.SelectionChanged
        Filter.getFilteredElementsAsync()
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Filter.getFilteredElementsAsync()
    End Sub


End Class
