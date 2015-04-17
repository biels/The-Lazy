Imports TheLazyClientMVVM

Public Class ProfileViewerControl
    Private Filter As Filter.ElementFilter
    Private _Initialized As Boolean

    Private _Username As String
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property
    Public Sub Init() 'Tenint el username
        If Username = "" Then Throw New Exception("Nom d'usuari buit!")
        If _Initialized Then Exit Sub

        Filter = New Filter.ElementFilter
        Filter.username = Username
        controlElementViewer.Filter = Filter
        controlElementViewer.Init()
        _Initialized = True
    End Sub

    Private Sub txtProfileName_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtProfileName.GotFocus
        txtProfileName.Clear()
    End Sub
    Private Sub txtProfileName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProfileName.KeyDown
        If e.Key = Key.Enter Then
            MsgBox("Funció no implementada")
        End If
    End Sub
End Class
