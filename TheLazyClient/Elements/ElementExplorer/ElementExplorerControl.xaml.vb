Imports TheLazyClientMVVM
Imports System.ComponentModel

''' <summary>
''' Control que mostra una llista d'elements basant-se en un filtre.
''' </summary>
''' <remarks></remarks>
Public Class ElementExplorerControl
    Private _Filter As Filter.ElementFilter
    Public Property Filter() As Filter.ElementFilter
        Get
            If _Filter Is Nothing Then Return c.filter
            Return _Filter
        End Get
        Set(ByVal value As Filter.ElementFilter)
            _Filter = value
        End Set
    End Property


    Dim _Initialized As Boolean = False
    Private Sub ElementExplorerControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Init()
    End Sub
    Public Sub Init()
        If _Initialized Or DesignerProperties.GetIsInDesignMode(Me) Then Exit Sub 'Evitar multi-handlings dels esdeveniments del filtre
        SetFilterHandlers()
        AcademicLevelCombobox()
        UpdateCriteriaCombobox()
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
    Sub AcademicLevelCombobox()
        If Filter._AcademicLevels Is Nothing Then Exit Sub
        With cmbAcademicLevels.Items
            .Clear()
            .Add(New ComboBoxItem() With {.Content = "Tots"})
            For Each e As Entities.AcademicLevelEntity In Filter._AcademicLevels
                .Add(e)
            Next
        End With
        cmbAcademicLevels.SelectedIndex = 0
    End Sub
    Sub UpdateSubjectsCombobox()
        If Filter._Subjects Is Nothing Then Exit Sub
        With cmbSubjectFilter.Items
            .Clear()
            .Add(New ComboBoxItem() With {.Content = "Tots"})
            For Each e As Entities.SubjectEntity In Filter._Subjects
                .Add(New ComboBoxItem() With {.Content = e, .Foreground = New SolidColorBrush(ColorFromInt(e.color))})
            Next
        End With
        cmbSubjectFilter.SelectedIndex = 0
    End Sub
    '------------------ ALERTA INDEXS BASE 1 (offset +1)

    Private Sub cmbAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAcademicLevels.SelectionChanged
        If TypeOf cmbAcademicLevels.SelectedItem Is Entities.AcademicLevelEntity Then
            Filter.updateSubjectList(cmbAcademicLevels.SelectedItem)
            cmbSubjectFilter.IsEnabled = True
            UpdateSubjectsCombobox()
        Else
            Filter.subject = Nothing
            cmbSubjectFilter.IsEnabled = False
            cmbSubjectFilter.Items.Clear()
        End If
    End Sub

    Private Sub cmbSubjectFilter_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSubjectFilter.SelectionChanged
        If cmbSubjectFilter.SelectedIndex <= Filter._Subjects.Count And cmbSubjectFilter.SelectedIndex > 0 Then
            Filter.subject = Filter._Subjects(cmbSubjectFilter.SelectedIndex - 1) 'Base 1
        Else
            Filter.subject = Nothing
        End If

        Filter.getFilteredElementsAsync()
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Filter.getFilteredElementsAsync()
    End Sub
    Private Sub UpdateCriteriaCombobox()
        With cmbOrderCriteria.Items
            .Clear()
            For Each v As TheLazyClientMVVM.Filter.ElementFilter.ElementOrderCriteriaEnum In [Enum].GetValues(GetType(TheLazyClientMVVM.Filter.ElementFilter.ElementOrderCriteriaEnum))
                .Add(GetDescription(v))
            Next
        End With
        cmbOrderCriteria.SelectedItem = FindItemContaining(cmbOrderCriteria.Items, GetDescription(Filter.order_criteria))
    End Sub
    'Passant informació cap al filtre


End Class
