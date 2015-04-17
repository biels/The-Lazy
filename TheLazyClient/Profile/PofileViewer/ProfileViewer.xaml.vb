Imports TheLazyClientMVVM
Public Class ProfileViewer

    Public Property Username() As String 'Wrapper
        Get
            Return control.Username
        End Get
        Set(ByVal value As String)
            control.Username = value
            control.Init()
        End Set
    End Property

End Class
