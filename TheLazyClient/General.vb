Module General
    'Public c As New TheLazyClientMVVM.MainClient
    Public hook As KeyboardListener
    Public Sub initC()
        hook = New KeyboardListener
        TheLazyClientMVVM.Com.main = New TheLazyClientMVVM.MainClient()
    End Sub
    Public Function c() As TheLazyClientMVVM.MainClient
        Return TheLazyClientMVVM.Com.main
    End Function

End Module
