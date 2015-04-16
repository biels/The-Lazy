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
        lblFileSize.Content = String.Format("{0} / {1}", getExtensionDescription(), GetSizeReadable(File.size))
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
        Try
            image.BeginInit()
            image.UriSource = New Uri("pack://application:,,,/media/Icones/" + n + ".png")
            image.EndInit()

            imgIcon.Source = image
        Catch ex As Exception

        End Try
   
    End Sub
    Function getExtensionDescription() As String
        Dim e As String = getExtension()
        If e = "docx" Or e = "doc" Then Return "Document de Microsoft Word"
        If e = "xlsx" Or e = "xls" Then Return "Full ce càlcul de Microsoft Excel"
        If e = "pptx" Or e = "ppt" Then Return "Presentació de Microsoft Powerpoint"
        If e = "odt" Then Return "Document de text OpenDocument"
        If e = "txt" Then Return "Fitxer de text pla"
        If e = "html" Then Return "Llenguatge de marcat d'hipertext" 
        Return "Tipus de fitxer desconegut"
    End Function
    Private Sub imgDownload_Click(sender As Object, e As RoutedEventArgs) Handles imgDownload.Click
        RaiseEvent DownloadClick(File)
    End Sub
End Class
