Imports TheLazyClientMVVM
Public Class ElementThumbnaiItem
    Private _Element As Entities.ElementEntity
    Public Property Element() As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
            UpdateUI()
        End Set
    End Property
    Sub UpdateUI()
        txtTitle.Content = Element.name
        lblDescription.Content = Element.description
        lblPrice.Content = Element.price
        imgFavourite.Visibility = If(Element.local_data.favourite, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
        imgEditGo.Visibility = If(Element.isFromLocalUser(), Windows.Visibility.Visible, Windows.Visibility.Collapsed)

    End Sub

    Sub OpenElementView()
        Dim frm As New ElementViewer
        frm.LoadElement(Element.id)
        frm.WindowStartupLocation = WindowStartupLocation.CenterScreen
        frm.ShowDialog()
        UpdateMainWindow()
    End Sub
    Private Sub ElementThumbnaiItem_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
        OpenElementView()

    End Sub

    Private Sub ElementThumbnaiItem_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseUp
        'MsgBox("s")
    End Sub
End Class
