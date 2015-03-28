Imports TheLazyClientMVVM
Public Class ElementThumbnaiItem
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
            UpdateUI()
        End Set
    End Property
    Sub UpdateUI()
        txtTitle.Content = Element.name
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As RoutedEventArgs) Handles btnEdit.Click
        Dim frm As New ElementViewer
        frm.LoadElement(Element.id)
        frm.Show()
    End Sub
End Class
