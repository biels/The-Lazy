Imports TheLazyClientMVVM

Public Class FileInfoItem
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
        lblFileSize.Content = File.size
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

End Class
