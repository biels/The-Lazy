Class Application

    Private Sub Application_DispatcherUnhandledException(sender As Object, e As Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Logger.E(e.Exception.Message)
    End Sub

    ' Los eventos de nivel de aplicación, como Startup, Exit y DispatcherUnhandledException
    ' se pueden controlar en este archivo.

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        'Dim w As ResultWindow = New ResultWindow
        'w.Show()
        ' I("Inicialitzant aplicació...")
        initC()
        c.init()
        ' AddHandler c.LogOutput, AddressOf LogHandle
        ' AddHandler TheLazyClientMVVM.Com.main.
        'c.wcfClient.channel.DoWork()
        'MessageBox.Show(c.wcfClient.factory.Endpoint.Address.ToString)
    End Sub
    Sub LogHandle(m As String)
        ' Ll(m, LogLevel.Core)
    End Sub
End Class
