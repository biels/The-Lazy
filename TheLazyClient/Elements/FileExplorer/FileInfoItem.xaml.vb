Imports TheLazyClientMVVM

Public Class FileInfoItem
    Public Event DownloadClick(File As FileExplorer.RemoteFileInfo)
    Private Sub FileInfoItem_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateUI()
    End Sub
    Private _File As FileExplorer.RemoteFileInfo
    Public Property File As FileExplorer.RemoteFileInfo
        Get
            Return _File
        End Get
        Set(ByVal value As FileExplorer.RemoteFileInfo)
            _File = value
        End Set
    End Property
    Sub UpdateUI()
        lblFileName.Content = File.filename
        lblFileSize.Content = String.Format("{0}, {1}", getExtensionDescription(), getFormattedSize)
        setIcon()
    End Sub
    Function getExtension() As String
        Dim array As String() = File.filename.Split(".")
        If array.Count > 1 Then
            Return array(array.Count - 1)
        End If
        Return ""
    End Function
    Function getIamgeName() As String
        Dim extension As String = getExtension()
        extension = extension.Substring(0, 1).ToUpper & extension.Substring(1, extension.Length - 1)
        Return String.Format("File-Extension-{0}", getExtension())
    End Function
    Sub setIcon()
        Dim n As String = getIamgeName()
        Dim image As BitmapImage = New BitmapImage()
        image.BeginInit()
        image.UriSource = New Uri("pack://application:,,,/media/Icones/" + n + ".png")
        image.EndInit()
        imgIcon.Source = image
    End Sub
    Function getFormattedSize() As String
        Return File.size & " unit" 'Millorar
    End Function
    Function getExtensionDescription() As String
        Select Case getExtension()
            Case Is = "exe"
                Return "Aplicació"
            Case Is = "docx" Or "doc"
                Return "Document de Microsoft Word"
            Case Is = "xlsx" Or "xls"
                Return "Document de Microsoft Excel"
            Case Is = "pptx" Or "ppt"
                Return "Document de Microsoft Powerpoint"

            Case Else
                Return "Tipus de fitxer desconegut"
        End Select
    End Function
    Private Sub imgDownload_Click(sender As Object, e As RoutedEventArgs) Handles imgDownload.Click
        RaiseEvent DownloadClick(File)
    End Sub
End Class
