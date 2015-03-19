Module Logger
    Public Sub Ll(text As String, level As LogLevel)
        '[16:33:46 INFO]: Done (3,709s)!
        'For Each w As Window In My.Application.Windows
        '    If TypeOf w Is ResultWindow Then
        '        Dim tw As ResultWindow = w
        '        tw.txtConsole.AppendText([String].Format("[{0} {1}]: {2}", DateTime.Now.ToShortTimeString(), [Enum].GetName(GetType(LogLevel), level).ToUpper(), text) & vbCrLf)

        '    End If
        'Next
        'DateTime.Now.ToShortTimeString(), Enum.GetName(typeof(LogLevel), level).ToUpper()), text);
    End Sub
    Function ResultWindowInstance() As ResultWindow
       
    End Function
    Public Sub I(text As String)
        'Console.BackgroundColor = ConsoleColor.DarkBlue;
        Ll(text, LogLevel.Info)
        ' Console.BackgroundColor = ConsoleColor.Black;
    End Sub
    Public Sub E(text As String)

        Ll(text, LogLevel.[Error])

    End Sub
    Public Sub F(text As String)

        Ll(text, LogLevel.Fatal)

    End Sub
    Public Sub D(text As String)

        Ll(text, LogLevel.Debug)

    End Sub
    Public Sub W(text As String)

        Ll(text, LogLevel.Warning)

    End Sub

    Public Enum LogLevel
        Info
        Warning
        [Error]
        Fatal
        Debug
        Core
    End Enum
End Module
