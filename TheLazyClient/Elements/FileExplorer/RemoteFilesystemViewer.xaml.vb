Imports TheLazyClientMVVM
Public Class RemoteFilesystemViewer
    Private _IsReadOnly As Boolean
    Public Property IsReadOnly() As Boolean
        Get
            Return _IsReadOnly
        End Get
        Set(ByVal value As Boolean)
            _IsReadOnly = value
        End Set
    End Property
    Public WithEvents Handler As FileExplorer.FileExploreHandler 'Inicalitzat des de fora
    Private _Element As Entities.ElementEntity
    Public Property Element As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
        End Set
    End Property
    Private _File As FileExplorer.FileExploreHandler
    Public Property File As FileExplorer.FileExploreHandler
        Get
            Return _File
        End Get
        Set(ByVal value As FileExplorer.FileExploreHandler)
            _File = value
            UpdateUI()
        End Set
    End Property
    Sub UpdateUI()
        File.updateFileList()
        lblFileStat.Content = File.files.Count & "fitxers, " & getTotalSize() & " Kb en total"
    End Sub
    Function getTotalSize() As Integer
        Dim total_size As Long = 0
        For Each e As FileExplorer.RemoteFileInfo In File.files
            total_size = total_size + e.size
        Next
        Return total_size
    End Function
    Sub FillFileViwer()
        pnlFileViwer.Children.Clear()
        For Each e As FileExplorer.RemoteFileInfo In File.files
            Dim control As New FileInfoItem
            control.File = e
            pnlFileViwer.Children.Add(control)
        Next
    End Sub
    Sub OnProgessUpdated(progress As Long, total As Long) Handles Handler.ProgressUpdatedEvent
        'Update progessbar
    End Sub

    Private Sub RemoteFilesystemViewer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        File.init()
        UpdateUI()
    End Sub
End Class
