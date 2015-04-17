Imports TheLazyClientMVVM
Imports System.ComponentModel

Public Class RemoteFilesystemViewer
    Dim _Initialized As Boolean = False
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
            Handler = Com.main.fileExploreHandlerManager.getHandler(_Element.id)
            UpdateUI()
        End Set
    End Property
    Sub UpdateUI()
        progProgress.IsIndeterminate = True
        UpdateFileList()
        lblFileStat.Content = Handler.files.Count & "fitxers, " & GetSizeReadable(getTotalSize()) & " en total"
    End Sub

    Function getTotalSize() As Integer
        Dim total_size As Long = 0
        For Each e As FileExplorer.RemoteFileInfo In Handler.files
            total_size = total_size + e.size
        Next
        Return total_size
    End Function
    Sub UpdateFileList()
        pnlFileViwer.Children.Clear()
        For Each e As FileExplorer.RemoteFileInfo In Handler.files
            Dim control As New FileInfoItem
            control.File = e
            AddHandler control.DownloadClick, AddressOf control_DownloadClick_Event
            pnlFileViwer.Children.Add(control)
        Next
        progProgress.IsIndeterminate = False
    End Sub
    Sub control_DownloadClick_Event(File As FileExplorer.RemoteFileInfo)
        Dim dialog As New Microsoft.Win32.SaveFileDialog
        dialog.FileName = File.filename
        If dialog.ShowDialog() Then
            Handler.downloadFileAsync(File.filename, dialog.FileName)
        End If
    End Sub
    Sub OnProgessUpdated(progress As Long, total As Long) Handles Handler.ProgressUpdatedEvent
        'Update progessbar
    End Sub

    Private Sub RemoteFilesystemViewer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Init()
    End Sub
    Sub Init()
        If _Initialized Or DesignerProperties.GetIsInDesignMode(Me) Then Exit Sub
        Handler.init()
        Handler.requestFileListAsync()
        UpdateUI()
    End Sub
    'Event Handlers
    Private Sub ElementListRecieved_Event() Handles Handler.FileListRecieved
        UpdateUI()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Handler.requestFileListAsync()
    End Sub
End Class
