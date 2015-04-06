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
        ImageFile(getExtension)
    End Sub
    Function getExtension() As String
        Dim array As String() = File.filename.Split(".")
        If array.Count > 1 Then
            Return array(array.Count - 1)
        End If
        Return ""
    End Function
    Sub ImageFile(extension As String)
        Select Case extension
            Case ".exe"


        End Select
    End Sub
End Class
