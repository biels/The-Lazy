Imports TheLazyClientMVVM

Public Class ElementViewer
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
        End Set
    End Property
    Public Sub LoadElement(id As Integer)
        Element = DbClient.DbElementClient.getElementInfo(id)
    End Sub

    Private Sub ElementViewer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LoadPropeties()
    End Sub

    Private Sub LoadPropeties()
        lblTitle.Content = Element.name
        lblDescription.Content = Element.description
        lblPrice.Content = Element.price
    End Sub
End Class
