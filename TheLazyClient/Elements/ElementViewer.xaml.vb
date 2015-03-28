Imports TheLazyClientMVVM

Public Class ElementViewer
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
    Public Sub LoadElement(id As Integer)
        Element = DbClient.DbElementClient.getElementInfo(id)
    End Sub

    Private Sub UpdateUI()
        If Element IsNot Nothing Then
            lblTitle.Content = Element.name
            lblDescription.Content = Element.description
            lblPrice.Content = Element.price
        End If
    End Sub
End Class
