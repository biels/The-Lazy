Imports TheLazyClientMVVM

Public Class ElementExplorer
    Public Property Element As Entities.ElementEntity '"Wrapper"
        Get
            Return controlElementExplorer.Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            controlElementExplorer.Element = value
        End Set
    End Property
 

End Class



