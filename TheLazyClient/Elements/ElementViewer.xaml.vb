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
    Public Sub LoadElement(id As Integer) 'Per editar
        Element = DbClient.DbElementClient.getElementInfo(id)
    End Sub
End Class
