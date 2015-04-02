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


    Sub UpdateUI()
        'Acivar/Desactivar botons...
        UpdateFileList()
    End Sub
    Sub UpdateFileList()
        'Mostrar fitxers a Handler.Files

    End Sub
    Sub OnProgessUpdated(progress As Long, total As Long) Handles Handler.ProgressUpdatedEvent
        'Update progessbar
    End Sub
End Class
